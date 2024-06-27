using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace BuberDiner.Api.Controllers;

[Route("[Controller]")]
public class DinerController : ApiController
{
    [HttpGet("dinners")]
    public IActionResult ListDinner()
    {
        return Ok(Array.Empty<String>());
    }
}
