using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProjectTemplate.API.Models;
using MyProjectTemplate.API.Data;

namespace MyProjectTemplate.API.Controllers
{
    [ApiController]
    [Route("[controller]")] // Route: /SubControlData
    public class SubControlDataController : ControllerBase
    {
        private readonly AppDbContext _db;

        public SubControlDataController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost] // POST /SubControlData
        public IActionResult Create([FromBody] SubControlDatum data)
        {
            _db.SubControlData.Add(data); 
            _db.SaveChanges();

            return Ok(data);
        }
    }
}