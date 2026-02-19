namespace ViggoMagnus.Services;

public record User(string Username, string PasswordHash);

public interface IUserService
{
    bool Register(string username, string password);
    bool Validate(string username, string password);
}
