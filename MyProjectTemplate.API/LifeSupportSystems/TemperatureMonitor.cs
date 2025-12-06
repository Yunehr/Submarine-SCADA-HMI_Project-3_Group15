using MyProjectTemplate.API.LifeSupportSystems;

public sealed class TemperatureMonitor : MonitorBase
{
    public override DeviceType DeviceType => DeviceType.Temperature;
    public override Unit Unit => Unit.Celsius;
protected override double SampleSensor()
    {
        return 22.0 + (Random.Shared.NextDouble() - 0.5) * 0.5; // Simulate temperature around 22.0 °C with slight variation
    }
}