using Microsoft.VisualStudio.TestTools.UnitTesting;
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
}