using System;
using System.Collections.Generic;

namespace MyProjectTemplate.API.Models;

public partial class SubLog
{
    public int LogId { get; set; }

    public Guid SubId { get; set; }

    public string? Level { get; set; }     // INFO, DANGER, ERROR, etc...

    public string? Message { get; set; }   // “Oxygen dropped below safe levels”

    public string? PerformedBy { get; set; }

    public string? TimeData { get; set; }

    public virtual SubDatum Sub { get; set; } = null!;
}
