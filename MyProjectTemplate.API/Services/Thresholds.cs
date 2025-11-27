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
        public ThresholdSet Pressure { get; set; }
        public ThresholdSet Humidity { get; set; }
        public ThresholdSet Temperature { get; set; }
        public ThresholdSet AirReserve { get; set; }
    }
}
