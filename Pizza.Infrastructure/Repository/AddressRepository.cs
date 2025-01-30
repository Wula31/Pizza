using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;
using Pizza.Infrastructure.Data;

namespace Pizza.Infrastucture.Repository;

public class AddressRepository(ApplicationDbContext context) :Repository<Address>(context), IAddressRepository
{
    
}