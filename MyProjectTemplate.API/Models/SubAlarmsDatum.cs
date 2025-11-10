using System;
using System.Collections.Generic;

namespace MyProjectTemplate.API.Models;

public partial class SubAlarmsDatum
{
    public int AlarmId { get; set; }

    public Guid SubId { get; set; }

    public string? AlarmName { get; set; }

    public int? SeverityLevel { get; set; }

    public string? RaisedAt { get; set; }

    public string? ClearedAt { get; set; }

    public virtual SubDatum Sub { get; set; } = null!;
}
