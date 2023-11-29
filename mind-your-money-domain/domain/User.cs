using System.ComponentModel.DataAnnotations;

namespace mind_your_money_domain;

public class User
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
}