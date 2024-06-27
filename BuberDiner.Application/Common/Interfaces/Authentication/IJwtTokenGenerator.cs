using BuberDiner.Domain.Entities;

namespace BuberDiner.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}