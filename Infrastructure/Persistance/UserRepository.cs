using Application.Common.Interfaces.Persistance;
using Domain.Entities;

namespace Infrastructure.Persistance;

public class UserRepository : IUserRepository
{
    //Why It Defines As STATIC?
    //https://stackoverflow.com/questions/4026785/how-do-static-properties-work-in-an-asp-net-environment/4026795#4026795
    private static readonly List<User> _users = new();

    public void Add(User user)
    {
        _users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return _users.SingleOrDefault(u => u.Email == email);
    }
}