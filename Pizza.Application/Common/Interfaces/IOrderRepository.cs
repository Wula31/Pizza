using System.Globalization;
using Pizza.Domain.Entities;

namespace Pizza.Application.Common.Interfaces;

public interface IOrderRepository 
{
    Task CreateOrderAsync(Guid userId, Guid addressId, List<string> pizzas, CancellationToken cancellationToken = default);
    Task<Order> GetOrderAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default);

}