namespace Pizza.Domain.Entities;

public class User : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required int PhoneNumber { get; set; }
    public required string[] Address {get; set;}
}