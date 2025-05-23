using Microsoft.AspNetCore.Identity;

namespace Lab_10_Qquelcca.Infrastructure.Security;

public class PasswordHasher
{
    private readonly PasswordHasher<object> _hasher = new();

    public string HashPassword(string password)
    {
        return _hasher.HashPassword(null, password);
    }

    public bool VerifyPassword(string hash, string providedPassword)
    {
        var result = _hasher.VerifyHashedPassword(null, hash, providedPassword);
        return result == PasswordVerificationResult.Success;
    }
}