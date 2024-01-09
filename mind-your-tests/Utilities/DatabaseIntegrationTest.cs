using Microsoft.EntityFrameworkCore;
using mind_your_domain.Database;

namespace mind_your_tests.Utilities;

public abstract class DatabaseIntegrationTest<T> where T : class
{
    protected MindYourMoneyDb _db;

    [OneTimeSetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<MindYourMoneyDb>()
            .UseInMemoryDatabase(databaseName: "Testy McTestington")
            .Options;

        _db = new MindYourMoneyDb(options);
    }
    
    [TearDown]
    public void TearDown()
    {
        _db.RemoveRange(_db.Set<T>());
    }
}