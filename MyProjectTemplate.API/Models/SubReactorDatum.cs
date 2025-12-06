using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyProjectTemplate.API.Models;

public partial class SubReactorDatum
{
    public Guid SubId { get; set; }
    public int ReactorReadingId { get; set; }
    public double? ReactorOutput { get; set; }
    public double? CoolantLevel { get; set; }
    public double? Radiation { get; set; }
    public double? Battery { get; set; }
    public double? Temperature { get; set; }
    public double? FuelRodStatus { get; set; }
    public string TimeData { get; set; }

    public virtual SubDatum Sub { get; set; } = null!;
}