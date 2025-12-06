using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyProjectTemplate.API.LifeSupportSystems;
using MyProjectTemplate.API.Services;
using System;
using System.Security.Claims;
public class ThresholdHandlers
{
    private readonly DeviceThresholds _thresh;


    public ThresholdHandlers(IOptions<DeviceThresholds> thresholds)
    {
        _thresh = thresholds.Value;
    }


    public int HandleReading(DeviceReading r)
    {
        // No alarm = 0
        // Warning = 1;
        // Danger = 2;


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


        if (t == null) return 0;


        if (t.VeryLow is double vLow && r.Value < vLow)
        {
            return 2;
        }


        if (t.Low is double low && r.Value < low)
        {
            return 1;
        }


        if (t.High is double high && r.Value > high)
        {
            return 1;
        }


        if (t.VeryHigh is double vHigh && r.Value > vHigh)
        {
            return 2;
        }

        else return 0;
    }

}