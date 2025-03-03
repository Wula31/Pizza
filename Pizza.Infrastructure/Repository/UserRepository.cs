using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;
using Pizza.Infrastucture.Data;
using Pizza.Infrastucture.Exceptions;

namespace Pizza.Infrastucture.Repository;

public class UserRepository(ApplicationDbContext context, IAddressRepository addressRepository) : Repository<User>(context), IUserRepository
{
    private readonly ApplicationDbContext _context = context;

    public override async Task DeleteEntityAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await GetEntityAsync(id, cancellationToken);
        
        if (user == null)
        {
            throw new UserNotFoundException("User not found");
        }
        
        var addresses = user.Addresses;
        
        
        user.IsDeleted = true;
        user.ModifiedOn = DateTime.UtcNow;

        if (!addresses.Any())
        {
            throw new AddressNotFoundException("Address not found");
        }
            
        foreach (var address2 in addresses)
        {
            address2.IsDeleted = true;
            address2.ModifiedOn = DateTime.UtcNow;
        }
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateEntityAsync(User user, CancellationToken cancellationToken = default)
    {
        if (user == null)
        {
            throw new UserNotFoundException("The user is not found.");
        }

        if (user.IsDeleted)
        {
            throw new UserDeletedException("The user is already deleted.");
        }
        
        user.ModifiedOn = DateTime.UtcNow;
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task UpdateFieldAsync(Guid id, string field, string value, CancellationToken cancellationToken = default)
    {
        User user = await GetEntityAsync(id, cancellationToken);
        if (user == null)
        {
            throw new UserNotFoundException("User not found");
        }

        if (user.IsDeleted)
        {
            throw new UserDeletedException("User is deleted");
        }
        
        if (value == null) throw new FieldNotFoundException("There is no filed like this");
        switch (field)
        {
            case "FirstName":
                user.FirstName = value;
                break;
            case "LastName":
                user.LastName = value;
                break;
            case "Email":
                user.Email = value;
                break;
            case "PhoneNumber":
                user.PhoneNumber = value;
                break;
            case "Addresses":
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    
                    Address address = JsonSerializer.Deserialize<Address>(value, options);
                    
                    if (address != null)
                    {
                        var existingAddress = user.Addresses.FirstOrDefault(a => a.Id == address.Id);
                        
                        if (existingAddress == null || user.Addresses.Count == 0)
                        {
                            await addressRepository.CreateEntityAsync(address, cancellationToken);
                        }
                        else
                        {
                            existingAddress.City = address.City;
                            existingAddress.Country = address.Country;
                            existingAddress.Region = address.Region;
                            existingAddress.Description = address.Description;
                            await addressRepository.UpdateEntityAsync(address, cancellationToken);
                        }
                    }
                    else
                    {
                        throw new FieldNotFoundException("Invalid address format");
                    }
                }
                catch (JsonException)
                {
                    throw new FieldNotFoundException("Invalid address format");
                }
                break;
            default:
                throw new FieldNotFoundException($"There is no field like this");
        }
        await UpdateEntityAsync(user,cancellationToken);
    }
    public new async Task<IEnumerable<User>> GetAllEntitiesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Where(x => !x.IsDeleted)
            .Include(u => u.Addresses.Where(x => !x.IsDeleted))
            .ToListAsync(cancellationToken);
    }
    
    public new async Task<User> GetEntityAsync(Guid id, CancellationToken cancellationToken = default)
    {
        User user = await _context.Users
            .Include(x => x.Addresses)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new InvalidOperationException();
        
        if (user == null) throw new UserNotFoundException("User not found");
        if(user.IsDeleted) throw new UserDeletedException("The user is deleted");
    
        return user;
    }
}