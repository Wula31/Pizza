using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;
using Pizza.Infrastructure.Data;
using Pizza.Infrastucture.Exceptions;

namespace Pizza.Infrastucture.Repository;

public class AddressRepository(ApplicationDbContext context) :Repository<Address>(context), IAddressRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task UpdateEntityAsync(Address entity)
    {
         entity.ModifiedOn = DateTime.UtcNow;
         _context.Addresses.Update(entity);
         await _context.SaveChangesAsync();
    }

    public async Task UpdateFieldAsync<T>(Guid id, string field, T value)
    {
        Address address = await GetEntityAsync(id);
        if (value == null) throw new FieldNotFoundException("There is no filed like this");
        switch (field)
        {
            case "UserId":
                address.UserId = value is Guid guidValue ? guidValue : throw new InvalidValueException("Value cannot be cast to Guid");
                break;
            case "City":
                address.City = value.ToString();
                break;
            case "Country":
                address.Country = value.ToString();
                break;
            case "Region":
                address.Region = value.ToString();
                break;
            case "Description":
                address.Description = value.ToString();
                break;
            default:
                throw new FieldNotFoundException($"There is no filed like this");
        }
        await UpdateEntityAsync(address);
    }
}