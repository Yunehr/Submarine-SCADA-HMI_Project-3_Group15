namespace MyProjectTemplate.API.LifeSupportSystems
{
    public abstract class MonitorBase : IDevice
    {
        private System.Timers.Timer? _timer;    // compiler confusion when using Timer? because it doesn't know if it might mean System.Threading.Timer

        public Guid Id { get; } = Guid.NewGuid();
        public abstract DeviceType DeviceType { get; }
        public abstract Unit Unit { get; }

        public double CurrentValue { get; private set; }

        public event EventHandler<DeviceReading>? ReadingAvailable;

        protected abstract double SampleSensor();

        public void Start()
        {
            if (_timer != null) return;
            _timer = new System.Timers.Timer(500);
            _timer.Elapsed += (_, __) =>
            {
                var value = SampleSensor();
                var reading = new DeviceReading(Id, DeviceType, value, Unit, DateTime.UtcNow);
                CurrentValue = value;
                ReadingAvailable?.Invoke(this, reading);
            };
            _timer.AutoReset = true;
            _timer.Start();
        }

        public void Stop()
        {
            _timer?.Stop();
            _timer?.Dispose();
            _timer = null;
        }
    }
}
