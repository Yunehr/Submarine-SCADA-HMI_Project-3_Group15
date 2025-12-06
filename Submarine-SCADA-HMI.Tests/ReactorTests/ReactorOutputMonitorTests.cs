using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProjectTemplate.API.LifeSupportSystems; 

namespace Submarine_SCADA_HMI.Tests.LifeSupportSystemTests
{
    [TestClass]
    [TestCategory("ReactorOutputMonitor")]
    public class ReactorOutputMonitorTests
    {
        // default reactor output test (within expected fluctuation range)
        [TestMethod]
        public void ReactorOutputMonitor_InitialOutputLevel_WithinExpectedRange()
        {
            // Arrange
            var reactorMonitor = new ReactorOutputMonitor();  // initial reactor output 500.0 Megawatts
            double initialOutput;

            // Act
            initialOutput = reactorMonitor.SampleSensorForTest();    // sample may vary slightly

            // Assert
            Assert.IsTrue(initialOutput >= 498.0 && initialOutput <= 502.0, "Initial reactor output should be within expected range (498.0 to 502.0 Megawatts)");
        }

        // SCRAM reactor output test
        [TestMethod]
        public void ReactorOutputMonitor_SCRAMReactorOutput_SetsOutputToZero()
        {
            // Arrange
            var reactorMonitor = new ReactorOutputMonitor();  // initial reactor output 500.0 Megawatts
            double outputAfterSCRAM;

            // Act
            reactorMonitor.SCRAMReactorOutput();                           // SCRAM the reactor output
            outputAfterSCRAM = reactorMonitor.SampleSensorForTest();   // should return 0.0 Megawatts

            // Assert
            Assert.AreEqual(0.0, outputAfterSCRAM, 0.0001, "After SCRAM, reactor output should be set to 0.0 Megawatts.");
        }
    }
}