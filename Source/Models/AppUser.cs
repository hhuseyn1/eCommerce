using Microsoft.AspNetCore.Identity;

namespace Source.Models;

public class AppUser:IdentityUser
{
    public string FullName { get; set; }
    public int Year { get; set; }
}   
