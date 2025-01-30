using Microsoft.EntityFrameworkCore;
using Pizza.Infrastructure.Data;
using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;
namespace Pizza.Infrastucture.Repository;

public class UserRepository(ApplicationDbContext context) : Repository<User>(context), IUserRepository
{
    public override async Task DeleteEntityAsync(int id)
    {
        var entity = await GetEntityAsync(id);
        var address = await context.Addresses.FirstOrDefaultAsync(x => x.UserId == id);
        entity.IsDeleted = true;
        entity.Address = "Deleted";
        address.IsDeleted = true;
        address.ModifiedOn = DateTime.UtcNow;
        entity.ModifiedOn = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }
}