using BuberDiner.Application.Authentication.Commands.Register;
using BuberDiner.Application.Authentication.Queries.Login;
using BuberDiner.Application.Common.Errors;
using BuberDiner.Application.Services.Authentication.Command;
using BuberDiner.Application.Services.Authentication.Common;
using BuberDiner.Application.Services.Authentication.Queries;
using BuberDiner.Contracts.Authentication;
using BuberDiner.Domain.Common.Errors;
using ErrorOr;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using LoginRequest = BuberDiner.Contracts.Authentication.LoginRequest;
using RegisterRequest = BuberDiner.Contracts.Authentication.RegisterRequest;

namespace BuberDiner.Api.Controllers;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    //private readonly IAuthenticationCommandServices _authenticationCommandServices;
    //private readonly IAuthenticationQueryServices _authenticationQuerServices;
    //public AuthenticationController(IAuthenticationCommandServices authenticationServices , IAuthenticationQueryServices authenticationQueryServices)
    //{
    //    _authenticationCommandServices = authenticationServices;
    //    _authenticationQuerServices = authenticationQueryServices;
    //}

    public AuthenticationController(ISender mediator , IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        //OneOf<AuthenticationResult , DuplicateEmailError> RegisterResult = _authenticationServices.Register(
        //    registerRequest.FirstName,
        //    registerRequest.LastName,
        //    registerRequest.Email,
        //    registerRequest.Password);

        // FluentResult Code.

        //Result<AuthenticationResult> RegisterResult = _authenticationServices.Register(
        //     registerRequest.FirstName,
        //     registerRequest.LastName,
        //     registerRequest.Email,
        //     registerRequest.Password);

        //var command = new RegisterCommand(
        //    registerRequest.FirstName,
        //    registerRequest.LastName,
        //    registerRequest.Email,
        //    registerRequest.Password);

        var command = _mapper.Map<RegisterCommand>(registerRequest);

        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);
      
        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        //var query = new LoginQuery(loginRequest.Email, loginRequest.Password);

        var query = _mapper.Map<LoginQuery>(loginRequest);
        var authResult = await _mediator.Send(query);

        if (authResult.IsError
            && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
            statusCode: StatusCodes.Status401Unauthorized,
            title: authResult.FirstError.Description);
        }

        return authResult.Match(
        authResult => Ok(MapAuthResult(authResult)),
        errors => Problem(errors));
    }
    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.user.Id,
            authResult.user.FirstName,
            authResult.user.LastName,
            authResult.user.Email,
            authResult.Token);
    }
}