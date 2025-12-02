using MyProjectTemplate.API.LifeSupportSystems;

public sealed class RadMonitor : MonitorBase
{
    public override DeviceType DeviceType => DeviceType.Rad;
    public override Unit Unit => Unit.Rads;
    private double _currentLevel = 0.05; // Initial radiation level in Rads
    private bool _isSCRAMmed = false;

    protected override double SampleSensor()
    {
        if (_isSCRAMmed)
        {
            return _currentLevel;
        }
        else
        {
            // Simulate radiation level fluctuations
            var variation = (Random.Shared.NextDouble() - 0.5) * 0.02; // +/- 0.01 Rads
            _currentLevel = Math.Max(0, _currentLevel + variation);
            return _currentLevel;
        }
    }
    public void SCRAMRadiationLevel()
    {
        _isSCRAMmed = true;
        _currentLevel = 0.5;
    }
}
