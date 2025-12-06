using MyProjectTemplate.API.Data;
using MyProjectTemplate.API.Models;
using MyProjectTemplate.API.LifeSupportSystems;
using MyProjectTemplate.API;
using System.Collections.Generic;

namespace MyProjectTemplate.API.Services
{
    public class ReactorDatabaseService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventBus _eventBus;
        private readonly Dictionary<string, Guid> _deviceIdMap;

        // Got this timer method from online
        private System.Timers.Timer? _saveTimer;
        private readonly object _lockObj = new object();

        public ReactorDatabaseService(
            IServiceProvider serviceProvider,
            IEventBus eventBus,
            Dictionary<string, IDevice> devices)
        {
            _serviceProvider = serviceProvider;
            _eventBus = eventBus;

            // Map device keys to their IDs for the later lookup in "GetDeviceValue"
            _deviceIdMap = new Dictionary<string, Guid>
            {
                ["ReactorOutput"] = devices["ReactorOutput"].Id,
                ["Coolant"] = devices["Coolant"].Id,
                ["FuelRod"] = devices["FuelRod"].Id,
                ["Radiation"] = devices["Radiation"].Id,
                ["Battery"] = devices["Battery"].Id,
                ["ReactorTemp"] = devices["ReactorTemp"].Id,
            };
        }


        // The timer method is used to prevent billions of writes to the DB (because of the 5 subscriptions)
        public void StartPeriodicSave(Guid subId)
        {
            _saveTimer = new System.Timers.Timer(500);
            _saveTimer.Elapsed += (sender, e) => SaveConsolidatedReading(subId);
            _saveTimer.AutoReset = true;
            _saveTimer.Start();
        }

        // More of the timer thingy
        public void StopPeriodicSave()
        {
            _saveTimer?.Stop();
            _saveTimer?.Dispose();
            _saveTimer = null;
        }

        private void SaveConsolidatedReading(Guid subId)
        {
            lock (_lockObj)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var reactor = new SubReactorDatum
                    {
                        SubId = subId,
                        ReactorOutput = GetDeviceValue("ReactorOutput"),
                        CoolantLevel = GetDeviceValue("Coolant"),
                        Radiation = GetDeviceValue("Radiation"),
                        Battery = GetDeviceValue("Battery"),
                        Temperature = GetDeviceValue("ReactorTemp"),
                        FuelRodStatus = GetDeviceValue("FuelRod"),
                        TimeData = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };

                    db.SubReactorData.Add(reactor);
                    db.SaveChanges();
                }
            }
        }

        private double GetDeviceValue(string deviceName)
        {
            if (_deviceIdMap.TryGetValue(deviceName, out var deviceId)) // Try to get the device ID (Making sure it actual made it to the map)
            {
                if (_eventBus.TryGetLatest(deviceId, out var reading))
                {
                    return Math.Round(reading.Value, 2); // Round that thang 
                }
            }
            return 0;
        }
    }
}