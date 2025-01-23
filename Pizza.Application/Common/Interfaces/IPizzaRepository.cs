namespace Pizza.Application.Common.Interfaces;

public interface IPizzaRepository
{
    Task<bool> PizzaExistsAsync(int pizzaId);
    
    Task<bool> PizzaMarkedDeletedAsync(int pizzaId);

};