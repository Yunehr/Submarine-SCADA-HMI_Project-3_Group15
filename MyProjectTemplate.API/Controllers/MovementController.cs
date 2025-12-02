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

        //Test -- Start
        public class ControlRequest
        {
            public double Value { get; set; }
        }

        [HttpPost("Throttle")]
        public IActionResult Throttle([FromBody] ControlRequest request)
        {
            mov.changeThrust(request.Value);
            return Ok(new { status = "Throttle updated", value = request.Value });
        }

        [HttpPost("Pitch")]
        public IActionResult Pitch([FromBody] ControlRequest request)
        {
            mov.changePitch(request.Value);
            return Ok(new { status = "Pitch updated", value = request.Value });
        }

        [HttpPost("Rudder")]
        public IActionResult Rudder([FromBody] ControlRequest request)
        {
            mov.changeRudder(request.Value);
            return Ok(new { status = "Rudder updated", value = request.Value });
        }

        // Add ballast endpoints
        [HttpPost("Ballast/Fill")]
        public IActionResult BallastFill([FromBody] ControlRequest request)
        {
            mov.changeBallast(request.Value); //Hard Coded to decrease ballast value by 10. might make it 1 later
            return Ok(new { status = "Ballast filling", value = request.Value });
        }

        [HttpPost("Ballast/Empty")]
        public IActionResult BallastEmpty([FromBody] ControlRequest request)
        {
            mov.changeBallast(request.Value); //Hard Coded to increase ballast value by 10. might make it 1 later
            return Ok(new { status = "Ballast emptying", value = request.Value });
        }

        //Test -- End

        //// POST api/<MovementController>
        //[HttpPost("Pitch")]
        //public void pitch([FromBody] string value)
        //{
        //    double val = double.Parse(value);
        //    mov.changePitch(val);
        //}

        //// POST api/<MovementController>
        //[HttpPost("Throttle")]
        //public void throttle([FromBody] string value)
        //{
        //    double val = double.Parse(value);
        //    mov.changeThrust(val);
        //}

        //// POST api/<MovementController>
        //[HttpPost("Rudder")]
        //public void rudder([FromBody] string value)
        //{
        //    double val = double.Parse(value);
        //    mov.changeRudder(val);
        //}

        //// PUT api/<MovementController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{

    }


}
