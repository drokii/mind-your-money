namespace mind_your_domain;

using System.ComponentModel.DataAnnotations;

public class User
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
}