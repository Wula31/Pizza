using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;
using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;
using Pizza.Infrastructure.Data;

namespace Pizza.Infrastucture.Repository;


public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> DbSet = context.Set<T>();

    public virtual async Task CreateEntityAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        await context.SaveChangesAsync();
    }
    public virtual async Task DeleteEntityAsync(int id)
    {
        var entity = await GetEntityAsync(id);
        entity.IsDeleted = true; 
        entity.ModifiedOn = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }
    public async Task<T> GetEntityAsync(int id)
    {
        var entity = await DbSet.FindAsync(id);
        if (entity == null || entity.IsDeleted)
            throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with id {id} not found.");
        return entity;
    }
    async Task<IEnumerable<T>> IRepository<T>.GetAllEntitiesAsync()
    {
        return await DbSet.Where(x => !x.IsDeleted).ToListAsync();
    }

    async Task IRepository<T>.UpdateEntityAsync(T entity)
    {
        if(entity == null || entity.IsDeleted)
            throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with id {entity.Id} not found.");
        DbSet.Update(entity);
        await context.SaveChangesAsync();
    }
    
    async Task IRepository<T>.UpdateFieldAsync(int id, object updates)
    {
        var entity = await GetEntityAsync(id);
        var type = entity.GetType();
        if(entity == null || entity.IsDeleted)
            throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with id {entity.Id} not found.");
        foreach (var prop in updates.GetType().GetProperties())
        {
            var entityProp = type.GetProperty(prop.Name);
            var newValues = prop.GetValue(updates);

            if (entityProp != null && newValues != null && entityProp.CanWrite)
                entityProp.SetValue(entity, newValues);
        }
        entity.ModifiedOn = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }

    public Task<bool> EntityExistsAsync(int Id)
    {
        
        return DbSet.AnyAsync(p => p.Id == Id && !p.IsDeleted); 
    }

    public Task<bool> EntityMarkedDeletedAsync(int Id)
    {
        return DbSet.AnyAsync(p => p.Id == Id && p.IsDeleted);
    }
}