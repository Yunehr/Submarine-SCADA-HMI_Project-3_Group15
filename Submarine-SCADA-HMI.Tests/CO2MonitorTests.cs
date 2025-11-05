using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using DeviceMonitor;

namespace Submarine_SCADA_HMI.Tests;

[TestClass]
public class CO2MonitorTests
{
    [TestMethod]
    public void SetCO2()
    {
        // Arrange
        var co2Monitor = new CO2Monitor();
        co2Monitor.PpmPercentage = 0.04;

        // Act
        var result = co2Monitor.PpmPercentage;

        // Assert
        Assert.AreEqual(0.04, result);
    }

    [TestMethod]
    public void SetNegativeCO2_ReturnZero() // CO2 levels should have a floor of zero
    {
        // Arrange
        var co2Monitor = new CO2Monitor();
        co2Monitor.PpmPercentage = -0.07;

        // Act
        var result = co2Monitor.PpmPercentage;

        // Assert
        Assert.AreEqual(0.0, result);
    }

    [TestMethod]
    public void SetCO2PercentageAbove100_Return100()
    {
        // Arrange
        var co2Monitor = new CO2Monitor();
        co2Monitor.PpmPercentage = 150.0;    // composition above 100% is not possible

        // Act
        var result = co2Monitor.PpmPercentage;

        // Assert
        Assert.AreEqual(100.0, result);
    }

    [TestMethod]
    public void GetCO2FromFile()
    {
        // Arrange
        var co2Monitor = new CO2Monitor();
        co2Monitor.UpdateCO2FromFile("TestData/TestCO2Data.txt");

        // Act
        var result = co2Monitor.PpmPercentage;

        // Assert
        Assert.AreEqual(4.0, result, 1e-9);
    }
}