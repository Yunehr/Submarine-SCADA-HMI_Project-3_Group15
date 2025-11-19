using MyProjectTemplate.API.LifeSupportSystems;

public sealed class OxygenMonitor : MonitorBase
{
    public override DeviceType DeviceType => DeviceType.Oxygen;
    public override Unit Unit => Unit.Percent;
    protected override double SampleSensor() =>
        20.9 + (Random.Shared.NextDouble() - 0.5) * 0.2;
}