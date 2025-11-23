using MyProjectTemplate.API.LifeSupportSystems;

public sealed class PressureMonitor : MonitorBase
{
    public override DeviceType DeviceType => DeviceType.Pressure;
    public override Unit Unit => Unit.Bar;
    protected override double SampleSensor() =>
        1 + (Random.Shared.NextDouble() - 0.2) * 0.2;
}