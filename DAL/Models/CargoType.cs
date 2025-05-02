using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class CargoType
{
    [Key]
    [Column("CargoTypeID")]
    public int CargoTypeId { get; set; }

    [StringLength(100)]
    public string TypeName { get; set; } = null!;

    [StringLength(255)]
    public string? Description { get; set; }

    [InverseProperty("CargoType")]
    public virtual ICollection<Cargo> Cargos { get; set; } = new List<Cargo>();

    [InverseProperty("CargoType")]
    public virtual ICollection<DowntimeCost> DowntimeCosts { get; set; } = new List<DowntimeCost>();
}
