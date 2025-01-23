namespace Pizza.Application.Common.Interfaces;

public interface IAllRepository 
{
    public IUserRepository UserRepository { get; }
    public IPizzaRepository PizzaRepository { get; }
}