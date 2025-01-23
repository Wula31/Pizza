using Microsoft.EntityFrameworkCore;
using Pizza.Application.Common.Interfaces;
using Pizza.Infrastructure.Data;

namespace Pizza.Infrastucture.Repository;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<bool> UserExistsAsync(int userId)
    {
        return await context
            .Users.AnyAsync(u => 
                u.Id == userId);
    }

    public async Task<bool> UserMarkedDeletedAsync(int useId)
    {
        return await context
            .Users.AnyAsync(u => 
                u.Id == useId && u.IsDeleted);
    }
}