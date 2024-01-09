using Bogus;
using mind_your_domain;

namespace mind_your_tests.Utilities;

public static class UserGenerator 
{
    public static List<User> Generate(int quantity)
    {
        var fakeUser = new Faker<User>()
            .RuleFor(u => u.Name, f => f.Person.UserName)
            .RuleFor(u => u.Password, f => f.Internet.Password())
            .RuleFor(u => u.Email, f => f.Internet.Email());
            
        var users = fakeUser.Generate(quantity);

        return users;
    }
}