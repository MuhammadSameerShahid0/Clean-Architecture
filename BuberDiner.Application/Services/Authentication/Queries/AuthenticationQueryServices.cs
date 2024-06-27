using BuberDiner.Application.Common.Errors;
using BuberDiner.Application.Common.Interfaces.Authentication;
using BuberDiner.Application.Common.Interfaces.Persistence;
using BuberDiner.Application.Services.Authentication.Common;
using BuberDiner.Domain.Common.Errors;
using BuberDiner.Domain.Entities;
using ErrorOr;
using FluentResults;
using OneOf;

namespace BuberDiner.Application.Services.Authentication.Queries;

public class AuthenticationQueryServices : IAuthenticationQueryServices
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    private readonly IUserRepository _userRepository;
    public AuthenticationQueryServices(IJwtTokenGenerator jwtTokenGenerator , IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public ErrorOr<AuthenticationResult> Login(string Email, string Password)
    {   
        // 1. Validate the User Exists
        if(_userRepository.GetUsersByEmail(Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // 2. Validate the Password Correct
        if (user.Password != Password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // 3. Create JWT Token
        var Token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            Token);
    }
}