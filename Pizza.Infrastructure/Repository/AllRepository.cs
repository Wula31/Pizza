using Pizza.Application.Common.Interfaces;
using Pizza.Infrastructure.Data;

namespace Pizza.Infrastucture.Repository;

public class AllRepository : IAllRepository
{
    private readonly ApplicationDbContext _context;
    public IUserRepository UserRepository { get; private set;  }
    public IPizzaRepository PizzaRepository { get; }

    public AllRepository(ApplicationDbContext context)
    {
        _context = context;
        UserRepository = new UserRepository(_context);
        PizzaRepository = new PizzaRepository(_context);
    }
}