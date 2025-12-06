using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProjectTemplate.API.LifeSupportSystems;

namespace Submarine_SCADA_HMI.Tests.LifeSupportSystemTests
{
    [TestClass]
    [TestCategory("CoolantMonitor")]
    public class CoolantMonitorTests
    {
        // default coolant level test
        [TestMethod]
        public void CoolantMonitor_InitialLevel_100Percent()
        {
            // Arrange
            var coolantMonitor = new CoolantMonitor();  // initial level 100%

            // Act
            var initialLevel = coolantMonitor.SampleSensorForTest();    // sample lowers level to 99.8

            // Assert
            Assert.AreEqual(99.8, initialLevel, "Initial coolant level should be 100%");
        }

        // coolant level decrease test
        [TestMethod]
        public void CoolantMonitor_CoolantLevelDecreases_OverTime()
        {
            // Arrange
            var coolantMonitor = new CoolantMonitor();  // initial level 100%
            double initialLevel;
            double currentLevel;

            // Act
            initialLevel = coolantMonitor.SampleSensorForTest();    // sample lowers level to 99.8
            currentLevel = initialLevel;  // initializes currentLevel to avoid compiler error
            
            // Simulate multiple samples to observe level decrease
            for (int i = 0; i < 10; i++)    // level should decrease by 0.2% each sample for a total of 2%
                currentLevel = coolantMonitor.SampleSensorForTest();

            // Assert
            Assert.AreEqual(99.8, initialLevel, 0.0001, "Coolant level should start at 100%.");
            Assert.AreEqual(97.8, currentLevel, 0.0001, "Coolant level should decrease to 98% after 10 samples.");
        }

        // SCRAM coolant level test
        [TestMethod]
        public void CoolantMonitor_SCRAMCoolantLevel_LevelSetTo100Percent()
        {
            // Arrange
            var coolantMonitor = new CoolantMonitor();  // initial level 100%
            double initialLevel;
            double levelBeforeSCRAM;
            double levelAfterSCRAM;

            // Act
            initialLevel = coolantMonitor.SampleSensorForTest();    // sample lowers level to 99.8
            levelBeforeSCRAM = initialLevel;

            // Simulate some samples to decrease level
            for (int i = 0; i < 5; i++)    
                levelBeforeSCRAM = coolantMonitor.SampleSensorForTest();

            coolantMonitor.SCRAMCoolantLevel();
            levelAfterSCRAM = coolantMonitor.SampleSensorForTest();

            // Assert
            Assert.AreEqual(98.8, 0.0001, levelBeforeSCRAM, "Coolant level should be approximately 99% before SCRAM after 5 samples.");
            Assert.AreEqual(100.0, levelAfterSCRAM, "Coolant level should be set to 100% after SCRAM.");
        }
    }
    
}