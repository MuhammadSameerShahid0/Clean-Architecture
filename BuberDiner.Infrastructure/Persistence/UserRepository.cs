using BuberDiner.Application.Common.Interfaces.Persistence;
using BuberDiner.Domain.Entities;

namespace BuberDiner.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new List<User>();
    public void Add(User user)
    {
        _users.Add(user);
    }

    public User GetUsersByEmail(string email)
    {
        return _users.SingleOrDefault(user => user.Email == email);
    }
}