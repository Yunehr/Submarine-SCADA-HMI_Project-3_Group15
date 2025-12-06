using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProjectTemplate.API.LifeSupportSystems;

namespace Submarine_SCADA_HMI.Tests.LifeSupportSystemTests
{
    [TestClass]
    [TestCategory("OxygenMonitor")]
    public class OxygenMonitorTests
    {
        [TestMethod]
        public void SampleSensor_DecreasesBy001EachCall()
        {
            // Arrange
            var monitor = new OxygenMonitor();
            monitor.resetOxygenLevel(); // make sure we start at 21.0

            // Act
            var first = monitor.SampleSensorForTest();  // should return 21.00, then set internal to 20.99
            var second = monitor.SampleSensorForTest(); // should return 20.99
            var third = monitor.SampleSensorForTest();  // should return 20.98

            // Assert
            Assert.AreEqual(21.00, first, 0.0001, "First sample should return initial level");
            Assert.AreEqual(20.98, second, 0.0001, "Second sample should be 0.01 lower");
            Assert.AreEqual(20.96, third, 0.0001, "Third sample should be 0.02 lower than start");
        }

        [TestMethod]
        public void ResetOxygenLevel_ResetsAfterSampling()
        {
            // Arrange
            var monitor = new OxygenMonitor();
            monitor.resetOxygenLevel(); // start at 21

            // Act
            var first = monitor.SampleSensorForTest();  // 21.00 (internal -> 20.99)
            monitor.resetOxygenLevel();                 // back to 21.0
            var afterReset = monitor.SampleSensorForTest(); // 21.00 again

            // Assert
            Assert.AreEqual(21.00, first, 0.0001);
            Assert.AreEqual(21.00, afterReset, 0.0001, "After reset, first sample should be 21.0 again");
        }

        [TestMethod]
        public void OxygenDropTo15_SetsLevelTo15()
        {
            // Arrange
            var monitor = new OxygenMonitor();
            monitor.resetOxygenLevel(); // start at 21

            // Act
            var first = monitor.SampleSensorForTest();  // 21.00 (internal -> 20.99)
            monitor.OxygenDropTo15();                   // set to 15.0

            // Assert
            Assert.AreEqual(21.00, first, 0.0001, "First sample should be 21.0%");
            Assert.AreEqual(15.0, monitor.SampleSensorForTest(), 0.0001, "After drop, sample should be 15.0%");
        }

        [TestMethod]
        public void OxygenHalveLevel_SetsLevelToHalf()
        {
            // Arrange
            var monitor = new OxygenMonitor();
            monitor.resetOxygenLevel(); // start at 21

            // Act
            var first = monitor.SampleSensorForTest();  // 21.00 (internal -> 20.99)
            monitor.HalveOxygenLevel();                 // halve to 10.49 from current reading of 20.99

            // Assert
            Assert.AreEqual(21.00, first, 0.0001, "First sample should be 21.0%");
            Assert.AreEqual(10.49, monitor.SampleSensorForTest(), 0.0001, "After halving, sample should be 10.5%");
        }
    }
}


// Old Tests for OxygenMonitor - kept for reference but no longer in use
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.IO;
//using DeviceMonitor;


//namespace Submarine_SCADA_HMI.Tests;

//[TestClass]
//public class OxygenMonitorTests
//{
//    [TestMethod]
//    public void SetOxygen()
//    {
//        // Arrange
//        var oxygenMonitor = new OxygenMonitor();
//        oxygenMonitor.PpmPercentage = 21.0;

//        // Act
//        var result = oxygenMonitor.PpmPercentage;

//        // Assert
//        Assert.AreEqual(21.0, result);
//    }

//    [TestMethod]
//    public void SetNegativeOxygen_ReturnZero()  // oxygen levels should have a floor of zero
//    {
//        // Arrange
//        var oxygenMonitor = new OxygenMonitor();
//        oxygenMonitor.PpmPercentage = -5.0;

//        // Act
//        var result = oxygenMonitor.PpmPercentage;

//        // Assert
//        Assert.AreEqual(0.0, result);
//    }

//    [TestMethod]
//    public void SetOxygenAbove100_Return100()
//    {
//        // Arrange
//        var oxygenMonitor = new OxygenMonitor();
//        oxygenMonitor.PpmPercentage = 150.0;    // composition above 100% is not possible

//        // Act
//        var result = oxygenMonitor.PpmPercentage;

//        // Assert
//        Assert.AreEqual(100.0, result); 
//    }

//    [TestMethod]
//    public void TakeOxygenReadingFromFile()
//    {
//        // Arrange
//        var oxygenMonitor = new OxygenMonitor();
//        oxygenMonitor.TakeOxygenReading("TestData/TestOxygenData.txt");

//        // Act
//        var result = oxygenMonitor.PpmPercentage;

//        // Assert
//        Assert.AreEqual(20.9, result, 1e-9);
//    }

//    // Integration testing with FileReader
//    [TestMethod]
//    public void TakeOxygenReadingOver100FromFile_Return100()
//    {
//        // Arrange
//        var oxygenMonitor = new OxygenMonitor();
//        oxygenMonitor.TakeOxygenReading("TestData/TestOxygenData.txt"); // reads 20.9
//        oxygenMonitor.TakeOxygenReading("TestData/TestOxygenData.txt"); // reads 120.0

//        // Act
//        var result = oxygenMonitor.PpmPercentage;   // should be capped at 100.0 

//        // Assert
//        Assert.AreEqual(100.0, result, 1e-9);
//    }

//    [TestMethod]
//    public void TakeOxygenReadingNegativeDoubleFromFile_ReturnZero()
//    {
//        // Arrange
//        // Multiple Readings is more accurate to how the system will be used, rather than specific line reading
//        var oxygenMonitor = new OxygenMonitor();
//        oxygenMonitor.TakeOxygenReading("TestData/TestOxygenData.txt"); // reads 20.9
//        oxygenMonitor.TakeOxygenReading("TestData/TestOxygenData.txt"); // reads 120.0
//        oxygenMonitor.TakeOxygenReading("TestData/TestOxygenData.txt"); // reads -21

//        // Act
//        var result = oxygenMonitor.PpmPercentage;   // should be floored at 0.0 

//        // Assert
//        Assert.AreEqual(0.0, result, 1e-9);
//    }

//    [TestMethod]
//    public void LogOxygenReading_AppendsToFile()
//    {
//        // Arrange
//        var oxygenMonitor = new OxygenMonitor();
//        string fp = "TestData/TestLogOxygenData.txt";
//        double reading = 21.0;
//        oxygenMonitor.PpmPercentage = reading;

//        // Act
//        oxygenMonitor.LogOxygenReading(fp);
//        var lastLine = File.ReadLines(fp).Last();

//        //Assert
//        Assert.AreEqual(reading.ToString(), lastLine);

//    }
//}
