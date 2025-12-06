using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProjectTemplate.API;
using MyProjectTemplate.API.LifeSupportSystems;

namespace Submarine_SCADA_HMI.Tests.LifeSupportSystemTests
{
    [TestClass]
    [TestCategory("EventBus")]
    public class EventBusTests
    {
        // Simple fake device that lets us manually push readings
        private sealed class FakeDevice : IDevice
        {
            public Guid Id { get; } = Guid.NewGuid();
            public DeviceType DeviceType { get; }
            public Unit Unit { get; }
            public double CurrentValue { get; private set; }

            public event EventHandler<DeviceReading>? ReadingAvailable;

            public FakeDevice(DeviceType deviceType, Unit unit)
            {
                DeviceType = deviceType;
                Unit = unit;
            }

            // EventBus will call these, but for tests they don't need to do anything
            public void Start() { }
            public void Stop() { }

            // Test helper: manually emit a reading into the bus
            public void Publish(double value)
            {
                CurrentValue = value;
                var reading = new DeviceReading(
                    DeviceId: Id,
                    DeviceType: DeviceType,
                    Value: value,
                    Unit: Unit,
                    TimestampUtc: DateTime.UtcNow);

                ReadingAvailable?.Invoke(this, reading);
            }
        }

        [TestMethod]
        public void Register_Subscribe_And_Publish_Should_Invoke_Handler_And_Cache_Latest()
        {
            // Arrange
            var bus = new EventBus();
            var device = new FakeDevice(DeviceType.Oxygen, Unit.Percent);

            // This variable will be set when the handler is called
            DeviceReading? received = null;

            // Subscribe to Oxygen readings
            bus.Subscribe(DeviceType.Oxygen, r =>
            {
                received = r;
            });

            // Register the device with the bus
            bus.Register(device);

            // Act
            device.Publish(21.0);

            // Assert - handler was called
            Assert.IsNotNull(received, "Subscriber should have received a reading.");
            Assert.AreEqual(device.Id, received.Value.DeviceId, "Reading should come from the registered device.");
            Assert.AreEqual(21.0, received.Value.Value, 0.0001, "Reading value should match what was published.");
            Assert.AreEqual(DeviceType.Oxygen, received.Value.DeviceType, "DeviceType should be Oxygen.");
            Assert.AreEqual(Unit.Percent, received.Value.Unit, "Unit should be Percent.");

            // Assert - latest reading was cached per device
            var success = bus.TryGetLatest(device.Id, out var latest);
            Assert.IsTrue(success, "EventBus should have a latest reading for this device.");
            Assert.AreEqual(21.0, latest.Value, 0.0001, "Cached latest value should match last published value.");
        }

        [TestMethod]
        public void Unregister_Should_Stop_Calling_Subscribers()
        {
            // Arrange
            var bus = new EventBus();
            var device = new FakeDevice(DeviceType.CO2, Unit.Ppm);

            int callCount = 0;

            bus.Subscribe(DeviceType.CO2, _ =>
            {
                callCount++;
            });

            bus.Register(device);

            // Act 1: publish once while registered
            device.Publish(100);
            Assert.AreEqual(1, callCount, "Handler should be called once while device is registered.");

            // Unregister the device
            bus.Unregister(device.Id);

            // Act 2: publish again after unregistering
            device.Publish(200);

            // Assert: handler shouldn't be called again
            Assert.AreEqual(1, callCount, "Handler should not be called after device is unregistered.");
        }
    }
}