//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.IO;
//using DeviceMonitor;

//namespace Submarine_SCADA_HMI.Tests;

//[TestClass]
//public class AirReserveMonitorTests
//{
//    [TestMethod]
//    public void SetAirReserve()
//    {
//        // Arrange
//        var airReserve = new AirReserveMonitor();
//        airReserve.ReservePercentage = 75.0;

//        // Act
//        var result = airReserve.ReservePercentage;

//        // Assert
//        Assert.AreEqual(75.0, result);
//    }

//    [TestMethod]
//    public void SetNegativeAirReserve_ReturnZero() // Air reserve should have a floor of zero
//    {
//        // Arrange
//        var airReserve = new AirReserveMonitor();
//        airReserve.ReservePercentage = -20.0;

//        // Act
//        var result = airReserve.ReservePercentage;

//        // Assert
//        Assert.AreEqual(0.0, result);
//    }

//    [TestMethod]
//    public void SetAirReserveAbove100_Return100()
//    {
//        // Arrange
//        var airReserve = new AirReserveMonitor();
//        airReserve.ReservePercentage = 150.0;    // reserve above 100% is not possible

//        // Act
//        var result = airReserve.ReservePercentage;

//        // Assert
//        Assert.AreEqual(100.0, result);
//    }

//    // File reading/writing tests

//    [TestMethod]
//    public void TakeAirReserveReading_FromFile()
//    {
//        // Arrange
//        var airReserve = new AirReserveMonitor();
//        string fp = "TestData/TestAirReserveData.txt";

//        // Act
//        airReserve.TakeAirReserveReading(fp); // First line in test file is 75
//        var result1 = airReserve.ReservePercentage;

//        // Assert
//        Assert.AreEqual(75.0, result1);
//    }
//    [TestMethod]
//    public void TakeAirReserveReading_ZeroReading()
//    {
//        // Arrange
//        var airResereve = new AirReserveMonitor();
//        string fp = "TestData/TestAirReserveData.txt";

//        // Act
//        airResereve.TakeAirReserveReading(fp); // First line in test file is 75
//        airResereve.TakeAirReserveReading(fp); // Second line in test file is 0
//        var result = airResereve.ReservePercentage;

//        // Act
//        Assert.AreEqual(0.0, result);
//    }

//    [TestMethod]
//    public void TakeNegativeAirReserveReading_ReturnsZero()
//    {
//        // Arrange
//        var airResereve = new AirReserveMonitor();
//        string fp = "TestData/TestAirReserveData.txt";

//        // Act
//        airResereve.TakeAirReserveReading(fp); // First line in test file is 75
//        airResereve.TakeAirReserveReading(fp); // Second line in test file is 0
//        airResereve.TakeAirReserveReading(fp); // Third line in test file is -20
//        var result = airResereve.ReservePercentage;

//        // Act
//        Assert.AreEqual(0.0, result);
//    }

//    [TestMethod]
//    public void TakeAbove100AirReserveReading_Returns100()
//    {
//        // Arrange
//        var airResereve = new AirReserveMonitor();
//        string fp = "TestData/TestAirReserveData.txt";

//        // Act
//        airResereve.TakeAirReserveReading(fp); // First line in test file is 75
//        airResereve.TakeAirReserveReading(fp); // Second line in test file is 0
//        airResereve.TakeAirReserveReading(fp); // Third line in test file is -20
//        airResereve.TakeAirReserveReading(fp); // Fourth line in test file is 120
//        var result = airResereve.ReservePercentage;

//        // Act
//        Assert.AreEqual(100.0, result);
//    }

//    [TestMethod]
//    public void LogAirReserveReading_AppendToFile()
//    {
//        // Arrange
//        var airReserve = new AirReserveMonitor();
//        string fp = "TestData/TestLogAirReserveData.txt";
//        airReserve.ReservePercentage = 85.0;
//        string reading = airReserve.ReservePercentage.ToString();

//        // Act
//        airReserve.LogAirReserveReading(fp);
//        string lastLine = File.ReadLines(fp).Last();
//        // double loggedValue = double.Parse(lastLine);

//        // Assert
//        Assert.AreEqual(reading, lastLine);
//    }
//}