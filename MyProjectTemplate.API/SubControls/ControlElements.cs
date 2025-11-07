//sebastian solorzano -- pr3j group 15 (sebastian, sam, evan, ryan) -- controls
//impelmetnation for actual control units
//these exist in place of something that physically interacts with a real sub


using System; //for math

namespace MyProjectTemplate.API.SubControls
{

    //I don't want to make 7 different files when they all practically do one thing


    //this abstract class exists purely for functional reasons, it doesn't really mean anything structually/logically
    //dont want to bother with an interface
    public abstract class ControlElement
    {
        //you can't do #define in c# so this is the equivalent?
        protected virtual double OffsetLimit { get; } = 100.0; //defining the range as +-100 is just most convenient

        //offset basically refers to the component's value, so 0 is neutral, +- are doing something in either direction
        public virtual double Offset { get=>Offset; //not quite sure what this is for but i'm pretty sure I need it
          set{  //setting (somewhat) arbitrary limits on component magnitudes. the gui will also have limits, but I just wanted to be sure
                if (value > OffsetLimit)
                    Offset = OffsetLimit;
                else if (value < (-OffsetLimit)) //extra brackets because I always want to make sure when working with negatives
                    Offset = (-OffsetLimit);
                else
                    Offset = value;
            }
        } 

    }


    public class Propeller : ControlElement, IPropeller
    {
        public bool PowerStat { get; private set; }

        //contructor
        public Propeller()
        {
            PowerStat = false; //off (didn't want to make a whole enum)
            Offset = 0.0; //neutral
        }

        //turns the propeller on (basically for flavor)
        public void TurnOn()
        {
            PowerStat = true;
        }
    }

    public class Rudder : ControlElement, IRudder {
        protected override double OffsetLimit { get; } = (Math.PI / 2);
        //setting less arbitrary limits on rudder angle. basically limits to 180 degrees in front of sub
        //since it wouldn't make sensee for rudder direction to point the sub backwards

        //constructor! -doesn't really do much
        public Rudder(){
            Offset = 0.0; //neutral
        }
    }

    public class SternPlate : ControlElement, ISternPlate {
        protected override double OffsetLimit { get; } = (Math.PI / 2);
        //same as above

        public SternPlate(){
            Offset = 0.0; //neutral
        }
    }

    public class Ballast : ControlElement, IBallast {
        //don't really need to do much here, its all covered by the abstract
        public Ballast() {
            Offset = 0.0;
        }
    }


}
