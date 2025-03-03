using Microsoft.EntityFrameworkCore;
using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;
using Pizza.Infrastucture.Data;
using Pizza.Infrastucture.Exceptions;

namespace Pizza.Infrastucture.Repository;

public class PizzaRepository(ApplicationDbContext context) : Repository<PizzaE>(context), IPizzaRepository
{
    private readonly ApplicationDbContext _context = context;

        public async Task<PizzaE> GetPizzaByName(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Pizzas.FirstOrDefaultAsync(x => x.Name == name && !x.IsDeleted, cancellationToken) ?? throw new PizzaNotFoundException("Pizza not found");
        }

    public async Task UpdateEntityAsync(PizzaE pizza, CancellationToken cancellationToken = default)
    {
        if (pizza == null)
        {
            throw new UserNotFoundException("The address is not found.");
        }

        if (pizza.IsDeleted)
        {
            throw new UserDeletedException("The address is already deleted.");
        }
        
        pizza.ModifiedOn = DateTime.UtcNow;
        _context.Pizzas.Update(pizza);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task UpdateFieldAsync(Guid id, string field, string value, CancellationToken cancellationToken = default)
    {
        PizzaE pizza = await GetEntityAsync(id, cancellationToken);
        if (pizza == null)
        {
            throw new PizzaNotFoundException("Pizza not found");
        }

        if (pizza.IsDeleted)
        {
            throw new PizzaDeletedException("Pizza is deleted");
        }
        
        if (value == null) throw new FieldNotFoundException("There is no filed like this");
        switch (field)
        {
            case "Name":
                pizza.Name = value;
                break;
            case "Price":
                if (decimal.TryParse(value, out decimal price))
                    pizza.Price = price;
                else
                    throw new InvalidValueException("Value cannot be cast to decimal");
                break;
            case "Description":
                pizza.Description = value;
                break;
            case "ImageId":
                if (Guid.TryParse(value, out Guid guidValue))
                    pizza.ImageId = guidValue;
                else
                    throw new InvalidValueException("Value cannot be cast to Guid");
                break;
            case "CaloryCount":
                if(int.TryParse(value, out int caloryCount))
                    pizza.CaloryCount = caloryCount;
                else
                    throw new InvalidValueException("Value cannot be cast to integer");
                break;
            default:
                throw new FieldNotFoundException($"There is no filed like this");
        }
        await UpdateEntityAsync(pizza, cancellationToken);
    }
}