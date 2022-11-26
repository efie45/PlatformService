using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;

[Route("/api/commands/platforms")]
[ApiController]
public class PlatformsController : ControllerBase
{
    [HttpPost]
    public ActionResult TestInboundConnection()
    {
        Console.WriteLine("--> Inbound POST at the command service");
        return Ok("Inbound POST test okay from platforms controller in command service.");
    }
}

