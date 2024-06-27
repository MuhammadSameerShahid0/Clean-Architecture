using BuberDiner.Application.Common.Errors;
using BuberDiner.Application.Services.Authentication.Common;
using ErrorOr;
using FluentResults;


namespace BuberDiner.Application.Services.Authentication.Queries;

public interface IAuthenticationQueryServices
{
    ErrorOr<AuthenticationResult> Login (string Email, string Password);
}