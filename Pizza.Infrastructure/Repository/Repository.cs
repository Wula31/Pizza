using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;
using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;
using Pizza.Infrastucture.Data;
using Pizza.Infrastucture.Exceptions;

namespace Pizza.Infrastucture.Repository;


public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;

    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task CreateEntityAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteEntityAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetEntityAsync(id, cancellationToken);
        entity.IsDeleted = true; 
        entity.ModifiedOn = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<T> GetEntityAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
        if (entity == null || entity.IsDeleted)
            throw new EntityNotFoundException($"Entity of type {typeof(T).Name} with id {id} not found.");
        return entity;
    }

    public async Task<IEnumerable<T>> GetAllEntitiesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().Where(x => !x.IsDeleted).ToListAsync(cancellationToken);
    }

    public Task<bool> EntityExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbSet.AnyAsync(p => p.Id.Equals(id) && !p.IsDeleted, cancellationToken);
    }

    public Task<bool> EntityMarkedDeletedAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbSet.AnyAsync(p => p.Id.Equals(id) && p.IsDeleted, cancellationToken);
    }
}