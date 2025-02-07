using Microsoft.EntityFrameworkCore;
using Pizza.Application.Common.Interfaces;
using Pizza.Infrastructure.Data;
using Pizza.Domain.Entities;
using Pizza.Infrastucture.Exceptions;

namespace Pizza.Infrastucture.Repository;

public class PizzaRepository(ApplicationDbContext context) : Repository<PizzaE>(context), IPizzaRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<PizzaE> GetPizzaByName(string name)
    {
        return await _context.Pizzas.FirstOrDefaultAsync(x => x.Name == name) ?? throw new PizzaNotFoundException("Pizza not found");
    }

    public async Task UpdateEntityAsync(PizzaE entity)
    {
        entity.ModifiedOn = DateTime.UtcNow;
        _context.Pizzas.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateFieldAsync<T>(Guid id, string field, T value)
    {
        PizzaE pizza = await GetEntityAsync(id);
        if (value == null) throw new FieldNotFoundException("There is no filed like this");
        switch (field)
        {
            case "Name":
                pizza.Name = value.ToString();
                break;
            case "Price":
                pizza.Price = Convert.ToDecimal(value);
                break;
            case "Description":
                pizza.Description = value.ToString();
                break;
            case "ImageId":
                pizza.ImageId = value is Guid guidValue ? guidValue : throw new InvalidValueException("Value cannot be cast to Guid");
                break;
            case "CaloryCount":
                pizza.CaloryCount = Convert.ToInt32(value);
                break;
            default:
                throw new FieldNotFoundException($"There is no filed like this");
        }
        await UpdateEntityAsync(pizza);
    }
}