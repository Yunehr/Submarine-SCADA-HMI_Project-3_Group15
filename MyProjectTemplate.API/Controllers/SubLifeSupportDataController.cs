using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProjectTemplate.API.Models;
using MyProjectTemplate.API.Data;

namespace MyProjectTemplate.API.Controllers
{
    [ApiController] 
    [Route("[controller]")] // Route: /SublifeSupportData
    public class SubLifeSupportDataController : Controller
    {
        private readonly AppDbContext _db;

        public SubLifeSupportDataController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost] // POST /SublifeSupportData
        public IActionResult Create([FromBody] SubLifeSupportDatum data)
        {
            _db.SubLifeSupportData.Add(data);
            _db.SaveChanges();  

            return Ok(data);
        }
    }
}
