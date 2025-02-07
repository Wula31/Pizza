using Pizza.Domain.Entities;

namespace Pizza.Application.Common.Interfaces;

public interface IAddressRepository: IRepository<Address>
{
    Task UpdateEntityAsync(Address entity);
    Task UpdateFieldAsync<T>(Guid id, string field, T value);

}