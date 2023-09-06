using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Source.Data;
using Source.Encryptors;
using Source.Models;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json;

namespace Source.Services;

public class UserManager : IUserManager
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly AppDbContext _context;

    public UserManager(IHttpContextAccessor contextAccessor, AppDbContext context)
    {
        this._contextAccessor = contextAccessor;
        this._context = context;
    }
    public UserCredentials GetCredentials()
    {
        if (_contextAccessor is not null && _contextAccessor.HttpContext is not null
            && _contextAccessor.HttpContext.Request.Cookies.ContainsKey("auth"))
        {
            var hash = _contextAccessor.HttpContext.Request.Cookies["auth"];
            var text = AesEncryptor.DecryptString("b14ca5898a4e4133bbce2ea2315a1916", hash);
            return JsonSerializer.Deserialize<UserCredentials>(text);
        }
        return null;
    }

    public bool Login(string username, string password)
    {
        var passHash = SHA256Encryptor.Encrypt(password);
        var user = _context.Users.FirstOrDefault(u => u.Login == username &&
        passHash == u.PasswordHash);
        if (user is not null)
        {
            UserCredentials credentials = new()
            {
                Login = user.Login,
                isAdmin = user.isAdmin
            };
            var json = JsonSerializer.Serialize(credentials);
            var encJson = AesEncryptor.EncryptString("b14ca5898a4e4133bbce2ea2315a1916", json);
            _contextAccessor.HttpContext.Response.Cookies.Append("auth", encJson);
            return true;
        }
        return false;
    }

    public bool Register(string username, string password, bool isAdmin)
    {
        var exsist = _context.Users.Any(u => u.Login == username);
        if (!exsist)
        {
            _context.Users.Add(new Models.User
            {
                Login = username,
                PasswordHash = SHA256Encryptor.Encrypt(password),
                isAdmin = false
            });
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}
