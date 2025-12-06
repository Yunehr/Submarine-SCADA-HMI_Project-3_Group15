using MyProjectTemplate.API.Data;
using MyProjectTemplate.API.Models;
using MyProjectTemplate.API.LifeSupportSystems;
using MyProjectTemplate.API;
using System.Collections.Generic;

namespace MyProjectTemplate.API.Services
{
    public class LifeSupportDatabaseService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventBus _eventBus;
        private readonly Dictionary<string, Guid> _deviceIdMap;

        // Got this timer method from online
        private System.Timers.Timer? _saveTimer;
        private readonly object _lockObj = new object();

        public LifeSupportDatabaseService(
            IServiceProvider serviceProvider, 
            IEventBus eventBus,
            Dictionary<string, IDevice> devices)
        {
            _serviceProvider = serviceProvider;
            _eventBus = eventBus;

            // Map device keys to their IDs for the later lookup in "GetDeviceValue"
            _deviceIdMap = new Dictionary<string, Guid>
            {
                ["O2"] = devices["O2"].Id,
                ["CO2"] = devices["CO2"].Id,
                ["Air"] = devices["Air"].Id,
                ["IntPressure"] = devices["IntPressure"].Id,
                ["ExPressure"] = devices["ExPressure"].Id,
                ["Temperature"] = devices["Temperature"].Id,
                ["Humidity"] = devices["Humidity"].Id
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
            lock (_lockObj) // Using the timer
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var lifeSupport = new SubLifeSupportDatum
                    {
                        SubId = subId,
                        O2level = GetDeviceValue("O2"),
                        Co2level = GetDeviceValue("CO2"),
                        AirTanklevel = GetDeviceValue("Air"),
                        InternalPressure = GetDeviceValue("IntPressure"),
                        ExternalPressure = GetDeviceValue("ExPressure"),
                        Temperature = GetDeviceValue("Temperature"),
                        Humidity = GetDeviceValue("Humidity"),
                        TimeData = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };

                    db.SubLifeSupportData.Add(lifeSupport);
                    //db.SaveChanges();
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