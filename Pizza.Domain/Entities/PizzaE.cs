namespace Pizza.Domain.Entities;

public class PizzaE : BaseEntity
{   
    public required string Name { get; set; }  
    public required decimal Price { get; set; }
    public Guid? ImageId { get; set; }
    public required string Description { get; set; }
    public required int CaloryCount { get; set; }
}