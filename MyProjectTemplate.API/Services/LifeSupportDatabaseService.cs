using MyProjectTemplate.API.Data;
using MyProjectTemplate.API.Models;
using MyProjectTemplate.API.LifeSupportSystems;
using MyProjectTemplate.API;

namespace MyProjectTemplate.API.Services
{
    public class LifeSupportDatabaseService
    {
        private readonly IServiceProvider _serviceProvider;
        private Dictionary<string, IDevice> _devices;
        private System.Diagnostics.Stopwatch _lastSaveTime;
        private readonly object _lockObj = new object();
        private const int SaveIntervalMs = 500;

        public LifeSupportDatabaseService(IServiceProvider serviceProvider, Dictionary<string, IDevice> devices)
        {
            _serviceProvider = serviceProvider;
            _devices = devices;
            _lastSaveTime = System.Diagnostics.Stopwatch.StartNew();
        }

        // Called from event bus subscription - saves only if 500ms has passed since last save
        public void OnDeviceReadingReceived(DeviceReading reading)
        {
            lock (_lockObj)
            {
                // Only save if enough time has passed (debounce multiple rapid events)
                if (_lastSaveTime.ElapsedMilliseconds >= SaveIntervalMs)
                {
                    SaveConsolidatedReading(Guid.Parse("11111111-1111-1111-1111-111111111111"));
                    _lastSaveTime.Restart();
                }
            }
        }

        public void SaveConsolidatedReading(Guid subId)
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
                db.SaveChanges();
            }
        }

        private double GetDeviceValue(string deviceKey)
        {
            if (_devices.TryGetValue(deviceKey, out var device))
            {
                return device.CurrentValue;
            }
            return 0.0;
        }
    }
}