using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VersionedApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersionNeutral] // applies to everything in the controller:
                    // no matter what version you ask for, you get the same controller
public class HealthController : ControllerBase
{
    [HttpGet]
    [Route("ping")]
    public IActionResult Ping()
    {
        return Ok("Pong");
    }

}