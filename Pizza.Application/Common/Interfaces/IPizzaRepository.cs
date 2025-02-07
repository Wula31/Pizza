namespace Pizza.Application.Common.Interfaces;
using Domain.Entities;    

public interface IPizzaRepository : IRepository<PizzaE> 
{
    Task<PizzaE> GetPizzaByName(string name);
    Task UpdateEntityAsync(PizzaE entity);
    Task UpdateFieldAsync<T>(Guid id, string field, T value);
};