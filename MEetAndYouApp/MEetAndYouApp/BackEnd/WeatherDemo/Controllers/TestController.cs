using Microsoft.AspNetCore.Mvc;

namespace Pentaskilled.MEetAndYou.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("Vehicle")]
    public Object Get1()
    {
        var car = new { Model = "Honda", Year = 1999 };
        return car;
    }

    //[HttpGet(Name = "CalculateFibonachi")]
    //public Object GetStudent()
    //{
    //    var student = new { Name = "Joshua", Age = 42};
    //    return student;
    //}

    [HttpPost]
    [Route("Hello")]
    public void Hello()
    {

    }
}

