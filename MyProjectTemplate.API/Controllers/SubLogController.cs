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

        [HttpPost] // POST /SubLog
        public IActionResult Create([FromBody] SubLog data)
        {
            _db.SubLogs.Add(data);
            _db.SaveChanges();

            return Ok(data);
        }
    }
}