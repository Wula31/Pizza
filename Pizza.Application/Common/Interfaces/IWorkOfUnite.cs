namespace Pizza.Application.Common.Interfaces;

public interface IWorkOfUnite
{
    public IUserRepository UserRepository { get; }
    public IPizzaRepository PizzaRepository { get; }
    public IOrderRepository OrderRepository { get; }
    
    public IAddressRepository AddressRepository { get; }
    public IRankHistoryRepository RankHistoryRepository { get; }
}