using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;
using Pizza.Infrastructure.Data;
using Pizza.Infrastucture.Exceptions;

namespace Pizza.Infrastucture.Repository;

public class OrderRepository(
    ApplicationDbContext context,
    IUserRepository userRepository,
    IPizzaRepository pizzaRepository,
    IAddressRepository addressRepository)
    : IOrderRepository
{
    public async Task CreateOrderAsync(Guid userId, Guid addressId, List<string> pizzas)
    {
        User user = await userRepository.GetEntityAsync(userId);
        
        Address address = await addressRepository.GetEntityAsync(addressId);
        
        if(user == null) throw new UserNotFoundException("User not found");
        if(address == null) throw new AddressNotFoundException("address not found");
        if(address.UserId != userId) throw new UserAndAddressNotRelated("User and address are not related");
        List<PizzaE> pizzaList = new List<PizzaE>();
        
        foreach (var pizza in pizzas)
        {
            PizzaE pizzaE = await pizzaRepository.GetPizzaByName(pizza);
            pizzaList.Add(pizzaE);
        }

        Order order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            AddressId = addressId,
            Pizzas = pizzaList,
            IsDeleted = false,
            CreatedOn = DateTime.UtcNow
        };

        await context.Set<Order>().AddAsync(order);
        await context.SaveChangesAsync();
    }
}