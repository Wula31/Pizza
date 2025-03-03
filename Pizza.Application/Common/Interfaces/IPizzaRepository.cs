namespace Pizza.Application.Common.Interfaces;
using Domain.Entities;    

public interface IPizzaRepository : IRepository<PizzaE> 
{
    Task<PizzaE> GetPizzaByName(string name, CancellationToken cancellationToken = default);
    Task UpdateEntityAsync(PizzaE pizza, CancellationToken cancellationToken = default);
    Task UpdateFieldAsync(Guid id, string field, string value, CancellationToken cancellationToken = default);
};