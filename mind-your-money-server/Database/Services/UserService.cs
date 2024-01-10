using Microsoft.EntityFrameworkCore;
using mind_your_domain;
using mind_your_domain.Database;
using mind_your_domain.Database.Services;

namespace mind_your_money_server.Database.Services;

public class UserService(MindYourMoneyDb db) : IEntityService<User>
{
    public async Task<User?> GetUserByName(string name)
    {
        return await db.Users.FirstOrDefaultAsync(
            user => user.Name.Equals(name));
    }

    public async Task CreateUser(User user)
    {
        // TODO: Add anti-duplicate measure
        await db.AddAsync(user);
        await db.SaveChangesAsync();
    }
    
    public async Task<List<User>> GetAllUsers()
    {
        return await db.Users.ToListAsync();
    }

    public async Task<User?> FindUserById(Guid userId)
    {
        return await db.Users.FindAsync(userId);
    }

    public async Task RemoveUser(User userToBeRemoved)
    {
        db.Users.Remove(userToBeRemoved);
        await db.SaveChangesAsync();    }
}