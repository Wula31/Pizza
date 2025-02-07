using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;
using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;
using Pizza.Infrastructure.Data;
using Pizza.Infrastucture.Exceptions;

namespace Pizza.Infrastucture.Repository;


public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public virtual async Task CreateEntityAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
    }
    public virtual async Task DeleteEntityAsync(Guid id)
    {
        var entity = await GetEntityAsync(id);
        entity.IsDeleted = true; 
        entity.ModifiedOn = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }
    public async Task<T> GetEntityAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null || entity.IsDeleted)
            throw new EntityNotFoundException($"Entity of type {typeof(T).Name} with id {id} not found.");
        return entity;
    }
    public async Task<IEnumerable<T>> GetAllEntitiesAsync()
    {
        return await _dbSet.Where(x => !x.IsDeleted).ToListAsync();
    }
    public Task<bool> EntityExistsAsync(Guid id)
    {
        return _dbSet.AnyAsync(p => p.Id.Equals(id) && !p.IsDeleted); 
    }

    public Task<bool> EntityMarkedDeletedAsync(Guid id)
    {
        return _dbSet.AnyAsync(p => p.Id.Equals(id) && p.IsDeleted);
    }
}