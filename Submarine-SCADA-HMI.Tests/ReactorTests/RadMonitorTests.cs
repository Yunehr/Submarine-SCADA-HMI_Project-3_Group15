using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProjectTemplate.API.LifeSupportSystems;

namespace Submarine_SCADA_HMI.Tests.LifeSupportSystemTests
{
    [TestClass]
    [TestCategory("RadMonitor")]
    public class RadMonitorTests
    {
        // default radiation level test (within expected fluctuation range)
        [TestMethod]
        public void RadMonitor_InitialRadiationLevel_WithinExpectedRange()
        {
            // Arrange
            var radMonitor = new RadMonitor();  // initial radiation level 0.05 Rads
            double initialLevel;

            // Act
            initialLevel = radMonitor.SampleSensorForTest();    // sample may vary slightly

            // Assert
            Assert.IsTrue(initialLevel >= 0.04 && initialLevel <= 0.06, "Initial radiation level should be within expected range (0.04 to 0.06 Rads)");
        }

        // SCRAM radiation level test
        [TestMethod]
        public void RadMonitor_SCRAMRadiationLevel_SetsLevelToPoint5Rads()
        {
            // Arrange
            var radMonitor = new RadMonitor();  // initial radiation level 0.05 Rads
            double levelAfterSCRAM;

            // Act
            radMonitor.SCRAMRadiationLevel();                           // SCRAM the radiation level
            levelAfterSCRAM = radMonitor.SampleSensorForTest();   // should return 0.5 Rads

            // Assert
            Assert.AreEqual(0.5, levelAfterSCRAM, 0.0001, "After SCRAM, radiation level should be set to 0.5 Rads.");
        }

    }
}