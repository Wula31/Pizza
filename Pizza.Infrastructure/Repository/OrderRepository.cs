    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Pizza.Application.Common.Interfaces;
    using Pizza.Domain.Entities;
    using Pizza.Infrastucture.Data;
    using Pizza.Infrastucture.Exceptions;

    namespace Pizza.Infrastucture.Repository;

    public class OrderRepository(
        ApplicationDbContext context,
        IUserRepository userRepository,
        IPizzaRepository pizzaRepository,
        IAddressRepository addressRepository)
        : IOrderRepository
    {
        public async Task CreateOrderAsync(Guid userId, Guid addressId, List<string> pizzas, CancellationToken cancellationToken = default)
        {
            User user = await userRepository.GetEntityAsync(userId ,cancellationToken);
    
            Address address = await addressRepository.GetEntityAsync(addressId, cancellationToken);
    
            if(user == null) throw new UserNotFoundException("User not found");
            if(address == null) throw new AddressNotFoundException("address not found");
            if(address.UserId != userId) throw new UserAndAddressNotRelated("User and address are not related");
            if(user.IsDeleted) throw new UserDeletedException("User is deleted");
            if(address.IsDeleted) throw new AddressDeletedException("Address is deleted");
    
            Order order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                AddressId = addressId,
                Pizzas = new List<PizzaE>(),
                CreatedOn = DateTime.UtcNow
            };
    
            foreach (var pizza in pizzas)
            {
                PizzaE pizzaE = await pizzaRepository.GetPizzaByName(pizza, cancellationToken);
                if (pizzaE == null)
                {
                    throw new PizzaNotFoundException("Pizza not found");
                }
                
                order.Pizzas.Add(pizzaE);
            }

            await context.Set<Order>().AddAsync(order, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
        
        public async Task<Order> GetOrderAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var order = await context.Set<Order>()
                .Include(x => x.Pizzas)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (order == null)
                throw new EntityNotFoundException($"Entity of type order with id {id} not found.");
            return order;
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken  cancellationToken = default)
        {
            return await context.Set<Order>()
                .Include(x => x.Pizzas)
                .ToListAsync(cancellationToken);
        }
        
    }