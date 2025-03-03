using Pizza.Domain.Entities;

namespace Pizza.Application.Common.Interfaces;

public interface IAddressRepository: IRepository<Address>
{
    Task UpdateEntityAsync(Address address, CancellationToken cancellationToken = default);
    Task UpdateFieldAsync(Guid id, string field, string value, CancellationToken cancellationToken = default);

}