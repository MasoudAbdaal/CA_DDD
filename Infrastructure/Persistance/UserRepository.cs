using Application.Common.Interfaces.Persistance;
using Domain.Entities;

namespace Infrastructure.Persistance;

public class UserRepository : IUserRepository
{
    //if define NOT STATIC, _users going to be empty
    //WHY?
    private static readonly List<User> _users = new ();

    public void Add(User user)
    {
        _users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return _users.SingleOrDefault(u => u.Email == email);
    }
}