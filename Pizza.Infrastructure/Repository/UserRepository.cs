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
        entity.IsDeleted = true;
        entity.Address = "Deleted";
        entity.ModifiedOn = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }

    
}