using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProjectTemplate.API.LifeSupportSystems;

namespace Submarine_SCADA_HMI.Tests.LifeSupportSystemTests
{
    [TestClass]
    [TestCategory("CO2Monitor")]
    public class CO2MonitorTests
    {
        [TestMethod]
        public void IncreaseBy5EachCall()
        {
            // Arrange
            var monitor = new Co2Monitor();
            monitor.resetCo2Level(); // ensure we start at 0

            // Act
            var first = monitor.SampleSensorForTest();  // should return 0, then set internal to 5
            var second = monitor.SampleSensorForTest(); // should return 5 
            var third = monitor.SampleSensorForTest();  // should return 10

            // Assert
            Assert.AreEqual(first, 0.0, 0.0001, "First sample should be 0 ppm");
            Assert.AreEqual(second, 5.0, 0.0001, "Second sample should be 5 ppm");
            Assert.AreEqual(third, 10.0, 0.0001, "Third sample should be 10 ppm");
        }

        [TestMethod]
        public void ResetCo2Level_ResetsAfterSampling()
        {
            // Arrange
            var monitor = new Co2Monitor();
            monitor.resetCo2Level(); // start at 0

            // Act
            var first = monitor.SampleSensorForTest();  // 0 (internal -> 5)
            var second = monitor.SampleSensorForTest(); // 5 (internal -> 10)
            monitor.resetCo2Level();                     // back to 0
            var afterReset = monitor.SampleSensorForTest(); // 0 again

            // Assert
            Assert.AreEqual(0.0, first, 0.0001);
            Assert.AreEqual(second, 5.0, 0.0001);
            Assert.AreEqual(0.0, afterReset, 0.0001, "After reset, first sample should be 0 ppm again");
        }

        [TestMethod]
        public void CO2Spike_SetsLevelTo1200()
        {
            // Arrange
            var monitor = new Co2Monitor();
            monitor.resetCo2Level(); // start at 0

            // Act
            var first = monitor.SampleSensorForTest();  // 0 (internal -> 5)
            monitor.Co2Spike();                         // set to 1200

            // Assert
            Assert.AreEqual(0.0, first, 0.0001, "First sample should be 0 ppm");
            Assert.AreEqual(1200.0, monitor.SampleSensorForTest(), 0.0001, "After spike, sample should be 1200 ppm");
        }
    }
}