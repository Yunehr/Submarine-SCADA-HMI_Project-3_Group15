using System;
using System.Collections.Generic;

namespace MyProjectTemplate.API.Models;

public partial class SubPositionDatum
{
    public Guid SubId { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string TimeData { get; set; } = null!;

    public virtual SubDatum Sub { get; set; } = null!;
}
