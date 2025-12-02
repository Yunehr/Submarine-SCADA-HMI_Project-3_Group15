using Microsoft.AspNetCore.Mvc;
using MyProjectTemplate.API.Data;
using MyProjectTemplate.API.Models;
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
        private const double O2_MIN = 21.0;
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

        public LifeSupportController(IEventBus bus,
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
            var co2Monitor = (Co2Monitor)_devices["CO2"];
            co2Monitor.resetCo2Level();

            return Ok(new { status = "Scrubber activated" });
        }

        [HttpPost("OxygenGeneration")]
        public IActionResult OxygenGeneration()
        {
            var oxygenMonitor = (OxygenMonitor)_devices["OxygenMonitor"];
            oxygenMonitor.resetOxygenLevel();

            return Ok(new { status = "Oxygen Generation activated" });
        }

        [HttpPost("scram")]
        public IActionResult Scram()
        {
            // shutdown logic here
            //...
            return Ok(new { status = "SCRAM executed" });
        }


        [HttpPost("Pressurize")]
        public IActionResult Pressurize()
        {
            // Reset internal pressure level
            var pressureMonitor = (PressureMonitor)_devices["IntPressure"];
            pressureMonitor.resetPressureLevel();

            // decrease air reserve by 10%
            var AirReserveMonitor = (AirReserveMonitor)_devices["AirReserveMonitor"];
            AirReserveMonitor.AirReserveDropBy10();

            return Ok(new { status = "Pressurization activated" });
        }

        [HttpPost("ReplenishAirReserve")]
        public IActionResult ResetAirReserve()
        {
            // Reset air reserve level
            var AirReserveMonitor = (AirReserveMonitor)_devices["AirReserveMonitor"];
            AirReserveMonitor.resetAirReserveLevel();

            return Ok(new { status = "Air Reserve Reset activated" });
        }


        [HttpGet("{deviceKey}")]
        public IActionResult GetLatest(string deviceKey)
        {
            if (_devices.TryGetValue(deviceKey, out var device) &&
                _bus.TryGetLatest(device.Id, out var reading))
            {
                return Ok(reading);
            }
            return NotFound();
        }

        //[HttpGet("alarms")]
        //public IActionResult GetAlarms()
        //{
        //    //var alarms = _db.SubAlarmsData.ToList(); // needs fixing _db does not exist in current context
        //    return Ok(alarms);
        //}

    }

    public record DeviceCommand(DeviceType DeviceType, string Action);
}
