using BuberDiner.Application.Common.Errors;
using BuberDiner.Application.Common.Interfaces.Authentication;
using BuberDiner.Application.Common.Interfaces.Persistence;
using BuberDiner.Application.Services.Authentication.Common;
using BuberDiner.Domain.Common.Errors;
using BuberDiner.Domain.Entities;
using ErrorOr;
using FluentResults;
using OneOf;

namespace BuberDiner.Application.Services.Authentication.Command;

public class AuthenticationCommandServices : IAuthenticationCommandServices
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    private readonly IUserRepository _userRepository;
    public AuthenticationCommandServices(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public ErrorOr<AuthenticationResult> Register(string FirstName, string LastName, string Email, string Password)
    {
        if (_userRepository.GetUsersByEmail(Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        var user = new User
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Password = Password
        };
        _userRepository.Add(user);

        var Token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
           user,
           Token);
    }
}