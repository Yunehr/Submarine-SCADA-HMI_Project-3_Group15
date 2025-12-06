using MyProjectTemplate.API.LifeSupportSystems;


public sealed class CoolantMonitor : MonitorBase
{
    public override DeviceType DeviceType => DeviceType.Coolant;
    public override Unit Unit => Unit.Percent;
    private double _currentLevel = 100.0; // Initial coolant level in Percent
    private bool _isSCRAMmed = false;

    protected override double SampleSensor()
    {
        if (_isSCRAMmed)
        {
            return _currentLevel;
        }
        else
        {
            // // Simulate coolant level variable decrease over time
            // var variation = (Random.Shared.NextDouble() - 0.5) * 1.0; // +/- 0.5%
            // //_currentLevel = Math.Max(0, _currentLevel - 0.1 + variation); // Decrease by 0.1% each sample
            // _currentLevel = Math.Max(0, _currentLevel + variation);

            // Simulate stable level decrease each sample
            _currentLevel -= 0.2; // Decrease by 0.2% each sample
            return _currentLevel;
        }
    }
    public double SampleSensorForTest()
    {
        return SampleSensor();
    }

    public void SCRAMCoolantLevel()
    {
        // reactor has been SCRAMmed, set coolant level to max to avoid false alarms
        _isSCRAMmed = true;
        _currentLevel = 100.0;
    }

    public void CoolantLevelDrop()
    {
        _currentLevel = 20.0;
    }

}