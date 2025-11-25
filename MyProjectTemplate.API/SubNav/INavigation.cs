//sebastian solorzano -- pr3j group 15 (sebastian, sam, evan, ryan) -- CSCN72030 f25
//interf for nav

namespace MyProjectTemplate.API.SubNav //still not sure how namespaces work but everything has its folder so it probably works
{
    public interface INavigation
    {
        //xy define the horizontal plane
        public double X { get; }
        public double Y { get; }
        //z defines depth
        public double Z { get; }

        //just stick in the numbers from controls.calcvelocity, don't worry about anything else
        //call this for every frame or whatever you want to update position
        void UpdatePos(double Vx, double Vy, double Vz);
            //wow, how nice of calcvelocity to give us the direction vectors for each axis
            //i wonder who could have written such a nice function?
    }
}
