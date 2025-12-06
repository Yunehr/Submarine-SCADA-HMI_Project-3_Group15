using MyProjectTemplate.API.LifeSupportSystems;

public sealed class BatteryMonitor : MonitorBase
{
    public override DeviceType DeviceType => DeviceType.Battery;
    public override Unit Unit => Unit.Percent;
    private double _currentCharge = 100.0; // Initial battery charge in Percent
    private bool _isDischarging = true;

    protected override double SampleSensor()
    {
        if (_isDischarging)
        {
            // Simulate battery charge fluctuations
            var variation = (Random.Shared.NextDouble() - 0.5) * 2.0; // +/- 1%
            _currentCharge = Math.Max(0, Math.Min(100, _currentCharge + variation));
            // var variation = (Random.Shared.NextDouble() - 0.5) * 2.0; // +/- 1%
            // _currentCharge = Math.Max(0, Math.Min(100, _currentCharge + variation));
            // or just return for a stable charge reading
            return _currentCharge;
        }
        else
        {
            // Simulate battery disconnected from Reactor, slowly discharging
            _currentCharge = Math.Max(0, _currentCharge - 0.5); // Decrease by 0.5% each sample
            return _currentCharge;
        }
    }
    public double SampleSensorForTest()
    {
        return SampleSensor();
    }

    public void SCRAMBatteryDisconnect()
    {
        _isDischarging = false;
    }
}