//sebastian solorzano -- pr3j group 15 (sebastian, sam, evan, ryan)  -- CSCN72030 f25
//impelmetnation for actual control units
//these exist in place of something that physically interacts with a real sub


using System;

namespace MyProjectTemplate.API.SubMovement.SubControls
{

    //I don't want to make 7 different files when they all practically do one thing


    //this abstract class exists purely for functional reasons, it doesn't really mean anything structually/logically
    //dont want to bother with an interface
    public abstract class ControlElement
    {
        //you can't do #define in c# so this is the equivalent?
        protected virtual double OffsetLimit { get; } = 100.0; //defining the range as +-100 is just most convenient

        //offset basically refers to the component's value, so 0 is neutral, +- are doing something in either direction
        protected double _offset; //I don't know what this means but I think I need it
        public virtual double Offset { get=>_offset; //not quite sure what this is for but i'm pretty sure I need it
          set{  //setting (somewhat) arbitrary limits on component magnitudes. the gui will also have limits, but I just wanted to be sure
                if (value > OffsetLimit)
                    _offset = OffsetLimit;
                else if (value < -OffsetLimit) //extra brackets because I always want to make sure when working with negatives
                    _offset = -OffsetLimit;
                else
                    _offset = value;
            }
        } 



    }


    public class Propeller : ControlElement, IPropeller
    {
        public bool IsOn { get; private set; }

        //contructor
        public Propeller()
        {
            IsOn = false; //off (didn't want to make a whole enum)
            Offset = 0.0; //neutral
        }

        //turns the propeller on (basically for flavor)
        //not just setters because one would assume on a real machine it would be more involved (and thus require actual functions)
        public void TurnOn()
        {
            IsOn = true;
        }

        public void TurnOff()
        {
            IsOn = false;
        }
    }

    //okay so rudder now represents the direction you're facing rather than actual rudder
    public class Rudder : ControlElement, IRudder {
        protected override double OffsetLimit { get; } = Math.PI * 2;
        //this should limit it to 360 deg in either direction
        
        //what i'm changing is that instead of getting stuck at max, it rolls over instead
        public override double Offset
        {
            get => _offset; 
            set
            {  //technically I think this isn't necessary cus trig functions work above 360, but I didn't want to deal with stuff like that
                if (value > OffsetLimit)
                    _offset = value%OffsetLimit;
                else if (value < -OffsetLimit) 
                    _offset = value % (-OffsetLimit);
                else
                    _offset = value;
            }
        }

        //constructor! -doesn't really do much
        public Rudder(){
            Offset = 0.0; //neutral
        }
    }

    public class SternPlate : ControlElement, ISternPlate {
        protected override double OffsetLimit { get; } = Math.PI / 2;
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
