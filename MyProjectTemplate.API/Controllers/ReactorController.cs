using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
namespace MyProjectTemplate.API.LifeSupportSystems
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReactorController : ControllerBase
    {
        private readonly IEventBus _bus;
        private readonly Dictionary<Guid, string> _areaNames;
        private readonly Dictionary<string, IDevice> _devices;

        // Fuel Rod Integrity Min Threshold
        private const double FUEL_ROD_INTEGRITY_MIN = 50.0;
        private const double FUEL_ROD_INTEGRITY_CRITICAL = 30.0;

        public ReactorController(
            IEventBus bus,
            Dictionary<Guid, string> areaNames,
            Dictionary<string, IDevice> devices)
        {
            _bus = bus;
            _areaNames = areaNames;
            _devices = devices;
        }

        public void SetupSubscriptions()
        {
            // Reactor = ReactOutput
            _bus.Subscribe(DeviceType.Reactor, reading =>
            {
                var label = GetLabel(reading.DeviceId, "Reactor Output Sensor");
                Console.WriteLine($"{label}: {reading.Value:F2} {reading.Unit}");

            });

            _bus.Subscribe(DeviceType.Coolant, reading =>
            {
                var label = GetLabel(reading.DeviceId, "Coolant Sensor");
                Console.WriteLine($"{label}: {reading.Value:F2} {reading.Unit}");

            });

            _bus.Subscribe(DeviceType.FuelRod, reading =>
            {
                var label = GetLabel(reading.DeviceId, "FuelRod Sensor");
                Console.WriteLine($"{label} O₂: {reading.Value:F2} {reading.Unit}");

            });

            _bus.Subscribe(DeviceType.Rad, reading =>
            {
                var label = GetLabel(reading.DeviceId, "Rad Sensor");
                Console.WriteLine($"{label}: {reading.Value:F2} {reading.Unit}");

            });

            _bus.Subscribe(DeviceType.Battery, reading =>
            {
                var label = GetLabel(reading.DeviceId, "Reactor Battery Sensor");
                Console.WriteLine($"{label}: {reading.Value:F2} {reading.Unit}");

            });

            // Does reactor return a temp?
            /*
            _bus.Subscribe(DeviceType.Temperature, reading =>
            {
                var label = GetLabel(reading.DeviceId, "Pressure Sensor");
                Console.WriteLine($"{label}: {reading.Value:F2} {reading.Unit}");

            });
            */
        }

        private string GetLabel(Guid deviceId, string fallback)
        {
            return _areaNames.TryGetValue(deviceId, out var name)
                ? name
                : fallback;
        }


        // TODO: Reactor Buttons
        /*
        [HttpPost("scrubber")]
        public IActionResult ActivateScrubber()
        {
            // Reset CO2 level
            var co2Monitor = (Co2Monitor)_devices["CO2"];
            co2Monitor.resetCo2Level();

            // Simulate pressure drop
            var pressureMonitor = (PressureMonitor)_devices["IntPressure"];
            pressureMonitor.PressureDrop();

            // Halve oxygen level
            var oxygenMonitor = (OxygenMonitor)_devices["O2"];
            oxygenMonitor.HalveOxygenLevel();

            return Ok(new { status = "Scrubber activated" });
        }
        */

        // TODO: This is in Lifesupport but should probably be here
        /*
        [HttpPost("SCRAM Reactor")]
        public IActionResult SCRAMReactor()
        {
            // Reset reactor output to 0
            var reactorOutputMonitor = (ReactorOutputMonitor)_devices["ReactorOutput"];
            reactorOutputMonitor.SCRAMReactorOutput();

            var coolantMonitor = (CoolantMonitor)_devices["Coolant"];
            coolantMonitor.SCRAMCoolantLevel();

            var fuelRodMonitor = (FuelRodMonitor)_devices["FuelRod"];
            fuelRodMonitor.SCRAMFuelRodIntegrity();

            var radiationMonitor = (RadMonitor)_devices["Radiation"];
            radiationMonitor.SCRAMRadiationLevel();

            var batteryMonitor = (BatteryMonitor)_devices["Battery"];
            batteryMonitor.SCRAMBatteryDisconnect();

            return Ok(new { status = "Reactor SCRAM activated" });
        }
        */

        // TODO: Reactor Output Readings
        /*
        [HttpGet("ReactorOutput")]
        public IActionResult GetReactorOutput()
        {
          
        }
        */

    }

    // Not sure what this was for but i left it here for yall
    // public record DeviceCommand(DeviceType DeviceType, string Action);
}
