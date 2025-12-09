using System;
using MyProjectTemplate.API.LifeSupportSystems;

namespace Submarine_SCADA_HMI.Tests.LifeSupportSystemTests
{
    internal sealed class TestOxygenMonitor : MonitorBase
    {
        public override DeviceType DeviceType => DeviceType.Oxygen;
        public override Unit Unit => Unit.Percent;

        private double _nextValue;

        public void SetNextValue(double value)
        {
            _nextValue = value;
        }

        protected override double SampleSensor()
        {
            // For this test monitor, each tick just returns whatever we last set.
            return _nextValue;
        }   
    }
}