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
            // Simulate fuel rod integrity decrease over time
            var variation = (Random.Shared.NextDouble() - 0.5) * 0.5; // +/- 0.25%
            //_currentIntegrity = Math.Max(0, _currentIntegrity - 0.1 + variation); // Decrease by 0.1% each sample
            _currentIntegrity = Math.Max(0, _currentIntegrity + variation);
            return _currentIntegrity;
        }
    }

    public void SCRAMFuelRodIntegrity()
    {
        _isSCRAMmed = true;
        _currentIntegrity = 100.0;
    }

    public void FuelRodIntegrityDrop()
    {
        _currentIntegrity = 20.0;
    }
}