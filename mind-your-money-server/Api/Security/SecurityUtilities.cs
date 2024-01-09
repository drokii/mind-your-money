using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using mind_your_domain;
using static System.Text.Encoding;
using Convert = System.Convert;

namespace mind_your_money_server.Api.Security;

public static class SecurityUtilities {
    
    private const int KeySize = 32; // Size of the salt, and the hash.
    private const int Iterations = 350000; // Nr. of times PBKDF is applied during the hashing process.
    private const double tokenDuration = 12; // Duration until a generated token expires.
    
    public static byte[] Hash(string input, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(KeySize);
        
        return Rfc2898DeriveBytes.Pbkdf2(
            input,
            salt,
            Iterations,
            HashAlgorithmName.SHA512,
            KeySize
        );
    }

    public static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
    {
        var hashedPassword = Hash(password, out _);

        return CryptographicOperations.FixedTimeEquals(
            hashedPassword,
            storedHash);
    }
    
    public static string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        string? secret = Environment.GetEnvironmentVariable("Secret");

        if (secret == null)
            throw new CryptographicUnexpectedOperationException("The secret's missing, bud.");
        
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
                new(ClaimTypes.Name, user.Name), // Assuming username for identification
                new(ClaimTypes.Role, Roles.User.ToString()) // Adding a claim for the "User" role
            }),
            Expires = DateTime.UtcNow.AddHours(tokenDuration),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        return tokenDescriptor;
    }
}