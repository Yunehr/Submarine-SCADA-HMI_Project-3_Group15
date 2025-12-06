using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyProjectTemplate.API.Data;
using MyProjectTemplate.API.LifeSupportSystems;
using MyProjectTemplate.API.Models;
using MyProjectTemplate.API.Services;

namespace MyProjectTemplate.API.Controllers 
{
    [ApiController]
    [Route("[controller]")] // Route: /SubLog
    public class SubLogController : ControllerBase // This is still needed as a specific controller as logs are a seperate domain/thing from the devices like alarms and thangs
    {
        private readonly AppDbContext _db;
        private readonly Logger _logger;
        private readonly DeviceThresholds _thresh;
        private readonly DeviceLoggingService _loggingService;


        private readonly List<IDisposable> subs = new();

        public SubLogController(AppDbContext db, 
                                Logger logger, 
                                IOptions<DeviceThresholds> thresholds,
                                DeviceLoggingService loggingService) {
            _db = db;
            _logger = logger;
            _thresh = thresholds.Value;
            _loggingService = loggingService;
        }


        [HttpPost("processReading")]
        public IActionResult ProcessReading([FromBody] DeviceReading r)
        {
            _loggingService.HandleReading(r);
            return Ok();
        }

        [HttpGet("logRange")]
        public IActionResult GetLogRange([FromQuery] string start, [FromQuery] string end) // Remember we have to store time in this formate: 2025-01-17 14:30:00
        {
            var logs = _db.SubLogs
                .Where(log =>
                    string.Compare(log.TimeData, start) >= 0 &&
                    string.Compare(log.TimeData, end) <= 0)
                .ToList();

            return Ok(logs);
        }

        [HttpGet("log/{id}")] // Get a single log if needed
        public IActionResult GetLog(int id)
        {
            var log = _db.SubLogs.Find(id);

            if (log == null)
                return NotFound();

            return Ok(log);
        }
    }
}
