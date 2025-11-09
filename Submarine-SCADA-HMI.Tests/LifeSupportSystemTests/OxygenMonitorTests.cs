using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using DeviceMonitor;


namespace Submarine_SCADA_HMI.Tests;

[TestClass]
public class OxygenMonitorTests
{
    [TestMethod]
    public void SetOxygen()
    {
        // Arrange
        var oxygenMonitor = new OxygenMonitor();
        oxygenMonitor.PpmPercentage = 21.0;

        // Act
        var result = oxygenMonitor.PpmPercentage;

        // Assert
        Assert.AreEqual(21.0, result);
    }

    [TestMethod]
    public void SetNegativeOxygen_ReturnZero()  // oxygen levels should have a floor of zero
    {
        // Arrange
        var oxygenMonitor = new OxygenMonitor();
        oxygenMonitor.PpmPercentage = -5.0;

        // Act
        var result = oxygenMonitor.PpmPercentage;

        // Assert
        Assert.AreEqual(0.0, result);
    }

    [TestMethod]
    public void SetOxygenAbove100_Return100()
    {
        // Arrange
        var oxygenMonitor = new OxygenMonitor();
        oxygenMonitor.PpmPercentage = 150.0;    // composition above 100% is not possible

        // Act
        var result = oxygenMonitor.PpmPercentage;

        // Assert
        Assert.AreEqual(100.0, result); 
    }

    [TestMethod]
    public void TakeOxygenReadingFromFile()
    {
        // Arrange
        var oxygenMonitor = new OxygenMonitor();
        oxygenMonitor.TakeOxygenReading("TestData/TestOxygenData.txt");

        // Act
        var result = oxygenMonitor.PpmPercentage;

        // Assert
        Assert.AreEqual(20.9, result, 1e-9);
    }

    // Integration testing with FileReader
    [TestMethod]
    public void TakeOxygenReadingOver100FromFile_Return100()
    {
        // Arrange
        var oxygenMonitor = new OxygenMonitor();
        oxygenMonitor.TakeOxygenReading("TestData/TestOxygenData.txt"); // reads 20.9
        oxygenMonitor.TakeOxygenReading("TestData/TestOxygenData.txt"); // reads 120.0

        // Act
        var result = oxygenMonitor.PpmPercentage;   // should be capped at 100.0 

        // Assert
        Assert.AreEqual(100.0, result, 1e-9);
    }

    [TestMethod]
    public void TakeOxygenReadingNegativeDoubleFromFile_ReturnZero()
    {
        // Arrange
        // Multiple Readings is more accurate to how the system will be used, rather than specific line reading
        var oxygenMonitor = new OxygenMonitor();
        oxygenMonitor.TakeOxygenReading("TestData/TestOxygenData.txt"); // reads 20.9
        oxygenMonitor.TakeOxygenReading("TestData/TestOxygenData.txt"); // reads 120.0
        oxygenMonitor.TakeOxygenReading("TestData/TestOxygenData.txt"); // reads -21

        // Act
        var result = oxygenMonitor.PpmPercentage;   // should be floored at 0.0 

        // Assert
        Assert.AreEqual(0.0, result, 1e-9);
    }

    [TestMethod]
    public void LogOxygenReading_AppendsToFile()
    {
        // Arrange
        var oxygenMonitor = new OxygenMonitor();
        string fp = "TestData/TestLogOxygenData.txt";
        double reading = 21.0;
        oxygenMonitor.PpmPercentage = reading;

        // Act
        oxygenMonitor.LogOxygenReading(fp);
        var lastLine = File.ReadLines(fp).Last();

        //Assert
        Assert.AreEqual(reading.ToString(), lastLine);

    }
}