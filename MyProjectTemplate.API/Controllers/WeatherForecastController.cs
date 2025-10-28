// Controller that returns a small sample weather forecast collection.
// This is a template/example controller—replace or expand when building real endpoints.

using Microsoft.AspNetCore.Mvc;

namespace MyProjectTemplate.API.Controllers
{
    [ApiController]
    [Route("[controller]")] // Route: /WeatherForecast
    public class WeatherForecastController : ControllerBase
    {
        // Sample static summaries used to generate fake data
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // GET /WeatherForecast
        // Returns an array of WeatherForecast objects.
        // To expand: implement paging, caching or replace with real data from a database.
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            // This sample uses Random.Shared for simplicity. In production, inject a time / random provider for testability.
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}