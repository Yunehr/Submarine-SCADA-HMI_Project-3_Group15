using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProjectTemplate.API.LifeSupportSystems;

namespace Submarine_SCADA_HMI.Tests.LifeSupportSystemTests
{
    [TestClass]
    [TestCategory("BatteryMonitor")]
    public class BatteryMonitorTests
    {
        // default charge test
        [TestMethod]
        public void BatteryMonitor_InitialCharge_100Percent()
        {
            // Arrange
            var batteryMonitor = new BatteryMonitor();

            // Act
            var initialCharge = batteryMonitor.SampleSensorForTest();

            // Assert
            Assert.AreEqual(100.0, initialCharge, "Initial battery charge should be 100%");
        }

        // reactor disconnect and battery level decrease test
        [TestMethod]
        public void BatteryMonitor_SCRAMBatteryDisconnect_ChargeDecreases()
        {
            // Arrange
            var batteryMonitor = new BatteryMonitor();
            double initialCharge;
            double currentCharge;

            // Act
            initialCharge = batteryMonitor.SampleSensorForTest();
            currentCharge = initialCharge;  // initializes currentCharge to avoid compiler error
            batteryMonitor.SCRAMBatteryDisconnect();

            // Simulate multiple samples to observe charge decrease
            for (int i = 0; i < 10; i++)    // charge should decrease by 0.5% each sample for a total of 5%
                currentCharge = batteryMonitor.SampleSensorForTest();

            // Assert
            Assert.AreEqual(100.0, initialCharge, "Battery charge should start at 100% after SCRAM.");
            Assert.AreEqual(95.0, currentCharge, "Battery charge should decrease to 90% after 10 samples post-SCRAM.");
        }

    }
}