using System;
using MyProjectTemplate.API.SubMovement;
using MyProjectTemplate.API.LifeSupportSystems;

namespace MyProjectTemplate.API.LifeSupportSystems
{
    public sealed class ExternalPressureMonitor : MonitorBase
    {
        private readonly IMovement _movement;
        public override DeviceType DeviceType => DeviceType.Pressure;
        public override Unit Unit => Unit.Bar;
        private double _currentLevel = 1.0;

        public ExternalPressureMonitor(IMovement movement)
        {
            _movement = movement;
        }

        protected override double SampleSensor()
        {
            var depthMeters = _movement.GetPosZ();
            var externalPressure = Math.Abs(depthMeters) / 10;
            if (externalPressure <= 1)
                _currentLevel = 1;
            else
                _currentLevel = externalPressure;
            return _currentLevel;
        }

        public double SampleSensorForTest()
        {
            return SampleSensor();
        }
    }
}
