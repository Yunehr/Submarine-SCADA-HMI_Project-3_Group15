using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProjectTemplate.API.LifeSupportSystems;

namespace Submarine_SCADA_HMI.Tests.LifeSupportSystemTests
{
    [TestClass]
    [TestCategory("AirReserveMonitor")]
    public class AirReserveMonitorTests
    {
        [TestMethod]
        public void AirReserveMonitor_InitializesWithFullReserve()
        {
            // Arrange
            var monitor = new AirReserveMonitor();

            // Act
            var initialReserve = monitor.SampleSensorForTest();

            // Assert
            Assert.AreEqual(initialReserve, 100.0, 0.0001, "Initial air reserve should be 100%");
        }

        [TestMethod]
        public void AirReserveDropBy10_DecreasesReserveBy10Percent()
        {
            // Arrange
            var monitor = new AirReserveMonitor();

            // Act
            monitor.AirReserveDropBy10();
            var first = monitor.SampleSensorForTest();  // should return 90

            // Assert
            Assert.AreEqual(first, 90.0, 0.0001, "After one drop, air reserve should be 90%");
        }

        [TestMethod]
        public void AirReserveDropTo20_SetsReserveTo20Percent()
        {
            // Arrange
            var monitor = new AirReserveMonitor();

            // Act
            monitor.AirReserveDropTo20();
            var reading = monitor.SampleSensorForTest();

            // Assert
            Assert.AreEqual(reading, 20.0, 0.0001, "After drop to 20, air reserve should be 20%");
        }

        [TestMethod]
        public void ResetAirReserveLevel_ResetsToFullAfterDrops()
        {
            // Arrange
            var monitor = new AirReserveMonitor();

            // Act
            monitor.AirReserveDropBy10(); // drop to 90
            var first = monitor.SampleSensorForTest();
            monitor.AirReserveDropTo20(); // drop to 20
            var second = monitor.SampleSensorForTest();
            monitor.resetAirReserveLevel(); // reset to 100
            var afterReset = monitor.SampleSensorForTest();

            // Assert
            Assert.AreEqual(first, 90.0, 0.0001, "After first drop, air reserve should be 90%");
            Assert.AreEqual(second, 20.0, 0.0001, "After drop to 20, air reserve should be 20%");
            Assert.AreEqual(afterReset, 100.0, 0.0001, "After reset, air reserve should be back to 100%");
        }
    }
}