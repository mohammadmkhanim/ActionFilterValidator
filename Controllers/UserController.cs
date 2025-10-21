using Microsoft.AspNetCore.Mvc;

namespace ActionFilterValidator.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] CreateUserDto user)
    {
        // validation will execute by the action filter automatically

        return Ok();
    }
}