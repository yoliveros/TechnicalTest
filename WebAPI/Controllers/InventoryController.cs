using Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class InventoryController(IMediator mediator) : ControllerBase
{

    [HttpGet("InventoryTotalByCategory")]
    public async Task<IActionResult> GetTotalInventory()
    {
        var query = new GetInventoryTotalQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }
}
