using MyProjectTemplate.API.LifeSupportSystems;

public sealed class AirReserveMonitor : MonitorBase
{
    public override DeviceType DeviceType => DeviceType.AirReserve;
    public override Unit Unit => Unit.Percent;
    private double _currentLevel = 100.0;

    //TODO: Fix logic to only decrease on reppressurization
    protected override double SampleSensor()    // incorrect, should only decrease on reppressurization
    {
        var value = _currentLevel;
        //_currentLevel -= 0.05; // Simulate air reserve decrease each sample
        return value;
    }
    public void resetAirReserveLevel()
    {
        _currentLevel = 100.0; // Reset to normal air reserve level
    }
    public void AirReserveDropBy10()
    {
        _currentLevel *= 0.9; // Simulate air reserve drop by 10%
    }
    public void AirReserveDropTo20()
    {
        _currentLevel = 20; // Simulate air reserve drop to 20%
    }
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