namespace Pizza.Domain.Entities;

public class Order : BaseEntity
{
    public required int UserId { get; set; }
    public required int AddressId { get; set; }
    public required List<Pizza> Pizzas { get; set; }
}