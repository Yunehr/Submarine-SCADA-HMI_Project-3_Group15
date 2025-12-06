using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProjectTemplate.API.LifeSupportSystems;

namespace Submarine_SCADA_HMI.Tests.LifeSupportSystemTests
{
    [TestClass]
    [TestCategory("FuelRodMonitor")]
    public class FuelRodMonitorTests
    {
        // default fuel rod integrity test
        [TestMethod]
        public void FuelRodMonitor_InitialIntegrity_100Percent()
        {
            // Arrange
            var fuelRodMonitor = new FuelRodMonitor();  // initial integrity 100%

            // Act
            var initialIntegrity = fuelRodMonitor.SampleSensorForTest();    // sample lowers integrity to 99.9

            // Assert
            Assert.AreEqual(99.9, initialIntegrity, "Initial fuel rod integrity should be 100%");
        }

        // fuel rod integrity decrease test
        [TestMethod]
        public void FuelRodMonitor_FuelRodIntegrityDecreases_OverTime()
        {
            // Arrange
            var fuelRodMonitor = new FuelRodMonitor();  // initial integrity 100%
            double initialIntegrity;
            double currentIntegrity;

            // Act
            initialIntegrity = fuelRodMonitor.SampleSensorForTest();    // sample lowers integrity to 99.9
            currentIntegrity = initialIntegrity;  // initializes currentIntegrity to avoid compiler error

            // Simulate multiple samples to observe integrity decrease
            for (int i = 0; i < 10; i++)    // integrity should decrease by 0.1% each sample for a total of 1%
                currentIntegrity = fuelRodMonitor.SampleSensorForTest();

            // Assert
            Assert.AreEqual(99.9, initialIntegrity, 0.0001, "Fuel rod integrity should start at 100%.");
            Assert.AreEqual(98.9, currentIntegrity, 0.0001, "Fuel rod integrity should decrease to 99% after 10 samples.");
        }

        // Fuel rod integrity drop to 20% test
        [TestMethod]
        public void FuelRodMonitor_FuelRodIntegrityDrop_SetsIntegrityTo20Percent()
        {
            // Arrange
            var fuelRodMonitor = new FuelRodMonitor();  // initial integrity 100%
            double initialIntegrity;
            double integrityAfterDrop;

            // Act
            initialIntegrity = fuelRodMonitor.SampleSensorForTest();    // sample lowers integrity to 99.9
            fuelRodMonitor.FuelRodIntegrityDrop();                           // set integrity to 20.0
            integrityAfterDrop = fuelRodMonitor.SampleSensorForTest();   // should return 19.9 because of sampling decrease

            // Assert
            Assert.AreEqual(99.9, initialIntegrity, 0.0001, "Initial fuel rod integrity should be 100%.");
            Assert.AreEqual(19.9, integrityAfterDrop, 0.0001, "Fuel rod integrity should be set to 20% after drop.");
        }

        // SCRAM fuel rod integrity test
        [TestMethod]
        public void FuelRodMonitor_SCRAMFuelRodIntegrity_IntegritySetTo100Percent()
        {
            // Arrange
            var fuelRodMonitor = new FuelRodMonitor();  // initial integrity 100%
            double initialIntegrity;
            double integrityBeforeSCRAM;
            double integrityAfterSCRAM;

            // Act
            initialIntegrity = fuelRodMonitor.SampleSensorForTest();    // sample lowers integrity to 99.9
            integrityBeforeSCRAM = initialIntegrity;

            // Simulate some samples to decrease integrity
            for (int i = 0; i < 5; i++)    // decrease by 0.1% each sample for a total of 0.5%
                integrityBeforeSCRAM = fuelRodMonitor.SampleSensorForTest();

            fuelRodMonitor.SCRAMFuelRodIntegrity();
            integrityAfterSCRAM = fuelRodMonitor.SampleSensorForTest();

            // Assert
            Assert.AreEqual(99.4, 0.0001, integrityBeforeSCRAM, "Fuel rod integrity should be approximately 99.4% before SCRAM after 5 samples.");
            Assert.AreEqual(99.9, 0.0001, integrityAfterSCRAM, "Fuel rod integrity should be set to 100% after SCRAM.");
        }

    }
}