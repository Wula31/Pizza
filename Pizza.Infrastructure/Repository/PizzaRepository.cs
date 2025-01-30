using Microsoft.EntityFrameworkCore;
using Pizza.Application.Common.Interfaces;
using Pizza.Infrastructure.Data;
using Pizza.Domain.Entities;

namespace Pizza.Infrastucture.Repository;

public class PizzaRepository(ApplicationDbContext context) : Repository<PizzaE>(context), IPizzaRepository
{
    public async Task<PizzaE> GetPizzaByName(string name)
    {
        PizzaE pizza =  await context.Pizzas.FirstOrDefaultAsync(x => x.Name == name) ?? throw new InvalidOperationException("Pizza not found");
        return await Task.FromResult(pizza);
    }
}