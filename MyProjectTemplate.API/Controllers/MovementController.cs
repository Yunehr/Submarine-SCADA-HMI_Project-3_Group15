using Microsoft.AspNetCore.Mvc;
using MyProjectTemplate.API.SubMovement;
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

        private IMovement mov;

        public MovementController(IMovement mover)
        {
            mov = mover;
        }

        /*
        [HttpPost("ration")]
        public IActionResult SetRationLevel([FromBody] RationLevelRequest request)
        {
            bunkerStatuses.RationStatus = MapRationLevel(request.Level);
            return Ok(new { bunkerStatuses.RationStatus });
        }
        */


        // GET: api/<MovementController>
        [HttpGet("Xpos")]
        public double GetXPos()
        { 
            return mov.GetPosX();
        }

        [HttpGet("Ypos")]
        public double GetYPos()
        {
            return mov.GetPosY();
        }

        [HttpGet("Zpos")]
        public double GetZPos()
        {
            return mov.GetPosZ();
        }

        // GET: api/<MovementController>
        [HttpGet("speed")]
        public double SpeedGet()
        {
            return mov.GetSpeed();
        }


        // POST api/<MovementController>
        [HttpPost("Pitch")]
        public void pitch([FromBody] string value)
        {
            double val = double.Parse(value);
            mov.changePitch(val);
        }

        // POST api/<MovementController>
        [HttpPost("Throttle")]
        public void throttle([FromBody] string value)
        {
            double val = double.Parse(value);
            mov.changeThrust(val);
        }

        // POST api/<MovementController>
        [HttpPost("Rudder")]
        public void rudder([FromBody] string value)
        {
            double val = double.Parse(value);
            mov.changeRudder(val);
        }

        // PUT api/<MovementController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }


    }
}
