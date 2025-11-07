//sebastian solorzano -- pr3j group 15 (sebastian, sam, evan, ryan) -- controls
//interface for submarine controls

//this is basically like a .h file, except even more fragmented

//do i need includes here? no clue

//how do namespaces work? no clue
namespace MyProjectTemplate.API.SubControls 
{
    public interface IControls
    {
       // float direction { get; } //sum of xz and xy direction vectors, 
        void PowerOn(); //diagram said put this in prop, but the other 2 are dependant on it so its prolly fine
        //besides, if it wasn't here, how could the outside access it?

        //not sure if these should be void? whatever
        void Turn(double offset);
        void Pitch(double offset);
        void AdjBuoyancy(double offset);

        public double[] CalcVelocity();

    }

}