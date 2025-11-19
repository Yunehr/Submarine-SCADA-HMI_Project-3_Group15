using MyProjectTemplate.API.LifeSupportSystems;

public sealed class TemperatureMonitor : MonitorBase
{
    public override DeviceType DeviceType => DeviceType.Temperature;
    public override Unit Unit => Unit.Celsius;
    protected override double SampleSensor() =>
        20.9 + (Random.Shared.NextDouble() - 0.5) * 0.2;
}