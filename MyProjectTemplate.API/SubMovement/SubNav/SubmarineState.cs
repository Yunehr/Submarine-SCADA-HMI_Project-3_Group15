namespace MyProjectTemplate.API.Submarine
{
    public class SubmarineState: ISubmarineState    // this was a decoupled way for lifesupport to get depth.
    {                                               // but meh. Just gonna inject movement into lifesupport
        public double DepthMeters { get; set; }
    }
}
