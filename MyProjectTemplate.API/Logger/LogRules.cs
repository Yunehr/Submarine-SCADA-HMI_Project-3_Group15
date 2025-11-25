using MyProjectTemplate.API;
using MyProjectTemplate.API.LifeSupportSystems;

public class LogRules 
{
    private readonly IEventBus _bus;
    private readonly Logger _logger;

    private readonly List<IDisposable> subs = new();

    public LogRules(IEventBus bus, Logger logger)
    {
        _bus = bus;
        _logger = logger;

        subs.Add(_bus.Subscribe(DeviceType.Oxygen, ProcessReading));
        subs.Add(_bus.Subscribe(DeviceType.CO2, ProcessReading));
        subs.Add(_bus.Subscribe(DeviceType.Temperature, ProcessReading));
        subs.Add(_bus.Subscribe(DeviceType.Pressure, ProcessReading));
        subs.Add(_bus.Subscribe(DeviceType.Humidity, ProcessReading));
        subs.Add(_bus.Subscribe(DeviceType.AirReserve, ProcessReading));
    }

    public void FiveReadingZones(DeviceReading r, int vLow, int low, int high, int vHigh)
    {
        if (r.Value < vLow)
            _logger.Danger(r.DeviceId, $"{r.DeviceType} VERY low: {r.Value}{r.Unit}");
        else if (r.Value < low)
            _logger.Warning(r.DeviceId, $"{r.DeviceType} low: {r.Value}{r.Unit}");
        else if (r.Value > high)
            _logger.Danger(r.DeviceId, $"{r.DeviceType} high: {r.Value}{r.Unit}");
        else if (r.Value > vHigh)
            _logger.Warning(r.DeviceId, $"{r.DeviceType} VERY high: {r.Value}{r.Unit}");
    }

    public void ThreeReadingZonesUpper(DeviceReading r, int high, int vHigh)
    {
        if (r.Value > high)
            _logger.Danger(r.DeviceId, $"{r.DeviceType} high: {r.Value}{r.Unit}");
        else if (r.Value > vHigh)
            _logger.Warning(r.DeviceId, $"{r.DeviceType} VERY high: {r.Value}{r.Unit}");
    }
    public void ThreeReadingZonesLower(DeviceReading r, int high, int vHigh)
    {
        if (r.Value > high)
            _logger.Danger(r.DeviceId, $"{r.DeviceType} VERY low: {r.Value}{r.Unit}");
        else if (r.Value > vHigh)
            _logger.Warning(r.DeviceId, $"{r.DeviceType} low: {r.Value}{r.Unit}");
    }


    public void ProcessReading(DeviceReading r)
    {
        switch (r.DeviceType)
        {
            // OXYGEN
            case DeviceType.Oxygen:
                FiveReadingZones(r, 18, 20, 22, 23);
                break;

            // CO2
            case DeviceType.CO2:
                ThreeReadingZonesUpper(r, 1000, 2000);
                break;

            // PRESSURE (No zones yet)
            // case DeviceType.Pressure:
            //    ThreeReadingZonesLower(r, 50, 15);
            //   break;

            // HUMIDITY
            case DeviceType.Humidity:
                FiveReadingZones(r, 30, 35, 55, 60);
                break;

            // TEMPERATURE
            case DeviceType.Temperature:
                FiveReadingZones(r, 13, 15, 23, 25);
                break;

            // AIRTANKS
            case DeviceType.AirReserve:
                ThreeReadingZonesLower(r, 15, 50);
                break;
        }
    }
}