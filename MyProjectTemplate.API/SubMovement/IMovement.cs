using MyProjectTemplate.API.SubSubController.SubNav;

//oh yeah i moved around the files and made more folders, hopefully that doens't mess up too much
//this is basically sub-subcontroller, but i din't want to call it that since the actual controllers are like the interface to the backend
//and having sub-subcontroller + sub-subcontroller controller felt like a bit much
//i have like 4 layers of nested classes now though, i'm starting to wonder if its too much
//well whatever
namespace MyProjectTemplate.API.SubMovement
{
    public interface IMovement
    {
        public void Power(bool on);

        public double GetSpeed();

        //gets your current position
        //call this every tick or whatever
        public double[] GetPos();

        public void ChangeVel(double mag, double xyoff, double zoff, double buoy);

        //call this once every {time increment}
        //otherwise you'll be moving too fast/slow
        public void UpdatePos();
    }
}
