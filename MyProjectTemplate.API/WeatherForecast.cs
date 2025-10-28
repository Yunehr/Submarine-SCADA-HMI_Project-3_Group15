// Simple POCO representing weather data returned by the sample controller.
// Replace or extend this model with real business properties as needed.

namespace MyProjectTemplate.API
{
    public class WeatherForecast
    {
        // The date for the forecast. Uses DateOnly (C# 10 / .NET 6+) for date-only values.
        public DateOnly Date { get; set; }

        // Temperature in Celsius.
        public int TemperatureC { get; set; }

        // Computed temperature in Fahrenheit.
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        // Optional textual summary.
        public string? Summary { get; set; }
    }
}