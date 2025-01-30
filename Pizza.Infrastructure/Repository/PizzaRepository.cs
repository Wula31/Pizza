using Microsoft.EntityFrameworkCore;
using Pizza.Application.Common.Interfaces;
using Pizza.Infrastructure.Data;

namespace Pizza.Infrastucture.Repository;

public class PizzaRepository(ApplicationDbContext context) : Repository<Pizza.Domain.Entities.Pizza>(context), IPizzaRepository
{
    private readonly ApplicationDbContext _context = context;

    
    
}