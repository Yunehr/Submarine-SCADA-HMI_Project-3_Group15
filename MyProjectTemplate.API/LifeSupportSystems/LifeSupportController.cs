//using DeviceMonitor;
using System;
using System.Threading;

namespace MyProjectTemplate.API.LifeSupportSystems
{
    class LifeSupportController
    {
        static void Main()
        {
            var bus = new EventBus();

            var o2 = new OxygenMonitor();
            var co2 = new Co2Monitor();
            var air = new AirReserveMonitor();

            bus.Register(o2);
            bus.Register(co2);
            bus.Register(air);

            // Alarm thresholds
            const double O2_MIN = 21.0; // 19.5
            const double CO2_MAX = 390;    // 1000

            // Subscribe
            bus.Subscribe(DeviceType.Oxygen, reading =>
            {
                Console.WriteLine($"O₂: {reading.Value:F2} {reading.Unit}");
                if (reading.Value < O2_MIN)
                    Console.WriteLine("⚠️  OXYGEN ALARM!");
            });

            bus.Subscribe(DeviceType.CO2, reading =>
            {
                Console.WriteLine($"CO₂: {reading.Value:F0} {reading.Unit}");
                if (reading.Value > CO2_MAX)
                    Console.WriteLine("⚠️  CO₂ ALARM!");
            });

            Console.WriteLine("Press Ctrl+C to stop...");
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
