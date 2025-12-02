using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyProjectTemplate.API.Services;
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
        private readonly DeviceThresholds _thresh; // Adding the thresholds 
        private readonly ThresholdsHandlers _handlerService;

        // Pressure thresholds (cause its not figured out yet)
        const double INTERNAL_PRESSURE_MAX = 1.2;
        const double INTERNAL_PRESSURE_MIN = 0.8;
        const double EXTERNAL_PRESSURE_UPPER_WARNING = 24.0;
        const double EXTERNAL_PRESSURE_MAX = 36.0;
        const double EXTERNAL_PRESSURE_MIN = 0.5;

        public LifeSupportController(
            IEventBus bus,
            Dictionary<Guid, string> areaNames,
            Dictionary<string, IDevice> devices,
            IOptions<DeviceThresholds> thresholds
            )
        {
            _bus = bus;
            _areaNames = areaNames;
            _devices = devices;
            _thresh = thresholds.Value;
            _handlerService = new ThresholdsHandlers(thresholds);
        }

        public void SetupSubscriptions()
        {
            // Convenience vars so we don’t constantly index the dictionary
            // var intPressure = _devices["IntPressure"];
            // var exPressure  = _devices["ExPressure"];
            int alarm = 0;

            _bus.Subscribe(DeviceType.Oxygen, reading =>
            {
                ThresholdSet? t = _thresh.Oxygen;
                var label = GetLabel(reading.DeviceId, "O2 Sensor");
                Console.WriteLine($"{label}: {reading.Value:F2} {reading.Unit}");

                alarm = _handlerService.HandleReading(reading);
            });

            _bus.Subscribe(DeviceType.CO2, reading =>
            {
                var label = GetLabel(reading.DeviceId, "CO2 Sensor");
                Console.WriteLine($"{label}: {reading.Value:F2} {reading.Unit}");

                alarm = _handlerService.HandleReading(reading);
            });

            _bus.Subscribe(DeviceType.AirReserve, reading =>
            {
                var label = GetLabel(reading.DeviceId, "Air Reserve Tank Sensor");
                Console.WriteLine($"{label} O₂: {reading.Value:F2} {reading.Unit}");

                alarm = _handlerService.HandleReading(reading);
            });

            /*
            _bus.Subscribe(DeviceType.Pressure, reading =>
            {
                var label = GetLabel(reading.DeviceId, "Pressure Sensor");
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
            }
            );
            */
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
            var co2Monitor = (Co2Monitor)_devices["CO2Monitor"];
            co2Monitor.resetCo2Level(); 

            // Simulate pressure drop
            var pressureMonitor = (PressureMonitor)_devices["IntPressure"];
            pressureMonitor.PressureDrop();

            // Halve oxygen level
            var oxygenMonitor = (OxygenMonitor)_devices["OxygenMonitor"];
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
            var AirReserveMonitor = (AirReserveMonitor)_devices["AirReserveMonitor"];
            AirReserveMonitor.AirReserveDropBy10();

            return Ok(new { status = "Pressurization activated" });
        }

        [HttpPost("OxygenGeneration")]
        public IActionResult OxygenGeneratio()
        {
            // Reset oxygen level
            var oxygenMonitor = (OxygenMonitor)_devices["OxygenMonitor"];
            oxygenMonitor.resetOxygenLevel();

            return Ok(new { status = "Oxygen Generation activated" });
        }

        [HttpPost("ReplenishAirReserve")]
        public IActionResult ResetAirReserve()
        {
            // Reset air reserve level
            var AirReserveMonitor = (AirReserveMonitor)_devices["AirReserveMonitor"];
            AirReserveMonitor.resetAirReserveLevel();

            return Ok(new { status = "Air Reserve Reset activated" });
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
    }

    public record DeviceCommand(DeviceType DeviceType, string Action);
}
