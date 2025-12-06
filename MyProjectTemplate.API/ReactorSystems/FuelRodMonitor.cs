using MyProjectTemplate.API.LifeSupportSystems;
public sealed class FuelRodMonitor : MonitorBase
{
    public override DeviceType DeviceType => DeviceType.FuelRod;
    public override Unit Unit => Unit.Percent; 
    private double _currentIntegrity = 100.0; // Initial fuel rod integrity in Percent
    private bool _isSCRAMmed = false;
    protected override double SampleSensor()
    {
        if (_isSCRAMmed)
        {
            return _currentIntegrity;
        }
        else
        {
            // // Simulate fuel rod integrity variably decrease over time
            // var variation = (Random.Shared.NextDouble() - 0.5) * 0.5; // +/- 0.25%
            // //_currentIntegrity = Math.Max(0, _currentIntegrity - 0.1 + variation); // Decrease by 0.1% each sample
            // _currentIntegrity = Math.Max(0, _currentIntegrity + variation);

            // Simulate stable integrity decrease each sample
            _currentIntegrity -= 0.1; // Decrease by 0.1% each sample
            return _currentIntegrity;
        }
    }
    public double SampleSensorForTest()
    {
        return SampleSensor();
    }

    public void SCRAMFuelRodIntegrity()
    {
        // reactor has been SCRAMmed, set fuel rod integrity to max to avoid false alarms
        _isSCRAMmed = true;
        _currentIntegrity = 100.0;
    }

    public void FuelRodIntegrityDrop()
    {
        _currentIntegrity = 20.0;
    }
}