using jiraF.Goal.API.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace jiraF.Goal.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [ApiKeyAuthorize]
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [ApiKeyAuthorize(Disabled = true)]
    [HttpGet("{value}")]
    public IActionResult GetByValue(int value)
    {
        return Ok();
    }
}
