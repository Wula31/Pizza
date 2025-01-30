using Pizza.Application.Common.Interfaces;
using Pizza.Infrastructure.Data;

namespace Pizza.Infrastucture.Repository;

public class AllRepository : IAllRepository
{
    private readonly ApplicationDbContext _context;
    public IUserRepository UserRepository { get; }
    public IPizzaRepository PizzaRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IAddressRepository AddressRepository { get; }

    public AllRepository(ApplicationDbContext context)
    {
        _context = context;
        UserRepository = new UserRepository(_context);
        PizzaRepository = new PizzaRepository(_context);
        AddressRepository = new AddressRepository(_context);
        OrderRepository = new OrderRepository(_context, UserRepository, PizzaRepository, AddressRepository);
    }
}