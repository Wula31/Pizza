using Pizza.Domain.Entities;
namespace Pizza.Application.Common.Interfaces;
    
public interface IUserRepository : IRepository<User>
{
    Task UpdateEntityAsync(User entity);
    Task UpdateFieldAsync<T>(Guid id, string field, T value);

}