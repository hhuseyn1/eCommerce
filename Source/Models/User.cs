namespace Source.Models;

public class User
{
    public string Id { get; set; }
    public string? Login { get; set; }
    public string? PasswordHash { get; set; }
    public bool isAdmin { get; set; }
}
