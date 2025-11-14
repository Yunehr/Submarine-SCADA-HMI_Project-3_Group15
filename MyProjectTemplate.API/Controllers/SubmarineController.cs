using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProjectTemplate.API.Models;
using MyProjectTemplate.API.Data;

namespace MyProjectTemplate.API.Controllers
{
    [ApiController]
    [Route("[controller]")] // Route: /SubData
    public class SubDataController : ControllerBase
    {
        private readonly AppDbContext _db;

        public SubDataController(AppDbContext db) // Setting up the database connection
        {
            _db = db;
        }

        // POST /SubData
        [HttpPost]
        public IActionResult CreateSubData([FromBody] SubDatum data) // This will accept a .json representing a SubDatum
        {
            _db.SubData.Add(data); // Saves that .json data into our database through EF Core
            _db.SaveChanges(); 

            return Ok(data);
        }
    }
}