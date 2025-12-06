using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProjectTemplate.API.LifeSupportSystems;
using MyProjectTemplate.API.SubMovement;

namespace Submarine_SCADA_HMI.Tests.LifeSupportSystemTests
{
    // Simple fake movement just for tests
    internal class FakeMovement : IMovement
    {
        private double _z;

        public FakeMovement(double initialDepthMeters)
        {
            _z = initialDepthMeters;
        }

        public double GetPosZ() => _z;
        public void SetDepth(double depthMeters) => _z = depthMeters;

        // The rest of IMovement's members 'don't matter for external pressure tests

        public double GetPosX() => 0;
        public double GetPosY() => 0;
        public double GetSpeed() => 0;

        public void changeThrust(double value) { }
        public void changePitch(double value) { }
        public void changeRudder(double value) { }
        public void changeBallast(double value) { }
        public void Power(bool on) { }
        public void RunStart() { }
        public void RunStop() { }
        public void TestingRunStartOnce() { }
    }

    [TestClass]
    [TestCategory("ExternalPressureMonitor")]
    public class ExternalPressureMonitorTests
    {
        [TestMethod]
        public void ExternalPressure_SurfaceDepth_1Bar()
        {
            // Arrange – at surface / shallow depth (0 m)
            var movement = new FakeMovement(0.0);
            var monitor = new ExternalPressureMonitor(movement);

            // Act
            var pressure = monitor.SampleSensorForTest();

            // Assert
            Assert.AreEqual(pressure, 1.0, 0.0001,
                "At depth 0 m, external pressure should clamp to 1 bar.");
        }

        [TestMethod]
        public void ExternalPressure_ShallowDepth_1Bar()
        {
            // Arrange – 5 m depth => 0.5 bar, but clamped to 1
            var movement = new FakeMovement(-5.0); // sign doesn't matter; we use Abs
            var monitor = new ExternalPressureMonitor(movement);

            // Act
            var pressure = monitor.SampleSensorForTest();

            // Assert
            Assert.AreEqual(pressure, 1.0, 0.0001,
                "At depth less than 10 m, pressure should still clamp to 1 bar.");
        }

        [TestMethod]
        public void ExternalPressure_DeepDepth_TracksDepthDividedBy10()
        {
            // Arrange – 300 m depth => 30 bar
            var movement = new FakeMovement(-300.0);
            var monitor = new ExternalPressureMonitor(movement);

            // Act
            var pressure = monitor.SampleSensorForTest();

            // Assert
            Assert.AreEqual(pressure, 30.0, 0.0001,
                "At 300m depth, external pressure should be ~30 bar.");
        }

        [TestMethod]
        public void ExternalPressure_UpdatesWhenDepthChanges()
        {
            // Arrange – start near surface
            var movement = new FakeMovement(-5.0);
            var monitor = new ExternalPressureMonitor(movement);

            var first = monitor.SampleSensorForTest();   // should be clamped to 1.0

            // Act – go deep
            movement.SetDepth(-100.0);                  // 100m => 10 bar
            var second = monitor.SampleSensorForTest();

            // Assert
            Assert.AreEqual(first, 1.0, 0.0001,
                "Initial shallow depth should be clamped to 1 bar.");
            Assert.AreEqual(second, 10.0, 0.0001,
                "After depth change to 100m, pressure should be 10 bar.");
        }

        [TestMethod]
        public void ExternalPressure_handlesPositiveDepthInput()
        {
            // Arrange – change in code to deliver depth as positive value should still work
            var movement = new FakeMovement(20.0); // +20 m
            var monitor = new ExternalPressureMonitor(movement);

            // Act
            var pressure = monitor.SampleSensorForTest();

            // Assert
            Assert.AreEqual(pressure, 2.0, 0.0001,
                "At positive depths (above sea level), pressure should clamp to 1 bar.");
        }
    }
}