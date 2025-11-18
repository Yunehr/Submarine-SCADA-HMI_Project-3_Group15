using System;
using System.Collections.Generic;

namespace MyProjectTemplate.API.Models;

public partial class SubReactorDatum
{
    public Guid SubId { get; set; }

    public int ReactorId { get; set; }

    public double? CoolantLevel { get; set; }

    public double? Temperature { get; set; }

    public double? Radiation { get; set; }

    public double? FuelRodStatus { get; set; }

    public virtual SubDatum Sub { get; set; } = null!;
}
