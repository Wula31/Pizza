using Microsoft.EntityFrameworkCore;
using Pizza.Application.Common.Interfaces;
using Pizza.Infrastructure.Data;

namespace Pizza.Infrastucture.Repository;

public class PizzaRepository(ApplicationDbContext context) : IPizzaRepository
{
    
    public async Task<bool> PizzaExistsAsync(int pizzaId)
    {
        return await context
            .Pizzas.AnyAsync(p =>
                p.Id == pizzaId);
    }

    public async Task<bool> PizzaMarkedDeletedAsync(int pizzaId)
    {
        return await context
            .Pizzas.AnyAsync(p =>
                p.Id == pizzaId && p.IsDeleted);
    }
}