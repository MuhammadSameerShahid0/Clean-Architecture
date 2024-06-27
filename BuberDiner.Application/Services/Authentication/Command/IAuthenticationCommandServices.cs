using BuberDiner.Application.Common.Errors;
using BuberDiner.Application.Services.Authentication.Common;
using ErrorOr;
using FluentResults;


namespace BuberDiner.Application.Services.Authentication.Command;

public interface IAuthenticationCommandServices
{
    ErrorOr<AuthenticationResult> Register(string FirstName, string LastName, string Email, string Password);
}