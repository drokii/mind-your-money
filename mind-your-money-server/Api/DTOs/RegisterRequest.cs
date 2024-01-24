using mind_your_money_server.Api.Validation;

namespace mind_your_money_server.Api.DTOs;

public class RegisterRequest : IRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public bool IsValid()
    {
        return new RegisterRequestValidator().Validate(this).IsValid;
    }
}