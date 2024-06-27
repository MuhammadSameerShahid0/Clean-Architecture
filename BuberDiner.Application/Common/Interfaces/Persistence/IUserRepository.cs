using BuberDiner.Domain.Entities;

namespace BuberDiner.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User GetUsersByEmail(string email);
    void Add (User user);

}