using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MyProjectTemplate.API.LifeSupportSystems
{
    [ApiController]
    [Route("api/[controller]")]
    public class LifeSupportController : ControllerBase
    {
        private readonly IEventBus _bus;
        private readonly Dictionary<Guid, string> _areaNames;
        private readonly Dictionary<string, IDevice> _devices;

        // Alarm thresholds
        private const double O2_MIN = 19.5;
        private const double O2_MAX = 23.5;
        private const double CO2_MAX = 390;
        private const double AIR_RESERVE_MIN = 40.0;
        private const double INT_PRESSURE_MAX = 1.2;
        private const double INT_PRESSURE_MIN = 0.8;
        private const double EX_PRESSURE_UPPER_WARNING = 24.0;
        private const double EX_PRESSURE_MAX = 36.0;
        private const double EX_PRESSURE_MIN = 0.5;
        private const double TEMP_MAX = 27.0;
        private const double TEMP_MIN = 15.0;
        private const double HUMIDITY_MAX = 60.0;
        private const double HUMIDITY_MIN = 20.0;
        // Fuel Rod Integrity Min Threshold
        private const double FUEL_ROD_INTEGRITY_MIN = 50.0;
        private const double FUEL_ROD_INTEGRITY_CRITICAL = 30.0;

        public LifeSupportController(
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
            // Convenience vars so we don’t constantly index the dictionary
            var intPressure = _devices["IntPressure"];
            var exPressure  = _devices["ExPressure"];

            _bus.Subscribe(DeviceType.Oxygen, reading =>
            {
                var label = GetLabel(reading.DeviceId, "Unknown O2 Sensor");
                Console.WriteLine($"{label}: {reading.Value:F2} {reading.Unit}");

                if (reading.Value < O2_MIN)
                {
                    Console.WriteLine($"Oxygen ALARM in {label} - BELOW SAFE MINIMUM!");
                }
            });

            _bus.Subscribe(DeviceType.CO2, reading =>
            {
                var label = GetLabel(reading.DeviceId, "Unknown CO2 Sensor");
                Console.WriteLine($"{label}: {reading.Value:F2} {reading.Unit}");

                if (reading.Value > CO2_MAX)
                {
                    Console.WriteLine($"{label} - ABOVE SAFE MAXIMUM!");
                }
            });

            _bus.Subscribe(DeviceType.AirReserve, reading =>
            {
                var label = GetLabel(reading.DeviceId, "Unknown Air Reserve Tank Sensor");
                Console.WriteLine($"{label} O₂: {reading.Value:F2} {reading.Unit}");

                if (reading.Value < AIR_RESERVE_MIN)
                {
                    Console.WriteLine($"{label} - BELOW SAFE MINIMUM!");
                }
            });

            _bus.Subscribe(DeviceType.Pressure, reading =>
            {
                var label = GetLabel(reading.DeviceId, "Unknown Pressure Sensor");
                Console.WriteLine($"{label}: {reading.Value:F2} {reading.Unit}");

                if (reading.DeviceId == intPressure.Id)
                {
                    if (reading.Value > INT_PRESSURE_MAX)
                        Console.WriteLine($"{label} - ABOVE SAFE MAXIMUM!");
                    else if (reading.Value < INT_PRESSURE_MIN)
                        Console.WriteLine($"{label} - BELOW SAFE MINIMUM!");
                }
                else if (reading.DeviceId == exPressure.Id)
                {
                    if (reading.Value > EX_PRESSURE_MAX)
                        Console.WriteLine($"{label} - ABOVE SAFE MAXIMUM!");
                    else if (reading.Value < EX_PRESSURE_MIN)
                        Console.WriteLine($"{label} - BELOW SAFE MINIMUM!");
                }
            });
        }

        private string GetLabel(Guid deviceId, string fallback)
        {
            return _areaNames.TryGetValue(deviceId, out var name)
                ? name
                : fallback;
        }

        public void CO2SpikeScenaario()
        {
            var co2Monitor = (Co2Monitor)_devices["CO2Monitor"];
            co2Monitor.Co2Spike();
        }

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

        [HttpPost("Pressurize")]
        public IActionResult Pressurize()
        {
            // Reset internal pressure level
            var pressureMonitor = (PressureMonitor)_devices["IntPressure"];
            pressureMonitor.resetPressureLevel();

            // decrease air reserve by 10%
            var AirReserveMonitor = (AirReserveMonitor)_devices["Air"];
            AirReserveMonitor.AirReserveDropBy10();

            return Ok(new { status = "Pressurization activated" });
        }

        [HttpPost("OxygenGeneration")]
        public IActionResult OxygenGeneratio()
        {
            // Reset oxygen level
            var oxygenMonitor = (OxygenMonitor)_devices["O2"];
            oxygenMonitor.resetOxygenLevel();

            return Ok(new { status = "Oxygen Generation activated" });
        }

        [HttpPost("ReplenishAirReserve")]
        public IActionResult ResetAirReserve()
        {
            // Reset air reserve level
            var AirReserveMonitor = (AirReserveMonitor)_devices["Air"];
            AirReserveMonitor.resetAirReserveLevel();

            return Ok(new { status = "Air Reserve Reset activated" });
        }

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

            // This one requires more refactoring than time permits to get the 
            // different temp devices to be set separately
            // var reactorTempMonitor = (TemperatureMonitor)_devices["ReactorTemp"];
            // reactorTempMonitor.SCRAMReactorTemperature();

            var batteryMonitor = (BatteryMonitor)_devices["Battery"];
            batteryMonitor.SCRAMBatteryDisconnect();

            return Ok(new { status = "Reactor SCRAM activated" });
        }

        [HttpPost("Reactor Critical Scenario")]
        public IActionResult ReactorCriticalScenario()
        {
            // var reactorOutputMonitor = (ReactorOutputMonitor)_devices["ReactorOutput"];
            // reactorOutputMonitor.ReactorOutputSpike();

            // var coolantMonitor = (CoolantMonitor)_devices["Coolant"];
            // coolantMonitor.CoolantLevelDrop();

            var fuelRodMonitor = (FuelRodMonitor)_devices["FuelRod"];
            fuelRodMonitor.FuelRodIntegrityDrop();

            // var radiationMonitor = (RadMonitor)_devices["Radiation"];
            // radiationMonitor.RadiationLevelSpike();

            return Ok(new { status = "Reactor Critical Scenario activated" });
        }

        // Ryan's OG stuff below
        // GET latest reading for a device
        //[HttpGet("{deviceType}")]
        //public IActionResult GetLatest(Guid deviceId)   // I don't want to break this, but idk if we need it
        //{
        //    if (_bus.TryGetLatest(deviceId, out var reading))
        //        return Ok(reading);

        //    return Ok(new { deviceId, value = 1, unit = "N/A" });
        //}

        //// POST a command (e.g. switch toggles)
        //[HttpPost("command")]
        //public IActionResult SendCommand([FromBody] DeviceCommand command)
        //{
        //    // For now, just log or forward to device
        //    Console.WriteLine($"Command received: {command.DeviceType} -> {command.Action}");
        //    return Ok(new { status = "accepted" });
        //}

        // fix attempt
        [HttpGet("Oxygen")]
        public IActionResult GetOxygen()
        {
            var oxygenMonitor = (OxygenMonitor)_devices["O2"];
            if (_bus.TryGetLatest(oxygenMonitor.Id, out var reading))
                return Ok(reading);

            return Ok(new { oxygenMonitor, value = 1, unit = "N/A" });
        }

        [HttpGet("CO2")]
        public IActionResult GetCO2()
        {
            var co2Monitor = (Co2Monitor)_devices["CO2"];
            if (_bus.TryGetLatest(co2Monitor.Id, out var reading))
                return Ok(reading);

            return Ok(new { co2Monitor, value = 1, unit = "N/A" });
        }

        [HttpGet("AirReserve")]
        public IActionResult GetAirReserve()
        {
            var airMonitor = (AirReserveMonitor)_devices["Air"];
            if (_bus.TryGetLatest(airMonitor.Id, out var reading))
                return Ok(reading);

            return NotFound();
        }

        [HttpGet("Pressure")]
        public IActionResult GetPressure()
        {
            var pressureMonitor = (PressureMonitor)_devices["IntPressure"];
            if (_bus.TryGetLatest(pressureMonitor.Id, out var reading))
                return Ok(reading);

            return NotFound();
        }

        [HttpGet("Temperature")]
        public IActionResult GetTemperature()
        {
            var tempMonitor = (TemperatureMonitor)_devices["Temperature"];
            if (_bus.TryGetLatest(tempMonitor.Id, out var reading))
                return Ok(reading);

            return NotFound();
        }

        [HttpGet("Humidity")]
        public IActionResult GetHumidity()
        {
            var humidityMonitor = (HumidityMonitor)_devices["Humidity"];
            if (_bus.TryGetLatest(humidityMonitor.Id, out var reading))
                return Ok(reading);

            return NotFound();
        }

    }

    public record DeviceCommand(DeviceType DeviceType, string Action);
}
