using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProjectTemplate.API.Data;
using MyProjectTemplate.API.Models;

namespace MyProjectTemplate.API.Controllers
{
    [ApiController]
    [Route("[controller]")] // Route: /SubReactorData

    public class SubReactorDataController : Controller
    {
        private readonly AppDbContext _db;

        public SubReactorDataController(AppDbContext db) // Setting up the database connection
        {
            _db = db;
        }

        [HttpPost] // POST /SubReactorData
        public IActionResult Create([FromBody] SubReactorDatum data)
        {
            _db.SubReactorData.Add(data);
            _db.SaveChanges();
            
            return Ok(data);
        }
    }
}
