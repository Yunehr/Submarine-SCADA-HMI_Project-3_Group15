using MyProjectTemplate.API.LifeSupportSystems;

public sealed class AirReserveMonitor : MonitorBase
{
    public override DeviceType DeviceType => DeviceType.AirReserve;
    public override Unit Unit => Unit.Bar;
    protected override double SampleSensor() =>
        185 + (Random.Shared.NextDouble() - 0.5);
}


//using FileHandler;

//namespace DeviceMonitor
//{
//    public class AirReserveMonitor
//    {
//        private double rerservePercentage;
//        private int currentDataLine = 0;

//        public double ReservePercentage
//        {
//            get => rerservePercentage;
//            set => rerservePercentage = value < 0 ? 0.0 : (value > 100 ? 100.0 : value);
//        }

//        public void ResetDataLine() {currentDataLine = 0;}

//        // Take AirReserve Reading
//        public void TakeAirReserveReading(string fp)
//        {
//            var fr = new FileReader();
//            ReservePercentage = fr.ReadDoubleFromNextLine(fp, currentDataLine);
//            currentDataLine++;
//        }

//        // Log AirReserve
//        public void LogAirReserveReading(string fp)
//        {
//            var fw = new FileWriter();
//            fw.AppendDoubleToFile(fp, ReservePercentage);
//        }
//    }
//}