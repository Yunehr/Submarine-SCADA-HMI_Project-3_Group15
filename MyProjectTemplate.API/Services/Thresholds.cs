// Alarm thresholds
// const double O2_MIN = 21.0;
// const double CO2_MAX = 390;
// const double AIR_RESERVE_MIN = 40.0;
// const double INTERNAL_PRESSURE_MAX = 1.2;
// const double INTERNAL_PRESSURE_MIN = 0.8;
// const double EXTERNAL_PRESSURE_UPPER_WARNING = 24.0;
// const double EXTERNAL_PRESSURE_MAX = 36.0;
// const double EXTERNAL_PRESSURE_MIN = 0.5;
// const double TEMP_MAX = 27.0;
// const double TEMP_MIN = 15.0;
// const double HUMIDITY_MAX = 60.0;
// const double HUMIDITY_MIN = 20.0;

namespace MyProjectTemplate.API.Services {
    public class ThresholdSet {
        public double? VeryLow { get; set; }
        public double? Low { get; set; }
        public double? High { get; set; }
        public double? VeryHigh { get; set; }
    }

    public class DeviceThresholds {
        public ThresholdSet Oxygen { get; set; }
        public ThresholdSet CO2 { get; set; }
        public ThresholdSet inPressure { get; set; }
        public ThresholdSet exPressure { get; set; }
        public ThresholdSet Humidity { get; set; }
        public ThresholdSet Temperature { get; set; }
        public ThresholdSet AirReserve { get; set; }
    }
}
