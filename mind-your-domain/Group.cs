using System.ComponentModel.DataAnnotations;

namespace mind_your_domain;

public class Group
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public List<User> Members { get; } = new();
    
}