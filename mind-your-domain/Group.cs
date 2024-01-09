using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace mind_your_domain;

public class Group
{
    [Key] [JsonIgnore] public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public List<User> Members { get; } = new();
    
}