using System;
using System.Collections.Generic;

namespace MyProjectTemplate.API.Models;

public partial class SubDatum
{
    public Guid SubId { get; set; }

    public string? SubName { get; set; }

    public virtual ICollection<SubAlarmsDatum> SubAlarmsData { get; set; } = new List<SubAlarmsDatum>();

    public virtual ICollection<SubControlDatum> SubControlData { get; set; } = new List<SubControlDatum>();

    public virtual ICollection<SubLifeSupportDatum> SubLifeSupportData { get; set; } = new List<SubLifeSupportDatum>();

    public virtual ICollection<SubLog> SubLogs { get; set; } = new List<SubLog>();

    public virtual ICollection<SubPositionDatum> SubPositionData { get; set; } = new List<SubPositionDatum>();

    public virtual ICollection<SubReactorDatum> SubReactorData { get; set; } = new List<SubReactorDatum>();
}
