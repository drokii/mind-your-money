using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using mind_your_domain;
using static System.Text.Encoding;

namespace mind_your_money_server.Api.Security;

public static class SecurityUtilities
{
    private const int KeySize = 128; // Size of the salt, and the hash.
    private const int Iterations = 350000; // Nr. of times PBKDF is applied during the hashing process.
    private const double TokenDuration = 12; // Duration until a generated token expires.

    public static byte[] Hash(string input, byte[] salt)
    {
        return Rfc2898DeriveBytes.Pbkdf2(
            input,
            salt,
            Iterations,
            HashAlgorithmName.SHA512,
            KeySize
        );
    }

    public static byte[] GenerateSalt()
    {
        return RandomNumberGenerator.GetBytes(KeySize);
    }

    public static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
    {
        var hashedPassword = Hash(password, storedSalt);

        return CryptographicOperations.FixedTimeEquals(
            hashedPassword,
            storedHash);
    }

    public static string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        //TODO: better way 2 do this plsthx (it sucks)
        string? secret = Environment.GetEnvironmentVariable("SECRET");

        if (secret == null)
            throw new CryptographicUnexpectedOperationException(
                "The secret's missing, bud. Because this is in development, set the variable SECRET in your environment to solve this.");

        var key = ASCII.GetBytes(secret);

        var tokenDescriptor = DescribeToken(user, key);

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static SecurityTokenDescriptor DescribeToken(User user, byte[] key)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Role, Roles.User.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(TokenDuration),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        return tokenDescriptor;
    }
}