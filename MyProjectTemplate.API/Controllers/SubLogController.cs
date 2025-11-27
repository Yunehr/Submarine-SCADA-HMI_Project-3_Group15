using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

// using Microsoft.Extensions.Logging; could maybe use this

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

        private readonly List<IDisposable> subs = new();

        public SubLogController(AppDbContext db, Logger logger, IOptions<DeviceThresholds> thresholds) {
            _db = db;
            _logger = logger;
            _thresh = thresholds.Value;
        }


        [HttpPost("processReading")]
        public IActionResult ProcessReading([FromBody] DeviceReading r) {
            
            ThresholdSet? t = r.DeviceType switch {
                DeviceType.Oxygen => _thresh.Oxygen,
                DeviceType.Humidity => _thresh.Humidity,
                DeviceType.CO2 => _thresh.CO2,
                DeviceType.Pressure => _thresh.Pressure,
                DeviceType.AirReserve => _thresh.AirReserve,
                _ => null
            };

            if (t == null)
                return Ok(); // Unknown device type

            // Each of the below will first check if the device has a threshold level, then if one was found, if the reading is less/greater than that
            // VERY LOW
            if (t.VeryLow is double vLow && r.Value < vLow)
                _logger.Danger(r.DeviceId, $"{r.DeviceType} VERY low: {r.Value}{r.Unit}");

            // LOW
            else if (t.Low is double low && r.Value < low)
                _logger.Warning(r.DeviceId, $"{r.DeviceType} low: {r.Value}{r.Unit}");

            // HIGH
            else if (t.High is double high && r.Value > high)
                _logger.Danger(r.DeviceId, $"{r.DeviceType} high: {r.Value}{r.Unit}");

            // VERY HIGH
            else if (t.VeryHigh is double vHigh && r.Value > vHigh)
                _logger.Warning(r.DeviceId, $"{r.DeviceType} VERY high: {r.Value}{r.Unit}");

            return Ok();
        }


        [HttpPost("log")]
        public IActionResult AddLog([FromBody] SubLog data)
        {
            _db.SubLogs.Add(data);
            _db.SaveChanges();

            return Ok(data);
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
