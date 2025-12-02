using MyProjectTemplate.API.LifeSupportSystems;

public sealed class PressureMonitor : MonitorBase
{
    public override DeviceType DeviceType => DeviceType.Pressure;
    public override Unit Unit => Unit.Bar;
    private double _currentLevel = 1.0; // Starting pressure in Bar
    
    // TODO: Fix logic to only decrease on Scrubber activity
    protected override double SampleSensor() 
    {
        var value = _currentLevel;
        //_currentLevel -= 0.01;  // Simulate pressure decrease each sample
        return value;
    }
    public void resetPressureLevel()
    {
        _currentLevel = 1.0; // Reset to normal pressure level
    }
    public void PressureDrop()
    {
        _currentLevel = 0.5; // Simulate pressure drop to 0.5 Bar
    }
}