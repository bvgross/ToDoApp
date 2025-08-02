using Microsoft.AspNetCore.Mvc;

namespace Tarefas.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TarefaController : ControllerBase
{
    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok("pong");
    }
}