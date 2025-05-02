using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class RouteSegment
{
    [Key]
    [Column("RouteSegmentID")]
    public int RouteSegmentId { get; set; }

    [Column("RouteID")]
    public int RouteId { get; set; }

    [Column("SegmentID")]
    public int SegmentId { get; set; }

    public int OrderNumber { get; set; }

    [ForeignKey("RouteId")]
    [InverseProperty("RouteSegments")]
    public virtual Route Route { get; set; } = null!;

    [ForeignKey("SegmentId")]
    [InverseProperty("RouteSegments")]
    public virtual Segment Segment { get; set; } = null!;
}
