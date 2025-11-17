using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProjectTemplate.API.Models;
using MyProjectTemplate.API.Data;

namespace MyProjectTemplate.API.Controllers
{
    [ApiController]
    [Route("[controller]")] // Route: /SubAlarmsData
    public class SubAlarmsDataController : ControllerBase
    {
        private readonly AppDbContext _db;

        public SubAlarmsDataController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost] // POST /SubAlarmsData
        public IActionResult Create([FromBody] SubAlarmsDatum data)
        {
            _db.SubAlarmsData.Add(data);
            _db.SaveChanges();

            return Ok(data);
        }
    }
}