using Pizza.Application.Common.Interfaces;
using Pizza.Infrastucture.Data;

namespace Pizza.Infrastucture.Repository;

public class WorkOfUnite : IWorkOfUnite
{
    public IUserRepository UserRepository { get; }
    public IPizzaRepository PizzaRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IAddressRepository AddressRepository { get; }
    public IRankHistoryRepository RankHistoryRepository { get; }

    public WorkOfUnite(ApplicationDbContext context)
    {
        AddressRepository = new AddressRepository(context);
        UserRepository = new UserRepository(context, AddressRepository);
        PizzaRepository = new PizzaRepository(context);
        RankHistoryRepository = new RankHistoryRepository(context);
        OrderRepository = new OrderRepository(context, UserRepository, PizzaRepository, AddressRepository);
    }
}