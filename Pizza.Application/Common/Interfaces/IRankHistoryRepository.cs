using Pizza.Domain.Entities;

namespace Pizza.Application.Common.Interfaces;

public interface IRankHistoryRepository 
{
    Task RankPizza(Guid userId, Guid pizzaId, int rank, CancellationToken cancellationToken = default);

    Task<double> SeeAverage(Guid pizzaId,  CancellationToken cancellationToken = default);
}   