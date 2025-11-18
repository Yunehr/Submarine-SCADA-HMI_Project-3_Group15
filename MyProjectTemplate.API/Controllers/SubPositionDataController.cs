using Microsoft.AspNetCore.Mvc;
using MyProjectTemplate.API.Data;
using MyProjectTemplate.API.Models;

namespace MyProjectTemplate.API.Controllers
{
    [ApiController]
    [Route("[controller]")] // Route: /SubPositionData

    public class SubPositionDataController : Controller
    {
        private readonly AppDbContext _db;

        public SubPositionDataController(AppDbContext db)
        {
            _db = db;
        }


        [HttpPost] // POST /SubPositionData
        public IActionResult Create([FromBody] SubPositionDatum data)
        {
            _db.SubPositionData.Add(data);
            _db.SaveChanges();

            return Ok(data);
        }
    }
}
