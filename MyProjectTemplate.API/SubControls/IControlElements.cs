//sebastian solorzano -- pr3j group 15 (sebastian, sam, evan, ryan) -- CSCN72030 f25
//interfaces for sub control elements (propeller, rudder, etc.)

namespace MyProjectTemplate.API.SubControls
{
    //putting all four of them in this file because they're so small and I hate having like 18 files
    //if we were making a real submarine, the implementations of these would call the servos or hydraulics or whatever

    //for sub movement and direction, we basically use polar coordinates to represent a vector
    // the propeller = thrust/radius, rudder= LR/phi, stern=UD/theta
        //ballast is a seperate unrelated vector acting on the sub, = bk̂

    public interface IPropeller
    {
        //represents thrust in whatever units we decide on
        public double Offset { get; set; }      //+forward; -backward

        public bool PowerStat { get;}
        public void TurnOn(); //useful if in reality, pretty much just flavor here
    }

    public interface IRudder
    {
        //represents the angle offset from the x-axis along the xy plane, in radians
        public double Offset { get; set; }         //+left; -right
    }

    public interface ISternPlate
    {
        //represents the angle offset from the x-axis along the xz-plane, in radians
        //this is technically improper, but more convenient
        //basically we're using the internal angle vs the external one (which is wrong according to convention)
        public double Offset { get; set; }         //+up; -down
    }

    public interface IBallast
    {
        //represents ballast buoyancy
        public double Offset { get; set; }      //+up;-down
    }

    //they're all really simple which is why I didn't want to bother with 4 interface files
}
