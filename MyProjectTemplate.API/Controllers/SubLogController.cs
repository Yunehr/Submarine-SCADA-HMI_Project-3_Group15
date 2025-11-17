using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProjectTemplate.API.Models;
using MyProjectTemplate.API.Data;

namespace MyProjectTemplate.API.Controllers
{
    [ApiController]
    [Route("[controller]")] // Route: /SubLog
    public class SubLogController : ControllerBase
    {
        private readonly AppDbContext _db;

        public SubLogController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult Create([FromBody] SubLog data)
        {
            _db.SubLogs.Add(data);
            _db.SaveChanges();

            return Ok(data);
        }

        // [HttpGet] // I might leave out "get all logs in the table" because that could cause problems if the table gets massive

        [HttpGet("range")]
        public IActionResult GetRange([FromQuery] string start, [FromQuery] string end) // Remember we have to store time in this formate: 2025-01-17 14:30:00
        {
            var logs = _db.SubLogs
                .Where(log =>
                    string.Compare(log.TimeData, start) >= 0 &&
                    string.Compare(log.TimeData, end) <= 0)
                .ToList();

            return Ok(logs);
        }

        [HttpGet("{id}")] // Get a single log if needed
        public IActionResult GetLog(int id)
        {
            var log = _db.SubLogs.Find(id);
            
            if (log == null) 
                return NotFound();

            return Ok(log);
        }
    }
}