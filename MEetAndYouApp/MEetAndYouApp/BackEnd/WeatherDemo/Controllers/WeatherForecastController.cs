using Microsoft.AspNetCore.Mvc;

namespace WeatherDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet]
    [Route("Short")]
    public Object Get1()
    {
        var student = new { Name = "Joshua", Age = 42 };
        return student;
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

