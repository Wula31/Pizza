    namespace Pizza.Application.Common.Interfaces;

    public interface IRepository<T> where T : class
    {
        Task CreateEntityAsync(T entity);
        Task DeleteEntityAsync(Guid id);
        Task<T> GetEntityAsync(Guid id);
        Task<IEnumerable<T>> GetAllEntitiesAsync();
        Task<bool> EntityExistsAsync(Guid pizzaId);
        Task<bool> EntityMarkedDeletedAsync(Guid pizzaId);

        }