using MyProjectTemplate.API.LifeSupportSystems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyProjectTemplate.API
{
    public interface IEventBus
    {
        void Register(IDevice device);
        void Unregister(Guid deviceId);
        IDisposable Subscribe(DeviceType topic, Action<DeviceReading> handler);
        bool TryGetLatest(DeviceType topic, out DeviceReading reading);
    }

    public sealed class EventBus : IEventBus
    {
        private readonly object _lock = new();
        private readonly Dictionary<DeviceType, List<Action<DeviceReading>>> _subs = new();
        private readonly Dictionary<DeviceType, DeviceReading> _latest = new();
        private readonly Dictionary<Guid, IDevice> _devices = new();

        public void Register(IDevice device)
        {
            lock (_lock)
            {
                if (_devices.ContainsKey(device.Id)) return;
                _devices.Add(device.Id, device);
                device.ReadingAvailable += OnReading;
                device.Start();
            }
        }

        public void Unregister(Guid deviceId)
        {
            lock (_lock)
            {
                if (!_devices.Remove(deviceId, out var device)) return;
                device.ReadingAvailable -= OnReading;
                device.Stop();
            }
        }

        public IDisposable Subscribe(DeviceType topic, Action<DeviceReading> handler)
        {
            lock (_lock)
            {
                if (!_subs.TryGetValue(topic, out var list))
                {
                    list = new List<Action<DeviceReading>>();
                    _subs[topic] = list;
                }
                list.Add(handler);
            }
            return new Unsubscriber(() =>
            {
                lock (_lock)
                {
                    if (_subs.TryGetValue(topic, out var list))
                        list.Remove(handler);
                }
            });
        }

        public bool TryGetLatest(DeviceType topic, out DeviceReading reading)
            => _latest.TryGetValue(topic, out reading);

        private void OnReading(object? sender, DeviceReading reading)
        {
            List<Action<DeviceReading>> targets;
            lock (_lock)
            {
                _latest[reading.DeviceType] = reading;
                if (!_subs.TryGetValue(reading.DeviceType, out targets!))
                    return;
                targets = targets.ToList();
            }

            foreach (var h in targets)
                h(reading);
        }

        private sealed class Unsubscriber : IDisposable
        {
            private readonly Action _onDispose;
            private bool _disposed;
            public Unsubscriber(Action onDispose) => _onDispose = onDispose;
            public void Dispose()
            {
                if (_disposed) return;
                _onDispose();
                _disposed = true;
            }
        }
    }
}
