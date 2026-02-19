using System.Security.Cryptography;
using System.Text;

namespace ViggoMagnus.Services;

public class UserService : IUserService
{
    private readonly List<User> _users = new();

    public bool Register(string username, string password)
    {
        if (_users.Any(u => u.Username == username)) return false;
        var hash = Hash(password);
        _users.Add(new User(username, hash));
        return true;
    }

    public bool Validate(string username, string password)
    {
        var user = _users.FirstOrDefault(u => u.Username == username);
        if (user == null) return false;
        return user.PasswordHash == Hash(password);
    }

    private static string Hash(string input)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(bytes);
    }
}
