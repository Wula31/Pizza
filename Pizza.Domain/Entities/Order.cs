namespace Pizza.Domain.Entities;

public class Order : BaseEntity
{
    public required Guid UserId { get; set; }
    public required Guid AddressId { get; set; }
    public virtual required List<PizzaE> Pizzas { get; set; }
}