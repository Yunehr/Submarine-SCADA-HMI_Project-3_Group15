using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyProjectTemplate.API.SubSubController.SubControls;
using MyProjectTemplate.API.SubSubController.SubNav;

namespace MyProjectTemplate.API.SubSubController
{
    public class Movement
    {
        private IControls helm;
        private INavi navi;

        public Movement()
        {
            helm = new Controls();
            navi = new Navi();

        }

        public void Power(bool on) //i feel bad giving these all so similar names but reall what else to call them?
        {
            if(on)
                helm.PowerOn();
            if (!on)
                helm.PowerOff();
        }

        public double GetSpeed() //speed specifically, not vel. might be useless since the thruster bar kinda tells you already?
        {
            double[] temp = helm.CalcVelocity();
            return temp[4];//loc of speed
        }

        //gets your current position
        //call this every tick or whatever
        public double[] GetPos()
        {
            double[] temp = { navi.X, navi.Y, navi.Z };
            return temp;
        }

        //not sure if things should be changed all at once or together?
        public void ChangeVel(double mag, double xyoff, double zoff, double buoy)
        {
            helm.Thrust(mag);
            helm.Turn(xyoff);
            helm.Pitch(zoff);
            helm.AdjBuoyancy(buoy);
        }

        //call this once every {time increment}
        //otherwise you'll be moving too fast/slow
        public void UpdatePos()
        {
            double[] temp = helm.CalcVelocity(); //grabs current speed
            navi.UpdatePos(temp[0], temp[1], temp[2]); //moves sub by that much
        }


        //instead of going with eventbus i could just create a similar <timer> function here
        //'dhave to see how that works though


    }
}
