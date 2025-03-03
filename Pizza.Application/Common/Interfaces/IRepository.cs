    namespace Pizza.Application.Common.Interfaces;

    public interface IRepository<T> where T : class
    {
        Task CreateEntityAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteEntityAsync(Guid id, CancellationToken cancellationToken = default);
        Task<T> GetEntityAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllEntitiesAsync(CancellationToken cancellationToken = default);
        Task<bool> EntityExistsAsync(Guid pizzaId, CancellationToken cancellationToken = default);
        Task<bool> EntityMarkedDeletedAsync(Guid pizzaId, CancellationToken cancellationToken = default);

        }