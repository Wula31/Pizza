namespace Pizza.Application.Common.Interfaces;

public interface IRepository<T> where T : class
{
    Task CreateEntityAsync(T entity);
    Task DeleteEntityAsync(int id);
    Task<T> GetEntityAsync(int id);
    Task<IEnumerable<T>> GetAllEntitiesAsync();

    Task UpdateEntityAsync(T entity);
    Task UpdateFieldAsync(int id, object updates);
    
    Task<bool> EntityExistsAsync(int pizzaId);
    
    Task<bool> EntityMarkedDeletedAsync(int pizzaId);

}