namespace Pizza.Domain.Entities;

public class Pizza
{
    public required int Id { get; set; }
    public required string Name { get; set; }  
    public required decimal Price { get; set; }
    public int? ImageId { get; set; }
    public required string Description { get; set; }
    public required int CaloryCount { get; set; }
}