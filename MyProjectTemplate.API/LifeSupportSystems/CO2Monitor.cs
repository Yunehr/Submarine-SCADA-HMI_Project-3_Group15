using MyProjectTemplate.API.LifeSupportSystems;

public sealed class Co2Monitor : MonitorBase
{
    public override DeviceType DeviceType => DeviceType.CO2;
    public override Unit Unit => Unit.Ppm;
    private double _currentLevel = 0;
    protected override double SampleSensor()
    {
        var value = _currentLevel;
        _currentLevel += 5; // Simulate CO2 increase each sample
        return value;
    }
    public double SampleSensorForTest()
    {
        return SampleSensor();
    }

    public void resetCo2Level()
    {
        _currentLevel = 0; // Reset to normal CO2 level
    }

    public void Co2Spike()
    {
        _currentLevel = 1200; // Simulate CO2 spike to 1200 ppm
    }
}