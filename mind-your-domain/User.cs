using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace mind_your_domain;

public class User
{
    [Key] [JsonIgnore] public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; }
    public string Email { get; set; }
    public List<Group> Groups { get; } = [];
    public byte[] Password { get; set; }
    public byte[] Salt { get; set; }

    // Necessary for Bogus data generation
    public User()
    {
    }

    public User(string name, string email, byte[] password, byte[] salt)
    {
        Name = name;
        Email = email;
        Password = password;
        Salt = salt;
    }
}