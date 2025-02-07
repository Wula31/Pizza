namespace Pizza.Domain.Entities;

public class RankHistory  
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int Rank { get; set; }
    public Guid PizzaId { get; set; }
}