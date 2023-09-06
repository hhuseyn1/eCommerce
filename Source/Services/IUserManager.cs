using Source.Models;

namespace Source.Services;

public interface IUserManager
{
    bool Register(string username, string password,bool isAdmin);
    bool Login(string username, string password);
    UserCredentials GetCredentials();
}
