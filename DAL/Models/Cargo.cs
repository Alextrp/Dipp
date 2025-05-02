using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

[Table("Cargo")]
public partial class Cargo
{
    [Key]
    [Column("CargoID")]
    public int CargoId { get; set; }

    [Column("CargoTypeID")]
    public int CargoTypeId { get; set; }

    [StringLength(100)]
    public string CargoName { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? WeightKg { get; set; }

    [StringLength(255)]
    public string? AdditionalInfo { get; set; }

    [ForeignKey("CargoTypeId")]
    [InverseProperty("Cargos")]
    public virtual CargoType CargoType { get; set; } = null!;

    [InverseProperty("Cargo")]
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
