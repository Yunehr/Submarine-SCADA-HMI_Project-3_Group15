using Microsoft.AspNetCore.Mvc;
using MyProjectTemplate.API.SubSubController;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyProjectTemplate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        //uh so the idea was that this would be a singleton like eventbus
        //and it would also have a similar <timer> function or something
        //which would constantly controls.calcvelocity --> navi.updatepos
        //the safetch get loop you use on the front end is actually independant of all that (at least in the case of eventbus)
        //so you'd just have to do the same thing to constatnly udpate position

        //as for updating controls, who knows
        //i couldn't figure out how to get the POST to work
        private Movement mov;

        // GET: api/<MovementController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MovementController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MovementController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MovementController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MovementController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        [HttpPost("rudder")]
        public IActionResult Rudder([FromBody] string direction)
        {
            // update movement state
            return Ok(new { status = $"Rudder {direction}" });
        }

        [HttpPost("ballast")]
        public IActionResult Ballast([FromBody] string action)
        {
            return Ok(new { status = $"Ballast {action}" });
        }

        [HttpPost("throttle")]
        public IActionResult Throttle([FromBody] int value)
        {
            return Ok(new { status = $"Throttle set to {value}" });
        }

        //[HttpGet("position")]
        //public IActionResult GetPosition()
        //{
        //    return Ok(new { x = mov.X, y = mov.Y, depth = mov.Depth }); // might have to subscribe to Navigation through event bus to get x,y,z Positions to front end 
        //}

    }
}
