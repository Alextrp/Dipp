using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class DowntimeCost
{
    [Key]
    [Column("DowntimeCostID")]
    public int DowntimeCostId { get; set; }

    [Column("StationID")]
    public int StationId { get; set; }

    [Column("CargoTypeID")]
    public int CargoTypeId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal CostPerHour { get; set; }

    public DateOnly EffectiveDate { get; set; }

    [ForeignKey("CargoTypeId")]
    [InverseProperty("DowntimeCosts")]
    public virtual CargoType CargoType { get; set; } = null!;

    [ForeignKey("StationId")]
    [InverseProperty("DowntimeCosts")]
    public virtual Station Station { get; set; } = null!;
}
