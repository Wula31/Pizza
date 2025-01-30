using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;
using Pizza.Infrastructure.Data;

namespace Pizza.Infrastucture.Repository;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    
    private readonly ApplicationDbContext context;
    private readonly IUserRepository userRepository;
    private readonly IPizzaRepository pizzaRepository;
    private readonly IAddressRepository addressRepository;
    
    public OrderRepository(ApplicationDbContext _context, IUserRepository _userRepository, IPizzaRepository _pizzaRepository, IAddressRepository _addressRepository): base(_context)
    {
        context = _context;
        userRepository = _userRepository;
        pizzaRepository = _pizzaRepository;
        addressRepository = _addressRepository;
    }
    
    public async Task CreateOrder(int UserId, int AddressId, List<string> Pizzas)
    {
        return;
    }
}