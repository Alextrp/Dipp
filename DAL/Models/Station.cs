using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class Station
{
    [Key]
    [Column("StationID")]
    public int StationId { get; set; }

    [StringLength(100)]
    public string StationName { get; set; } = null!;

    [StringLength(200)]
    public string? Location { get; set; }

    [InverseProperty("Station")]
    public virtual ICollection<DowntimeCost> DowntimeCosts { get; set; } = new List<DowntimeCost>();

    [InverseProperty("CurrentStation")]
    public virtual ICollection<MovementStatus> MovementStatuses { get; set; } = new List<MovementStatus>();

    [InverseProperty("EndStation")]
    public virtual ICollection<Route> RouteEndStations { get; set; } = new List<Route>();

    [InverseProperty("StartStation")]
    public virtual ICollection<Route> RouteStartStations { get; set; } = new List<Route>();

    [InverseProperty("StationFrom")]
    public virtual ICollection<Segment> SegmentStationFroms { get; set; } = new List<Segment>();

    [InverseProperty("StationTo")]
    public virtual ICollection<Segment> SegmentStationTos { get; set; } = new List<Segment>();
}
