using System;
using System.Collections.Generic;

namespace MyProjectTemplate.API.Models;

public partial class SubControlDatum
{
    public Guid SubId { get; set; }

    public double PropellerState { get; set; }

    public double RudderState { get; set; }

    public double SternPlateState { get; set; }

    public double BallastState { get; set; }

    public string TimeData { get; set; } = null!;

    public virtual SubDatum Sub { get; set; } = null!;
}
