using Bogus;
using mind_your_domain;

namespace mind_your_tests.Generators;

public static class UserGenerator
{
    public static List<User> Generate(int quantity)
    {
        var fakeUser = new Faker<User>()
            .RuleFor(u => u.Name, f => f.Person.UserName);

        var users = fakeUser.Generate(quantity);

        return users;
    }
}