using Microsoft.EntityFrameworkCore;
using Pizza.Domain.Entities;

namespace Pizza.Infrastucture.Data;

public class ApplicationDbContext : DbContext
{
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
         
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        ConfigureBaseEntity<User>(modelBuilder);
        ConfigureBaseEntity<PizzaE>(modelBuilder);
        ConfigureBaseEntity<Address>(modelBuilder);
        ConfigureBaseEntity<Image>(modelBuilder);
 
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Addresses)
            .WithOne()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade); 
        
        modelBuilder.Entity<Order>()
            .HasMany(o => o.Pizzas)
            .WithMany()
            .UsingEntity(j => j.ToTable("OrderPizzas"));
    }

    private void ConfigureBaseEntity<T>(ModelBuilder modelBuilder) where T : BaseEntity
    {
        modelBuilder.Entity<T>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<T>()
            .Property(e => e.CreatedOn)
            .HasDefaultValueSql("GETUTCDATE()");
        
        modelBuilder.Entity<T>()
            .Property(e => e.ModifiedOn)
            .ValueGeneratedOnUpdate();
    }

    
    public DbSet<PizzaE> Pizzas { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<RankHistory> RankHistories { get; set; }
    public DbSet<User> Users { get; set; }
    
}