namespace Pizza.Domain.Entities;

public class Image : BaseEntity
{
    public required Guid PizzaId { get; set; }
    public required string OriginalName { get; set; }
    public required string Path { get; set; }
}