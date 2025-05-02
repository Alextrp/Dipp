using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class Route
{
    [Key]
    [Column("RouteID")]
    public int RouteId { get; set; }

    [StringLength(100)]
    public string RouteName { get; set; } = null!;

    [Column("StartStationID")]
    public int StartStationId { get; set; }

    [Column("EndStationID")]
    public int EndStationId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? TotalDistance { get; set; }

    public int? EstimatedTime { get; set; }

    [ForeignKey("EndStationId")]
    [InverseProperty("RouteEndStations")]
    public virtual Station EndStation { get; set; } = null!;

    [InverseProperty("Route")]
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    [InverseProperty("Route")]
    public virtual ICollection<RouteSegment> RouteSegments { get; set; } = new List<RouteSegment>();

    [ForeignKey("StartStationId")]
    [InverseProperty("RouteStartStations")]
    public virtual Station StartStation { get; set; } = null!;
}
