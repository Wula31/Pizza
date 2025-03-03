using Microsoft.AspNetCore.Mvc;
using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;

namespace Pizza.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    
    public OrderController(IWorkOfUnite workOfUnite)
    {
        _orderRepository = workOfUnite.OrderRepository;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Welcome to the Order API");
    }

    [HttpPost("CreateOrder/{userid}/{addressid}")]
    public async Task<IActionResult> CreateOrder(Guid userid, Guid addressid, [FromBody] List<string> pizzaname, CancellationToken cancellationToken = default)
    {
        await _orderRepository.CreateOrderAsync(userid, addressid, pizzaname, cancellationToken);
        
        return Ok("Order created for user");
    }

    [HttpGet("GetOrder/{orderid}")]
    public async Task<IActionResult> GetOrders(Guid orderid, CancellationToken cancellationToken = default)
    {
        await _orderRepository.GetOrderAsync(orderid, cancellationToken);
        
        return Ok("Order found");
    }

    [HttpGet("GetAllOrder")]
    public async Task<IActionResult> GetAllOrder(CancellationToken cancellationToken = default)
    {
        return Ok(await _orderRepository.GetAllOrdersAsync(cancellationToken));
    }
   
}