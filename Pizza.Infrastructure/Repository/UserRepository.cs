using Microsoft.EntityFrameworkCore;
using Pizza.Infrastructure.Data;
using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;
using Pizza.Infrastucture.Exceptions;

namespace Pizza.Infrastucture.Repository;

public class UserRepository(ApplicationDbContext context) : Repository<User>(context), IUserRepository
{
    private readonly ApplicationDbContext _context = context;

    public override async Task DeleteEntityAsync(Guid id)
    {
        var entity = await GetEntityAsync(id);
        var address = await _context.Addresses.FirstOrDefaultAsync(x => x.UserId.Equals(id));
        entity.IsDeleted = true;
        entity.Address = "Deleted";
        if (address == null) throw new AddressNotFoundException("Address not found");
        address.IsDeleted = true;
        address.ModifiedOn = DateTime.UtcNow;
        entity.ModifiedOn = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEntityAsync(User entity)
    {
        entity.ModifiedOn = DateTime.UtcNow;
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateFieldAsync<T>(Guid id, string field, T value)
    {
        User user = await GetEntityAsync(id);
        if (value == null) throw new FieldNotFoundException("There is no filed like this");
        switch (value)
        {
            case "FirstName":
                user.FirstName = value.ToString();
                break;
            case "LastName":
                user.LastName = value.ToString();
                break;
            case "Email":
                user.Email = value.ToString();
                break;
            case "PhoneNumber":
                user.PhoneNumber = value.ToString();
                break;
            case "Address":
                user.Address = value.ToString();
                break;
            default:
                throw new FieldNotFoundException($"There is no filed like this");
        }
        await UpdateEntityAsync(user);
    }
}