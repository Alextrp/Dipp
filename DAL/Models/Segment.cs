using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class Segment
{
    [Key]
    [Column("SegmentID")]
    public int SegmentId { get; set; }

    [Column("StationFromID")]
    public int StationFromId { get; set; }

    [Column("StationToID")]
    public int StationToId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal DistanceKm { get; set; }

    [InverseProperty("Segment")]
    public virtual ICollection<RouteSegment> RouteSegments { get; set; } = new List<RouteSegment>();

    [ForeignKey("StationFromId")]
    [InverseProperty("SegmentStationFroms")]
    public virtual Station StationFrom { get; set; } = null!;

    [ForeignKey("StationToId")]
    [InverseProperty("SegmentStationTos")]
    public virtual Station StationTo { get; set; } = null!;
}
