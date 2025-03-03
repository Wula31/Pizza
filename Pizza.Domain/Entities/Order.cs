namespace Pizza.Domain.Entities;

public sealed class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required Guid UserId { get; set; }
    public required Guid AddressId { get; set; }
    public required List<PizzaE> Pizzas { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

}