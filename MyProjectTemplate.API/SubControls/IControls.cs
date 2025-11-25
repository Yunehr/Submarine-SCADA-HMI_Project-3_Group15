//sebastian solorzano -- pr3j group 15 (sebastian, sam, evan, ryan) -- CSCN72030 f25
//interface for submarine controls

//this is basically like a .h file, except even more fragmented

//do i need includes here? no clue

//how do namespaces work? no clue
namespace MyProjectTemplate.API.SubControls 
{
    public interface IControls
    {
        public void PowerOn(); //diagram said put this in prop, but the other 2 are dependant on it so its prolly fine
        //besides, if it wasn't here, how could the outside access it?
        public void PowerOff(); //missing in diagram, but we prolly need it no?
        //could these be one function? I guess?

        //not sure if these should be void? whatever
        public void Thrust(double offset); //range -+100
        public void Turn(double offset); //range -+90
        public void Pitch(double offset); //range -+90
        public void AdjBuoyancy(double offset); //range -+90

        public double[] CalcVelocity();

    }

}