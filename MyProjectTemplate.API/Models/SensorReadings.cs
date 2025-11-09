using System;

namespace MyProjectTemplate.API.Models {
    public class SensorReading {
        public Guid ReadingId { get; set; } = Guid.NewGuid();  // Primary key
        public string SensorName { get; set; }
        public double Value { get; set; }
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;
    }
}