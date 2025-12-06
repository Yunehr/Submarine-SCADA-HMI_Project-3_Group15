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


// Old tests for CO2Monitor - Not in use anymore. Keeping for reference
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.IO;
//using DeviceMonitor;

//namespace Submarine_SCADA_HMI.Tests;

//[TestClass]
//public class CO2MonitorTests
//{
//    [TestMethod]
//    public void SetCO2()
//    {
//        // Arrange
//        var co2Monitor = new CO2Monitor();
//        co2Monitor.PpmPercentage = 0.04;

//        // Act
//        var result = co2Monitor.PpmPercentage;

//        // Assert
//        Assert.AreEqual(0.04, result);
//    }

//    [TestMethod]
//    public void SetNegativeCO2_ReturnZero() // CO2 levels should have a floor of zero
//    {
//        // Arrange
//        var co2Monitor = new CO2Monitor();
//        co2Monitor.PpmPercentage = -0.07;

//        // Act
//        var result = co2Monitor.PpmPercentage;

//        // Assert
//        Assert.AreEqual(0.0, result);
//    }

//    [TestMethod]
//    public void SetCO2PercentageAbove100_Return100()
//    {
//        // Arrange
//        var co2Monitor = new CO2Monitor();
//        co2Monitor.PpmPercentage = 150.0;    // composition above 100% is not possible

//        // Act
//        var result = co2Monitor.PpmPercentage;

//        // Assert
//        Assert.AreEqual(100.0, result);
//    }

//    [TestMethod]
//    public void TakeReadingCO2FromFile()
//    {
//        // Arrange
//        var co2Monitor = new CO2Monitor();
//        co2Monitor.TakeCO2Reading("TestData/TestCO2Data.txt");

//        // Act
//        var result = co2Monitor.PpmPercentage;

//        // Assert
//        Assert.AreEqual(4.0, result, 1e-9);
//    }

//    [TestMethod]
//    public void TakeReadingNegativeCO2FromFile_ReturnZero()
//    {
//        // Arrange
//        var co2Monitor = new CO2Monitor();
//        co2Monitor.UpdateCO2FromFile("TestData/TestCO2Data.txt"); // first line is 4.0
//        co2Monitor.UpdateCO2FromFile("TestData/TestCO2Data.txt"); // second line is -4.0

//        // Act
//        var result = co2Monitor.PpmPercentage;

//        // Assert
//        Assert.AreEqual(0.0, result, 1e-9);
//    }

//    [TestMethod]
//    public void TakeReadingCO2Above100FromFile_Return100()
//    {
//        // Arrange
//        var co2Monitor = new CO2Monitor();
//        co2Monitor.TakeCO2Reading("TestData/TestCO2Data.txt"); // first line is 4.0
//        co2Monitor.TakeCO2Reading("TestData/TestCO2Data.txt"); // second line is -4.0
//        co2Monitor.TakeCO2Reading("TestData/TestCO2Data.txt"); // third line is 120

//        // Act
//        var result = co2Monitor.PpmPercentage;

//        // Assert
//        Assert.AreEqual(100.0, result, 1e-9);
//    }

//    [TestMethod]
//    public void LogCO2ReadingToFile_AppendsToFile()
//    {
//        // Arrange
//        var co2Monitor = new CO2Monitor();
//        string fp = "TestData/TestLogCO2Data.txt";
//        double reading = 21.0;
//        co2Monitor.PpmPercentage = reading;

//        // Act
//        co2Monitor.LogCo2Reading(fp);
//        var lastLine = File.ReadLines(fp).Last();

//        //Assert
//        Assert.AreEqual(reading.ToString(), lastLine);

//    }
//}
