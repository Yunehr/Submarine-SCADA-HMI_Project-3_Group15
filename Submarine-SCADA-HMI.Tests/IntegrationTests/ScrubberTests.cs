using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProjectTemplate.API;
using MyProjectTemplate.API.LifeSupportSystems;
using Microsoft.AspNetCore.Mvc;

namespace Submarine_SCADA_HMI.Tests.LifeSupportSystemTests
{
    // integration test between LifeSupportController and various monitors when activating the scrubber
    [TestClass]
    [TestCategory("ScrubberIntegration")]
    public class LifeSupportControllerIntegrationTests
    {
        private IEventBus _bus = null!;
        private OxygenMonitor _o2 = null!;
        private Co2Monitor _co2 = null!;
        private AirReserveMonitor _air = null!;
        private PressureMonitor _intPressure = null!;
        private PressureMonitor _exPressure = null!;
        private TemperatureMonitor _temp = null!;
        private HumidityMonitor _humidity = null!;
        private LifeSupportController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _bus = new EventBus();

            _o2          = new OxygenMonitor();
            _co2         = new Co2Monitor();
            _air         = new AirReserveMonitor();
            _intPressure = new PressureMonitor();
            _exPressure  = new PressureMonitor();
            _temp        = new TemperatureMonitor();
            _humidity    = new HumidityMonitor();

            var areaNames = new Dictionary<Guid, string>
            {
                [_o2.Id]          = "O2 Main Cabin",
                [_co2.Id]         = "CO2 Main Cabin",
                [_air.Id]         = "Air Reserve Tank",
                [_intPressure.Id] = "Internal Pressure",
                [_exPressure.Id]  = "External Pressure",
                [_temp.Id]        = "Main Cabin Temperature",
                [_humidity.Id]    = "Main Cabin Humidity"
            };

            var devices = new Dictionary<string, IDevice>
            {
                ["O2"]          = _o2,
                ["CO2"]         = _co2,
                ["Air"]         = _air,
                ["IntPressure"] = _intPressure,
                ["ExPressure"]  = _exPressure,
                ["Temperature"] = _temp,
                ["Humidity"]    = _humidity
            };

            _controller = new LifeSupportController(_bus, areaNames, devices);
        }

        [TestMethod]
        public void Scrubber_Integration_UpdatesRelatedMonitors()
        {
            // Arrange: set some non-default values so we can see the change clearly.
            _co2.resetCo2Level();        // -> 0
            _o2.resetOxygenLevel();      // -> 21
            _intPressure.resetPressureLevel(); // -> 1.0

            // Take a baseline sample (matches how your unit tests work)
            var baseO2    = _o2.SampleSensorForTest();
            var baseCO2   = _co2.SampleSensorForTest();
            var basePress = _intPressure.SampleSensorForTest();

            // Act
            var result = _controller.ActivateScrubber();

            // Re-sample after the controller method has changed internal fields
            var newO2    = _o2.SampleSensorForTest();
            var newCO2   = _co2.SampleSensorForTest();
            var newPress = _intPressure.SampleSensorForTest();

            // Assert
            Assert.AreEqual(21.0, baseO2,  0.0001, "Baseline oxygen should be ~21%");
            Assert.AreEqual(0.0,  baseCO2, 0.0001, "Baseline CO2 should be 0");
            Assert.AreEqual(1.0,  basePress, 0.0001, "Baseline internal pressure should be 1.0 bar");

            Assert.AreEqual(10.49, newO2, 0.0001, "Scrubber should halve oxygen level.");   // 21 / 2 = 10.5 approx but the decrease happens after sampling so we get 10.49
            Assert.AreEqual(0.0,        newCO2, 0.0001, "Scrubber should reset CO2 to 0.");
            Assert.AreEqual(0.5,        newPress, 0.0001, "Scrubber should drop internal pressure to 0.5 bar.");

            Assert.AreEqual("Scrubber activated", 
                (result as OkObjectResult)?.Value?.GetType().GetProperty("status")?.GetValue((result as OkObjectResult)!.Value));
        }
    }
}