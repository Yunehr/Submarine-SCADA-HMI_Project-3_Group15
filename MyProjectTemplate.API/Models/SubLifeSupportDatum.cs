using System;
using System.Collections.Generic;

namespace MyProjectTemplate.API.Models;

public partial class SubLifeSupportDatum
{
    public Guid SubId { get; set; }

    public double? O2level { get; set; }

    public double? Co2level { get; set; }

    public double? AirTanklevel { get; set; }

    public double? InternalPressure { get; set; }

    public double? ExternalPressure { get; set; }

    public double? Temperature { get; set; }

    public double? Humidity { get; set; }

    public string TimeData { get; set; } = null!;

    public virtual SubDatum Sub { get; set; } = null!;
}
