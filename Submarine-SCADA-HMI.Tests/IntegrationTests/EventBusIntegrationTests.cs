using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProjectTemplate.API;
using Submarine_SCADA_HMI.Tests.LifeSupportSystemTests;

namespace Submarine_SCADA_HMI.Tests.EventBusTests
{
    // integration test between EventBus and a test monitor
    [TestClass]
    [TestCategory("EventBusIntegration")]
    public class EventBusIntegrationTests
    {
        // testing that EventBus correctly stores latest readings from a registered monitor
        [TestMethod]
        public void EventBus_StoresLatestReading_ForRegisteredMonitor()
        {
            // Arrange
            var bus = new EventBus();
            var monitor = new TestOxygenMonitor();
            monitor.SetNextValue(42.0);

            // Act
            bus.Register(monitor);         // this wires ReadingAvailable and starts the timer

            // Wait a bit longer than the 500ms interval in MonitorBase.Start()
            Thread.Sleep(700);

            // Assert
            var ok = bus.TryGetLatest(monitor.Id, out var reading);

            Assert.IsTrue(ok, "EventBus should have a latest reading for the registered monitor.");
            Assert.AreEqual(42.0, reading.Value, 0.001, "Latest reading should match monitor value.");
            Assert.AreEqual(DeviceType.Oxygen, reading.DeviceType);
            Assert.AreEqual(Unit.Percent, reading.Unit);
        }
    }
}