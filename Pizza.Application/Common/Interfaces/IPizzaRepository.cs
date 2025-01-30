namespace Pizza.Application.Common.Interfaces;
using Domain.Entities;    

public interface IPizzaRepository : IRepository<PizzaE>
{
    Task<PizzaE?> GetPizzaByName(string name);
};