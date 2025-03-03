using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;


namespace Pizza.Web.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IWorkOfUnite workOfUnite)
    {
        _userRepository = workOfUnite.UserRepository;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Welcome to the User API");
    }


    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] User user, CancellationToken cancellationToken)
    {
        await _userRepository.CreateEntityAsync(user, cancellationToken);
        
        return Ok("Created");
    }

    [HttpPost("Get/{id}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetEntityAsync(id, cancellationToken);
       
        return Ok(user.FirstName + " " + user.LastName);
    }
    
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _userRepository.GetAllEntitiesAsync(cancellationToken));
    }
    
    [HttpPut("Update/{id}/{name}")]
    public async Task<IActionResult> Update(Guid id, string name,[FromBody] string value, CancellationToken cancellationToken)
    {

        await _userRepository.UpdateFieldAsync(id, name, value, cancellationToken);

        return Ok("User updated");
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteEntityAsync(id, cancellationToken);
        
        return Ok("User deleted");
    }

    [HttpPost("Exist/{id}")]
    public async Task<IActionResult> Exist(Guid id, CancellationToken cancellationToken)
    {
        bool exist = await _userRepository.EntityExistsAsync(id, cancellationToken);
        
        return Ok(exist);
    }

    [HttpPost("IsDeleted")]
    public async Task<IActionResult> IsDeleted(Guid id, CancellationToken cancellationToken)
    {
        bool isDeleted = await _userRepository.EntityExistsAsync(id, cancellationToken);
        
        return Ok(isDeleted);
    }
}