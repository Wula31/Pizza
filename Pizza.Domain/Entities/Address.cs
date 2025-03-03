namespace Pizza.Domain.Entities;

public class Address : BaseEntity
{
    public required string City { get; set; }
    public required string Country { get; set; }
    public required string Region { get; set; }
    public required string Description { get; set; }
    
    public required Guid UserId { get; set; }
}
