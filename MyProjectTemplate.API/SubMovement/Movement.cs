using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyProjectTemplate.API.SubMovement;
using MyProjectTemplate.API.SubSubController.SubControls;
using MyProjectTemplate.API.SubSubController.SubNav;

namespace MyProjectTemplate.API.SubSubController
{
    public class Movement:IMovement
    {
        private IControls helm;
        private INavi navi;
        private System.Timers.Timer? clock;


        public Movement()
        {
            helm = new Controls();
            navi = new Navi(); 
            //their own constructors initialize them nicely alrady
        }


        public void Power(bool on) //i feel bad giving these all so similar names but reall what else to call them?
        {
            if(on)
                helm.PowerOn();
            if (!on)
                helm.PowerOff();
        }

        public void changeThrust(double val)
        {
            helm.Thrust(val);
        }

        public void changeRudder(double val)
        {
            helm.Turn(val);
        }

        public void changePitch(double val)
        {
            helm.Pitch(val);
        }

        public void changeBallast(double val)
        {
            helm.AdjBuoyancy(val);
        }

        //returns
        public double GetSpeed() 
        {
            double[] temp = helm.CalcVelocity();

            return temp[4];//loc of speed
        }//might be useless since the thruster bar should tell you this


        //call the following every 'frame' to update your gui
        public double GetPosX()
        {
            return navi.X;
        }
        public double GetPosY()
        {
            return navi.Y;
        }
        public double GetPosZ()
        {
            return navi.Z;
        }


        //this should be called in a loop eternally to simulate movement
        private void UpdatePos(Object source, System.Timers.ElapsedEventArgs e)
        {
            double[] temp = helm.CalcVelocity(); //the only way to get helm's values I think
            navi.UpdatePos(temp[0], temp[1], temp[2]); //moves sub by that much
        }


        public void RunStart()
        {
            if (clock != null) 
                return; //clocks running, dont do nothin

            clock = new System.Timers.Timer(2000);

            clock.Elapsed += UpdatePos; //basically saying it calls this function everytime the timer ends

            clock.AutoReset = true;
            clock.Start(); //same as clock.enabled= true
        }


    }
}
