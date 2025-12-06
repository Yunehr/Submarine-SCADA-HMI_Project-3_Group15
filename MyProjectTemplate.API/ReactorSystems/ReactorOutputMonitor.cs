using MyProjectTemplate.API.LifeSupportSystems;

public sealed class ReactorOutputMonitor : MonitorBase
{
    public override DeviceType DeviceType => DeviceType.Reactor;
    public override Unit Unit => Unit.Megawatts;
    private double _currentOutput = 500.0; // Initial reactor output in Megawatts
    private bool _isSCRAMmed = false;

    protected override double SampleSensor()
    {
        if (_isSCRAMmed)
        {
            return _currentOutput;
        }
        else
        {
            // Simulate reactor output fluctuations
            var variation = (Random.Shared.NextDouble() - 0.5) * 4.0; // +/- 2 Megawatts
            _currentOutput = Math.Max(0, _currentOutput + variation);
            return _currentOutput;
        }
        
    }
    public double SampleSensorForTest()
    {
        return SampleSensor();
    }

    public void SCRAMReactorOutput()
    {
        _isSCRAMmed = true;
        _currentOutput = 0.0;
    }
}