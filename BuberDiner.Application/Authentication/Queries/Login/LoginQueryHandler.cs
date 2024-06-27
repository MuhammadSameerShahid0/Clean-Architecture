using BuberDiner.Application.Authentication.Queries.Login;
using BuberDiner.Application.Common.Interfaces.Authentication;
using BuberDiner.Application.Common.Interfaces.Persistence;
using BuberDiner.Application.Services.Authentication.Common;
using BuberDiner.Domain.Common.Errors;
using BuberDiner.Domain.Entities;
using ErrorOr;
using MediatR;

namespace BuberDiner.Application.Authentication.Commands.Queries;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    private readonly IUserRepository _userRepository;
    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // 1. Validate the User Exists
        if (_userRepository.GetUsersByEmail(query.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // 2. Validate the Password Correct
        if (user.Password != query.Password)
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
