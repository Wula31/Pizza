using Microsoft.AspNetCore.Mvc;
using Pizza.Application.Common.Interfaces;

namespace Pizza.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class RankController : ControllerBase
{
    IRankHistoryRepository _rankHistoryRepository;
    
    public RankController(IWorkOfUnite workOfUnite)
    {
        _rankHistoryRepository =  workOfUnite.RankHistoryRepository;
    }

    [HttpPost("RankPizza/{userid}/{pizzaid}")]
    public async Task<IActionResult> RankPizza(Guid userid, Guid pizzaid, int rank, CancellationToken cancellation = default)
    {
        await _rankHistoryRepository.RankPizza(userid, pizzaid, rank, cancellation);
        
        return Ok("Pizza ranker");
    }

    [HttpGet("GetAverage/{pizzaid}")]
    public async Task<IActionResult> GetAverage(Guid pizzaid, CancellationToken cancellation = default)
    {
        var average = await _rankHistoryRepository.SeeAverage(pizzaid, cancellation);

        return Ok($"rank of the is {average}");
    }
}