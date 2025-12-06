using MyProjectTemplate.API.LifeSupportSystems;

public sealed class OxygenMonitor : MonitorBase
{
    public override DeviceType DeviceType => DeviceType.Oxygen;
    public override Unit Unit => Unit.Percent;
    private double _currentLevel = 21.0;
    protected override double SampleSensor()
    {
        var value = _currentLevel;
        _currentLevel -= 0.02;  // Simulate oxygen decrease each sample
        return value;
    }
    public double SampleSensorForTest()
    {
        return SampleSensor();
    }

    public void resetOxygenLevel()
    {
        _currentLevel = 21.0; // Reset to normal oxygen level
    }

    public void OxygenDropTo15()
    {
        _currentLevel = 15.0; // Simulate oxygen drop to 15%
    }
    public void HalveOxygenLevel()
    {
        _currentLevel /= 2; // Simulate oxygen drop to half of current level
    }
}