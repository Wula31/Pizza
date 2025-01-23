using Microsoft.EntityFrameworkCore;
namespace Pizza.Infrastructure.Data;

using Domain.Entities;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
         
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach(var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity)
                .IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.
                    Entity(entityType.ClrType).
                    Property(nameof(BaseEntity.Id))
                    .ValueGeneratedOnAdd();
                
                modelBuilder.
                    Entity(entityType.ClrType).
                    Property(nameof(BaseEntity.CreatedOn))
                    .HasDefaultValueSql("GETUTCDATE()");

                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(BaseEntity.ModifiedOn))
                    .ValueGeneratedOnUpdate();
            }
            base.OnModelCreating(modelBuilder);
        }
    }
    
    public DbSet<Pizza> Pizzas { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<User> Users { get; set; }
    
}