using Pizza.Domain.Entities;
namespace Pizza.Application.Common.Interfaces;
    
public interface IUserRepository : IRepository<User>
{
    Task UpdateEntityAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateFieldAsync(Guid id, string field, string value, CancellationToken cancellationToken = default);

}