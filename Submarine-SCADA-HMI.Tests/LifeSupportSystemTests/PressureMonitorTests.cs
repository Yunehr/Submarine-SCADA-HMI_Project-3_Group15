using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProjectTemplate.API.LifeSupportSystems;

namespace Submarine_SCADA_HMI.Tests.LifeSupportSystemTests
{
    [TestClass]
    [TestCategory("PressureMonitor")]

    public class PressureMonitorTests
    {
        // default pressure is 1.0 Bar
        [TestMethod]
        public void PressureMonitor_InitializesWithNormalPressure()
        {
            // Arrange
            var monitor = new PressureMonitor();

            // Act
            var initialPressure = monitor.SampleSensorForTest();

            // Assert
            Assert.AreEqual(initialPressure, 1.0, 0.0001, "Initial pressure should be 1.0 Bar");
        }

        // Pressure drop to 0.5 Bar
        [TestMethod]
        public void PressureDrop_SetsPressureToHalfBar()
        {
            // Arrange
            var monitor = new PressureMonitor();

            // Act
            monitor.PressureDrop();
            var reading = monitor.SampleSensorForTest();

            // Assert
            Assert.AreEqual(reading, 0.5, 0.0001, "After pressure drop, pressure should be 0.5 Bar");
        }

        // Reset pressure to 1.0 Bar
        [TestMethod]
        public void ResetPressureLevel_ResetsToNormalAfterDrop()
        {
            // Arrange
            var monitor = new PressureMonitor();

            // Act
            monitor.PressureDrop(); // drop to 0.5
            var first = monitor.SampleSensorForTest();
            monitor.resetPressureLevel(); // reset to 1.0
            var reset = monitor.SampleSensorForTest();

            // Assert
            Assert.AreEqual(first, 0.5, 0.0001, "After drop, pressure should be 0.5 Bar");
            Assert.AreEqual(reset, 1.0, 0.0001, "After reset, pressure should be 1.0 Bar");
        }
    }
}