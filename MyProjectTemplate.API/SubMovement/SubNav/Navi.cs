//sebastian solorzano -- pr3j group 15 (sebastian, sam, evan, ryan) -- CSCN72030 f25
//imp for navi
//pretty sure I don't need includes i'm doing fairly basic math
namespace MyProjectTemplate.API.SubSubController.SubNav
{
    public class Navi:INavi
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        private const double maplimit = 500.0; //decided by group
        private const double depthlimit = -440.0; //somewhat arbitrary; an Ohio class's test depth is 240m,
        //and with a US Navy test depth 2/3 of its design depth, it should be fine until 360m.
        //of course, it can almost certainly go deeper than that (which they obviously wouldn't tell us)
        //but this seems reasonable enough

        //cant believe I forgot the constructor
        public Navi()
        {
            X = 0.0;
            Y = 0.0;
            Z = 0.0; //assume that 0 meansthe surface?
        }

        public void UpdatePos(double Vx, double Vy, double Vz)
        {
            //would like add coefficients for this so we aren't moving at 100<unit> per frame rather than hour/minute/more reasonable measurment
            //its all abstract though so it doesn't really matter i guess


            X += Vx;
            //we're adding to, not just replacing, so we can't do it like i did in controlelements
            if (X > maplimit) 
                X = maplimit;
            else if (X < -maplimit)
                X = -maplimit;

            Y += Vy;
            if (Y > maplimit)
                Y = maplimit;
            else if (Y < -maplimit)
                Y = -maplimit;

            Z += Vz;
            if (Z > 0.0)
                Z = 0;
            else if (X < depthlimit)
                X = depthlimit;

            //I don't actually know what consequences should occur if we reach the limits, so i'll just leave it like this for now
        }

    }
}
