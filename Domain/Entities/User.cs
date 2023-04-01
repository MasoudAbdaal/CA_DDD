namespace Domain.Entities;

public class User
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Password { get; set; } = null!;
}
