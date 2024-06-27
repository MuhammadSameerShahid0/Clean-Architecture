using BuberDiner.Application.Services.Authentication.Common;
using ErrorOr;
using MediatR;
using OneOf.Types;

namespace BuberDiner.Application.Authentication.Commands.Register;

public record RegisterCommand(
            string FirstName,
            string LastName,
            string Email,
            string Password) : IRequest<ErrorOr<AuthenticationResult>>;
