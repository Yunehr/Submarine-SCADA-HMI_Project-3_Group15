using System;

public enum DeviceType { Oxygen, CO2, AirReserve, Pressure, Temperature, Humidity, 
                        FuelRod, Coolant, Rad, Reactor, Battery }
public enum Unit { Ppm, Percent, Bar, Celsius, Rads, Megawatts }


namespace MyProjectTemplate.API.LifeSupportSystems
{
    public readonly record struct DeviceReading(
        Guid DeviceId,
        DeviceType DeviceType,
        double Value,
        Unit Unit,
        DateTime TimestampUtc);
}
