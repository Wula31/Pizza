using Microsoft.EntityFrameworkCore;
using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;
using Pizza.Infrastucture.Data;
using Pizza.Infrastucture.Exceptions;

namespace Pizza.Infrastucture.Repository;

public class RankHistoryRepository(ApplicationDbContext context) : IRankHistoryRepository
{
    private readonly ApplicationDbContext _context = context;
    
    public async Task RankPizza(Guid userId, Guid pizzaId, int rank, CancellationToken cancellationToken = default)
    {
        bool ordered = await _context.Orders.AnyAsync(o => (o.UserId == userId) && o.Pizzas.Any(p => p.Id == pizzaId && !p.IsDeleted),cancellationToken);
        if (!ordered) throw new UserHasNotOrderPizzaException("User has not ordered this pizza");
        RankHistory rankHistory = new RankHistory
        {
            Id = Guid.NewGuid(),
            Rank = rank,
            UserId = userId,
            PizzaId = pizzaId,
        };

        await _context.Set<RankHistory>().AddAsync(rankHistory, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<double> SeeAverage(Guid pizzaId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<RankHistory>().Where(x => x.PizzaId == pizzaId).AverageAsync(x => x.Rank,cancellationToken);
    }
}