using System.Globalization;
using Pizza.Domain.Entities;

namespace Pizza.Application.Common.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task CreateOrder(int UserId, int AddressId,List<string> Pizzas);
}