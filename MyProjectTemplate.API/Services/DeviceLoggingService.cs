using Microsoft.Extensions.Options;
using MyProjectTemplate.API.LifeSupportSystems;
using MyProjectTemplate.API.Services;
using System;

// This is similar to the controller above, but is refactored into a SERVICE so that it is used constantly rather than only at HTTP requests
public class DeviceLoggingService
{
    private readonly Logger _logger;
    private readonly DeviceThresholds _thresh;

    public DeviceLoggingService(Logger logger, IOptions<DeviceThresholds> thresholds)
    {
        _logger = logger;
        _thresh = thresholds.Value;
    }

    public void HandleReading(DeviceReading r)
    {
        ThresholdSet? t = r.DeviceType switch
        {
            DeviceType.Oxygen => _thresh.Oxygen,
            DeviceType.CO2 => _thresh.CO2,
            DeviceType.Pressure =>
               r.Unit == Unit.Bar ? _thresh.inPressure : _thresh.exPressure,
            DeviceType.Humidity => _thresh.Humidity,
            DeviceType.Temperature => _thresh.Temperature,
            DeviceType.AirReserve => _thresh.AirReserve,
            _ => null
        };

        if (t == null) return;

        if (t.VeryLow is double vLow && r.Value < vLow)
        {
            _logger.Danger(Guid.Parse("11111111-1111-1111-1111-111111111111"), $": {r.DeviceId} - {r.DeviceType} VERY low: {r.Value}{r.Unit}");
            return;
        }

        if (t.Low is double low && r.Value < low)
        {
            _logger.Warning(Guid.Parse("11111111-1111-1111-1111-111111111111"), $": {r.DeviceId} - {r.DeviceType} low: {r.Value}{r.Unit}");
            return;
        }

        if (t.High is double high && r.Value > high)
        {
            _logger.Danger(Guid.Parse("11111111-1111-1111-1111-111111111111"), $": {r.DeviceId} - {r.DeviceType} high: {r.Value}{r.Unit}");
            return;
        }

        if (t.VeryHigh is double vHigh && r.Value > vHigh)
        {
            _logger.Warning(Guid.Parse("11111111-1111-1111-1111-111111111111"), $": {r.DeviceId} - {r.DeviceType} VERY high: {r.Value}{r.Unit}");
            return;
        }
    }
}