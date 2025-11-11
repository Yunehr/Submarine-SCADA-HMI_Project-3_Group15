using System;
using System.Collections.Generic;

namespace MyProjectTemplate.API.Models;

public partial class SubLog
{
    public int LogId { get; set; }

    public Guid SubId { get; set; }

    public string? ActionTaken { get; set; }

    public string? Command { get; set; }

    public string? PerformedBy { get; set; }

    public string TimeData { get; set; } = null!;

    public virtual SubDatum Sub { get; set; } = null!;
}
