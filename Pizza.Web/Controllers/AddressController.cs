using Microsoft.AspNetCore.Mvc;
using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;

namespace Pizza.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressRepository _addressRepository;
    

    public AddressController(IWorkOfUnite workOfUnite)
    {
        _addressRepository = workOfUnite.AddressRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Welcome to the Address API");
    }


    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] Address address, CancellationToken cancellationToken = default)
    {
        await _addressRepository.CreateEntityAsync(address, cancellationToken);
        
        return Ok("Created");
    }

    [HttpPost("Get/{id}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
    {
        await _addressRepository.GetEntityAsync(id, cancellationToken);
       
        return Ok("Address found");
    }
    
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        return Ok(await _addressRepository.GetAllEntitiesAsync(cancellationToken));
    }
    
    [HttpPut("Update/{id}/{name}")]
    public async Task<IActionResult> Update(Guid id, string name,[FromBody] string value, CancellationToken cancellationToken = default)
    {

        await _addressRepository.UpdateFieldAsync(id, name, value, cancellationToken);

        return Ok("Address updated");
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        await _addressRepository.DeleteEntityAsync(id, cancellationToken);
        
        return Ok("Address deleted");
    }

    [HttpPost("Exist/{id}")]
    public async Task<IActionResult> Exist(Guid id, CancellationToken cancellationToken = default)
    {
        bool exist = await _addressRepository.EntityExistsAsync(id, cancellationToken);
        
        return Ok(exist);
    }

    [HttpPost("IsDeleted")]
    public async Task<IActionResult> IsDeleted(Guid id, CancellationToken cancellationToken = default)
    {
        bool isDeleted = await _addressRepository.EntityExistsAsync(id, cancellationToken);
        
        return Ok(isDeleted);
    }
}