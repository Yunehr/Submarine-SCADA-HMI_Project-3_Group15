using Microsoft.AspNetCore.Mvc;
using MyProjectTemplate.API.Services; // This is used for thresholds


namespace MyProjectTemplate.API.LifeSupportSystems
{
    [ApiController]
    [Route("api/[controller]")]
    public class LifeSupportController : ControllerBase
    {
        private readonly IEventBus _bus;

        public LifeSupportController(IEventBus bus)
        {
            _bus = bus;
        }

        // GET latest reading for a device
        [HttpGet("{deviceType}")]
        public IActionResult GetLatest(DeviceType deviceType)
        {
            if (_bus.TryGetLatest(deviceType, out var reading))
                return Ok(reading);

            return Ok(new { deviceType, value = 1, unit = "N/A" });
        }

        // POST a command (e.g. switch toggles)
        [HttpPost("command")]
        public IActionResult SendCommand([FromBody] DeviceCommand command)
        {
            // For now, just log or forward to device
            Console.WriteLine($"Command received: {command.DeviceType} -> {command.Action}");
            return Ok(new { status = "accepted" });
        }
    }

    public record DeviceCommand(DeviceType DeviceType, string Action);
}
