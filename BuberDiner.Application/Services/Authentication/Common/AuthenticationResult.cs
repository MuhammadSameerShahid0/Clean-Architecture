using BuberDiner.Domain.Entities;

namespace BuberDiner.Application.Services.Authentication.Common;

public record AuthenticationResult(
    User user,
    string Token);
