using System;
using System.Timers;

namespace MyProjectTemplate.API.LifeSupportSystems
{
    public interface IDevice
    {
        Guid Id { get; }
        DeviceType DeviceType { get; }
        Unit Unit { get; }

        double CurrentValue { get; }

        event EventHandler<DeviceReading>? ReadingAvailable;

        void Start();
        void Stop();
    }
}
