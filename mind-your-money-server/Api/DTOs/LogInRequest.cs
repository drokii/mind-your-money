using mind_your_money_server.Api.Validation;

namespace mind_your_money_server.Api.DTOs;

public class LogInRequest : IRequest
{
    public string Name { get; set; }
    public string Password { get; set; }

    public bool IsValid()
    {
        return new LoginRequestValidator().Validate(this).IsValid;
    }
}