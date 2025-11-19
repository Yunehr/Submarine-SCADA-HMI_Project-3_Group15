using System;

public enum DeviceType { Oxygen, CO2, AirReserve, Pressure, Temperature, Humidity }
public enum Unit { Ppm, Percent, Bar, Celsius }


namespace MyProjectTemplate.API.LifeSupportSystems
{
    public readonly record struct DeviceReading(
        Guid DeviceId,
        DeviceType DeviceType,
        double Value,
        Unit Unit,
        DateTime TimestampUtc);
}
