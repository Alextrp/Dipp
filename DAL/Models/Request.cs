using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class Request
{
    [Key]
    [Column("RequestID")]
    public int RequestId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [Column("CargoID")]
    public int CargoId { get; set; }

    [Column("RouteID")]
    public int RouteId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RequestDate { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    [Column(TypeName = "decimal(12, 2)")]
    public decimal? Cost { get; set; }

    [StringLength(50)]
    public string? PaymentStatus { get; set; }

    [ForeignKey("CargoId")]
    [InverseProperty("Requests")]
    public virtual Cargo Cargo { get; set; } = null!;

    [InverseProperty("Request")]
    public virtual ICollection<MovementStatus> MovementStatuses { get; set; } = new List<MovementStatus>();

    [InverseProperty("Request")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [ForeignKey("RouteId")]
    [InverseProperty("Requests")]
    public virtual Route Route { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Requests")]
    public virtual User User { get; set; } = null!;
}
