using Microsoft.EntityFrameworkCore;
using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;
using Pizza.Infrastructure.Data;
using Pizza.Infrastucture.Exceptions;

namespace Pizza.Infrastucture.Repository;

public class RankHistoryRepository(ApplicationDbContext context) : IRankHistoryRepository
{
    private readonly ApplicationDbContext _context = context;
    
    public async Task RankPizza(Guid userId, Guid pizzaId, int rank)
    {
        bool hasOrderd = await _context.Orders.AnyAsync(o => o.UserId == userId && o.Pizzas.Any(p => p.Id == pizzaId));
        if (!hasOrderd) throw new UserHasNotOrderPizzaException("User has not ordered this pizza");
        RankHistory rankHistory = new RankHistory
        {
            Id = Guid.NewGuid(),
            Rank = rank,
            UserId = userId,
            PizzaId = pizzaId,
        };

        await _context.Set<RankHistory>().AddAsync(rankHistory);
        await _context.SaveChangesAsync();
    }

    public async Task<double> SeeAverage(Guid pizzaId)
    {
        return await _context.Set<RankHistory>().Where(x => x.PizzaId == pizzaId).AverageAsync(x => x.Rank);
    }
}