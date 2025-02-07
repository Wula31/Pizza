using Pizza.Application.Common.Interfaces;
using Pizza.Infrastructure.Data;

namespace Pizza.Infrastucture.Repository;

public class WorkOfUnite : IWorkOfUnite
{
    public IUserRepository UserRepository { get; }
    public IPizzaRepository PizzaRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IAddressRepository AddressRepository { get; }
    public RankHistoryRepository RankHistoryRepository { get; }

    public WorkOfUnite(ApplicationDbContext context)
    {
        UserRepository = new UserRepository(context);
        PizzaRepository = new PizzaRepository(context);
        AddressRepository = new AddressRepository(context);
        RankHistoryRepository = new RankHistoryRepository(context);
        OrderRepository = new OrderRepository(context, UserRepository, PizzaRepository, AddressRepository);
    }
}