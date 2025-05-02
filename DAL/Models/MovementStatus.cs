using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

[Table("MovementStatus")]
public partial class MovementStatus
{
    [Key]
    [Column("MovementStatusID")]
    public int MovementStatusId { get; set; }

    [Column("RequestID")]
    public int RequestId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime StatusDate { get; set; }

    [Column("CurrentStationID")]
    public int? CurrentStationId { get; set; }

    [StringLength(255)]
    public string? SattusDescription { get; set; }

    [ForeignKey("CurrentStationId")]
    [InverseProperty("MovementStatuses")]
    public virtual Station? CurrentStation { get; set; }

    [ForeignKey("RequestId")]
    [InverseProperty("MovementStatuses")]
    public virtual Request Request { get; set; } = null!;
}
