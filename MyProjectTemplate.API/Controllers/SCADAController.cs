using Microsoft.AspNetCore.Mvc;
using MyProjectTemplate.API.Data;
using MyProjectTemplate.API.Models;

namespace MyProjectTemplate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SCADAController : ControllerBase
    {
        private readonly AppDbContext _db;

        public SCADAController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("alarms")]
        public IActionResult AddAlarms([FromBody] SubAlarmsDatum data)
        {
            _db.SubAlarmsData.Add(data);
            _db.SaveChanges();

            return Ok(data);
        }

        [HttpPost("control")]
        public IActionResult AddControl([FromBody] SubControlDatum data)
        {
            _db.SubControlData.Add(data);
            _db.SaveChanges();
            return Ok(data);
        }

        [HttpPost("data")]
        public IActionResult AddData([FromBody] SubDatum data)
        {
            _db.SubData.Add(data);
            _db.SaveChanges();
            return Ok(data);
        }

        [HttpPost("lifesupport")]
        public IActionResult AddLifeSupport([FromBody] SubLifeSupportDatum data)
        {
            _db.SubLifeSupportData.Add(data);
            _db.SaveChanges();
            return Ok(data);
        }

        [HttpPost("position")]
        public IActionResult AddPosition([FromBody] SubPositionDatum data)
        {
            _db.SubPositionData.Add(data);
            _db.SaveChanges();
            return Ok(data);
        }

        [HttpPost("reactor")]
        public IActionResult AddReactor([FromBody] SubReactorDatum data)
        {
            _db.SubReactorData.Add(data);
            _db.SaveChanges();
            return Ok(data);
        }
    }
}