using System.Text.Json.Serialization;

namespace mind_your_domain;

using System.ComponentModel.DataAnnotations;

public class User
{
    [Key] [JsonIgnore] public Guid Id { get; init; } = Guid.NewGuid();
    
    public string Name { get; init; }
    public string Password { get; set; }
    public string Email { get; set; }
    
    public List<Group> Groups { get; } = [];

}