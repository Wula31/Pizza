namespace Pizza.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public required int UserId { get; set; }
    public required int AddressId { get; set; }
    public required List<Pizza> Pizzas { get; set; }
}