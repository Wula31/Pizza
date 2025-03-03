using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;
using Pizza.Infrastucture.Data;
using Pizza.Infrastucture.Exceptions;

namespace Pizza.Infrastucture.Repository;

public class AddressRepository :Repository<Address>, IAddressRepository
{
    private readonly ApplicationDbContext _context;
    public AddressRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task UpdateEntityAsync(Address address, CancellationToken cancellationToken = default)
    {
        if (address == null)
        {
            throw new AddressNotFoundException("The address is not found.");
        }

        if (address.IsDeleted)
        {
            throw new AddressDeletedException("The address is already deleted.");
        }
        
        address.ModifiedOn = DateTime.UtcNow;
        _context.Addresses.Update(address);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateFieldAsync(Guid id, string field, string value, CancellationToken cancellationToken = default)
    {
        Address address = await GetEntityAsync(id, cancellationToken);
        if (address.IsDeleted)
        {
            throw new AddressDeletedException("The address is already deleted.");
        }

        if (address == null)
        {
            throw new AddressNotFoundException("The address is not found.");
        }
        if (value == null) throw new FieldNotFoundException("There is no filed like this");
        switch (field)
        {
            case "UserId":
                if (Guid.TryParse(value, out Guid userId))
                    address.UserId = userId;
                else 
                    throw new InvalidValueException("Value cannot be cast to Guid");
                break;
            case "City":
                address.City = value;
                break;
            case "Country":
                address.Country = value;
                break;
            case "Region":
                address.Region = value;
                break;
            case "Description":
                address.Description = value;
                break;
            default:
                throw new FieldNotFoundException($"There is no filed like this");
        }
        await UpdateEntityAsync(address, cancellationToken);
    }

    public new async Task DeleteEntityAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var address = await GetEntityAsync(id, cancellationToken);
        address.IsDeleted = true; 
        address.ModifiedOn = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
    }

}