namespace Pizza.Domain.Entities;

public class Image : BaseEntity
{
    public required string PizzaId { get; set; }
    public required string OriginalName { get; set; }
    public required string GeneratedName { get; set; }
}