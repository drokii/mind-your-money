using FluentValidation;
using mind_your_money_server.Api.DTOs;

namespace mind_your_money_server.Api.Validation;

public class LoginRequestValidator : AbstractValidator<LogInRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(request => request.Name).NotNull().NotEmpty();
        RuleFor(request => request.Password).NotNull().NotEmpty();
    }
}

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(request => request.Name).NotNull().NotEmpty();
        RuleFor(request => request.Password).NotNull().NotEmpty();
        RuleFor(request => request.Email).NotNull().NotEmpty();
    }
}