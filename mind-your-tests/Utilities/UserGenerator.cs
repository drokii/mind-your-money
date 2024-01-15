using Bogus;
using mind_your_domain;
using mind_your_money_server.Api.Security;

namespace mind_your_tests.Utilities;

public static class UserGenerator
{
    public const string DefaultPassword = "DefaultPassword";

    public static List<User> Generate(int quantity)
    {
        var fakeUser = new Faker<User>()
            .RuleFor(u => u.Name, f => f.Person.UserName)
            .RuleFor(u => u.Email, f => f.Internet.Email());

        var users = fakeUser.Generate(quantity);

        // Generate password & salt
        users.ForEach(u =>
        {
            var salt = SecurityUtilities.GenerateSalt();
            u.Password = SecurityUtilities.Hash(DefaultPassword, salt);
            u.Salt = salt;
        });

        return users;
    }
}