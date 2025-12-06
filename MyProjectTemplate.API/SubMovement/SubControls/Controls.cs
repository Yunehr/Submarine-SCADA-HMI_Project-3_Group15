//sebastian solorzano -- pr3j group 15 (sebastian, sam, evan, ryan) -- CSCN72030 f25
//submarine controls

//do i need includes here? no clue

//how do namespaces work? no clue
using System;

namespace MyProjectTemplate.API.SubMovement.SubControls //capitals? iunno
{

    //ugghhh how do permissions work
    public class Controls: IControls
    {
        //no touchy the hardware from anywhere else
        private IPropeller prop;
        private IRudder rudd;
        private ISternPlate stnp;
        private IBallast ball;


        //constructor
        public Controls()
        {
            //I think this is how you're meant to use interface-concrete pairs?
            prop = new Propeller();
            rudd = new Rudder();
            stnp = new SternPlate();
            ball = new Ballast();
        }

        public void PowerOn()
        {
            prop.TurnOn();
        }
        public void PowerOff()
        {
            prop.TurnOff();
        }

        public void Thrust(double offset){
            prop.Offset = offset;
                //doing this is kinda ugly, but there's no point in making a function for it when this is literally the entire purpose of a setter
        }

        //accepting deg rather than rad since it makes it easier for everyone else
        //+left -right
        public void Turn(double offset) {
            offset = offset * Math.PI / 180.0;
            rudd.Offset = offset; 
        }

        //+up -down
        public void Pitch(double offset) {
            offset = offset * Math.PI / 180.0;
            stnp.Offset = offset;
        }
        //+up -down
        public void AdjBuoyancy(double offset) {
            ball.Offset = offset;
        }

        //thanks to all this no one touches the components directly

        //I am still unsure whether this should exist here or under navigation or be an attribute instead
        public double[] CalcVelocity()
        {
            double B = ball.Offset/20.0;

            //if the sub is off its obviously not moving
            if (prop.IsOn == false)
                return  new double[4] { 0.0,0.0,B,0.0 }; //except for ballast, which is independat of power

            //in any other circumstance

            //for convenience
            double phi = rudd.Offset;
            double theta = stnp.Offset;
            double R = prop.Offset/10.0;
                //assuming our units are m/s, then our unmodified speed was 100m/s, or 360km/h
                //this is obviously too fast. with this, its 10m/s, or 36km/h,
                //which is coinicdentally the same as an ohio classe's official undertwater speed
                //made bouyancy a bit weaker because that seems better? pure vibes though


            // sub's velocity vector = RV, where R is a scalar and V is the unit vector in its direction (direction vector)
            // ∴ V= |V| = 1

            // if we treat V as having a Z component and an XY component (as in this image: https://upload.wikimedia.org/wikipedia/commons/4/4f/3D_Spherical.svg)
            // then |Vz| = |V| sin(θ)
            // and |Vxy| = |V| cos(θ)
                    //(we're treating θ as the internal angle rather than the external as in the image)
            //but |V| = 1, so |Vz| = sin(θ) and |Vxy| = cos(θ)

            // and since |Ax| = |A| cos(θ) ,
            // |Vx| = |Vxy| cos(φ)
            // ∴   = cos(θ) cos(φ)
            double x = Math.Cos(theta) * Math.Cos(phi);
            double y = Math.Cos(theta) * Math.Sin(phi);
            double z = Math.Sin(theta);
            //thus, we have the normed unit vectors for the submarine's velocity

            //getting the sub's actual velocity is now simple, as we're just performing scalar multiplication on its components
            x = x * R;
            y = y * R;
            z = z * R;
            //however, we have an additional vector to consider, the buoyancy
            //since it exists purely in the Z, we can just add the two
            z = z + B;

            //putting the resultant vector into an array to give to navigation or whoever else wants it
            double[] subvelocity = new double[4] { x, y, z, R }; //adding R there just so the gui or whoever can easily access speed

            return subvelocity;
        }

    }

}