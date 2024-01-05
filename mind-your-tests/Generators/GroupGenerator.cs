using Bogus;
using mind_your_domain;

namespace mind_your_tests.Generators;

public static class GroupGenerator
{
    public static List<Group> Generate(int quantity)
    {
        var fakeUser = new Faker<Group>()
            .RuleFor(g => g.Name, f => f.Hacker.Noun());

        var users = fakeUser.Generate(quantity);

        return users;
    }
}