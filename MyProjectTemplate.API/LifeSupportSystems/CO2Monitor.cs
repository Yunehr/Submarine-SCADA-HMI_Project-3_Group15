using MyProjectTemplate.API.LifeSupportSystems;

public sealed class Co2Monitor : MonitorBase
{
    public override DeviceType DeviceType => DeviceType.CO2;
    public override Unit Unit => Unit.Ppm;
    protected override double SampleSensor() =>
        400 + (Random.Shared.NextDouble() - 0.5) * 10;
}