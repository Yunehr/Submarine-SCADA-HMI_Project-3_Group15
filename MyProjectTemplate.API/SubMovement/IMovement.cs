using MyProjectTemplate.API.SubMovement.SubNav;

//oh yeah i moved around the files and made more folders, hopefully that doens't mess up too much
//this is basically sub-subcontroller, but i din't want to call it that since the actual controllers are like the interface to the backend
//and having sub-subcontroller + sub-subcontroller controller felt like a bit much
//i have like 4 layers of nested classes now though, i'm starting to wonder if its too much
//well whatever
namespace MyProjectTemplate.API.SubMovement
{
    public interface IMovement
    {

        //commands to be called:
        public void Power(bool on);
        public void changeThrust(double val); //range +-100
        public void changeRudder(double val); //range +-360; +left -right
        public void changePitch(double val); //range +-90; +up -down

        public void changeBallast(double val); //range +-100; +up -down


        //returns
        public double GetSpeed();
            //call the following every 'frame' to update your gui:
        public double GetPosX();
        public double GetPosY();
        public double GetPosZ();


        //looper:
        public void RunStart();
        //this also handles constalnty updating the position, but that's private

        //the above uses a timer, which doens't work in the unti testing
        public void TestingRunStartOnce();

    }
}
