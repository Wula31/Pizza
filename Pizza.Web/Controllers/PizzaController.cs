using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Mvc;
using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;

namespace Pizza.Web.Controllers;
[ApiController]
[Route("[controller]")]
public class PizzaController: ControllerBase
{
    private readonly IPizzaRepository _pizzaRepository;
    
    public PizzaController(IWorkOfUnite workOfUnite)
    {
        _pizzaRepository = workOfUnite.PizzaRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Welcome to the Pizza API");
    }


    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] PizzaE pizza)
    {
        await _pizzaRepository.CreateEntityAsync(pizza);
        
        return Ok("Created");
    }

    [HttpPost("Get/{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
       PizzaE pizza = await _pizzaRepository.GetEntityAsync(id);
       
       return Ok(pizza.Name);
    }
    
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _pizzaRepository.GetAllEntitiesAsync());
    }
    
    [HttpPut("Update/{id}/{name}/{value}")]
    public async Task<IActionResult> Update(Guid id, string name, string value)
    {
        await _pizzaRepository.UpdateFieldAsync(id, name, value);
        
        return Ok($"Pizza updated");
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _pizzaRepository.DeleteEntityAsync(id);
        
        return Ok("Pizza deleted");
    }

    [HttpPost("Exist/{id}")]
    public async Task<IActionResult> Exist(Guid id, CancellationToken cancellationToken = default)
    {
        bool exist = await _pizzaRepository.EntityExistsAsync(id, cancellationToken);
        
        return Ok(exist);
    }

    [HttpPost("IsDeleted")]
    public async Task<IActionResult> IsDeleted(Guid id, CancellationToken cancellationToken)
    {
        bool isDeleted = await _pizzaRepository.EntityExistsAsync(id, cancellationToken);
        
        return Ok(isDeleted);
    }
    
    [HttpGet("Search/{name}")]
    public async Task<IActionResult> GetPizzas(string name, CancellationToken cancellationToken = default)
    {
        PizzaE pizza = await _pizzaRepository.GetPizzaByName(name, cancellationToken);
        
        return Ok($"{pizza.Name} pizza was found and it costs {pizza.Price}");
    }
}
