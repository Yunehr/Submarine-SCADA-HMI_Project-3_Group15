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
    public void GetOxygenFromFile()
    {
        // Arrange
        var oxygenMonitor = new OxygenMonitor();
        oxygenMonitor.UpdateOxygenFromFile("TestData/OxygenData.txt");

        // Act
        var result = oxygenMonitor.PpmPercentage;

        // Assert
        Assert.AreEqual(20.9, result, 1e-9);
    }
}