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
            Y += Vy;
            Z += Vz;

        }

    }
}
