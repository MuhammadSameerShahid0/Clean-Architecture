using BuberDiner.Domain.Entities;

namespace BuberDiner.Application.Authentication.Common;

public record AuthenticationResult(
    User User , 
    string Token);
