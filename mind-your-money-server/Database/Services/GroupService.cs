using Microsoft.EntityFrameworkCore;
using mind_your_domain;
using mind_your_domain.Database;
using mind_your_domain.Database.Services;

namespace mind_your_money_server.Database.Services;

public class GroupService(MindYourMoneyDb db) : IEntityService<Group>
{
    public async Task<List<Group>> GetAll()
    {
        return await db.Groups.ToListAsync();
    }

    public async Task Create(Group toBeCreated)
    {
        await db.Groups.AddAsync(toBeCreated);
        await db.SaveChangesAsync();
    }

    public async Task<Group?> FindById(Guid id)
    {
        return await db.Groups.FindAsync(id);
    }

    public async Task Remove(Group toBeRemoved)
    {
        db.Groups.Remove(toBeRemoved);
        await db.SaveChangesAsync();
    }
}