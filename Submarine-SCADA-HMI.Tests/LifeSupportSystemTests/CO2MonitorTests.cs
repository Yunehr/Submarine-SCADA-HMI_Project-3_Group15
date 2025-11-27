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