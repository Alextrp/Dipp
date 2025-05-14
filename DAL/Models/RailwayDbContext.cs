using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class RailwayDbContext : DbContext
{
    public RailwayDbContext(DbContextOptions<RailwayDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<ScheduleEntry> ScheduleEntries { get; set; }

    public virtual DbSet<CargoType> CargoTypes { get; set; }

    public virtual DbSet<DowntimeCost> DowntimeCosts { get; set; }

    public virtual DbSet<MovementStatus> MovementStatuses { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<RouteSegment> RouteSegments { get; set; }

    public virtual DbSet<Segment> Segments { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Railway;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.CargoId).HasName("PK__Cargo__B4E665EDD49C63BA");

            entity.HasOne(d => d.CargoType).WithMany(p => p.Cargos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cargo__CargoType__4BAC3F29");
        });

        modelBuilder.Entity<CargoType>(entity =>
        {
            entity.HasKey(e => e.CargoTypeId).HasName("PK__CargoTyp__87BCCBF95012A948");
        });

        modelBuilder.Entity<DowntimeCost>(entity =>
        {
            entity.HasKey(e => e.DowntimeCostId).HasName("PK__Downtime__38336108767D0851");

            entity.HasOne(d => d.CargoType).WithMany(p => p.DowntimeCosts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DowntimeC__Cargo__60A75C0F");

            entity.HasOne(d => d.Station).WithMany(p => p.DowntimeCosts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DowntimeC__Stati__5FB337D6");
        });

        modelBuilder.Entity<MovementStatus>(entity =>
        {
            entity.HasKey(e => e.MovementStatusId).HasName("PK__Movement__88DF1D1FAAEEB3BE");

            entity.Property(e => e.StatusDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CurrentStation).WithMany(p => p.MovementStatuses).HasConstraintName("FK__MovementS__Curre__5CD6CB2B");

            entity.HasOne(d => d.Request).WithMany(p => p.MovementStatuses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MovementS__Reque__5BE2A6F2");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A587D4CBE52");

            entity.Property(e => e.PaymentDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Payments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Paymen__5812160E");

            entity.HasOne(d => d.Request).WithMany(p => p.Payments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Reques__571DF1D5");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__PaymentM__DC31C1F3A3248BC9");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Requests__33A8519AC0D23075");

            entity.Property(e => e.RequestDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Cargo).WithMany(p => p.Requests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__CargoI__5070F446");

            entity.HasOne(d => d.Route).WithMany(p => p.Requests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__RouteI__5165187F");

            entity.HasOne(d => d.User).WithMany(p => p.Requests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__UserID__4F7CD00D");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3AC7A84CCE");
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.RouteId).HasName("PK__Routes__80979AADAF8F2EDB");

            entity.HasOne(d => d.EndStation).WithMany(p => p.RouteEndStations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Routes__EndStati__4316F928");

            entity.HasOne(d => d.StartStation).WithMany(p => p.RouteStartStations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Routes__StartSta__4222D4EF");
        });

        modelBuilder.Entity<RouteSegment>(entity =>
        {
            entity.HasKey(e => e.RouteSegmentId).HasName("PK__RouteSeg__24697603FE73E308");

            entity.HasOne(d => d.Route).WithMany(p => p.RouteSegments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RouteSegm__Route__45F365D3");

            entity.HasOne(d => d.Segment).WithMany(p => p.RouteSegments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RouteSegm__Segme__46E78A0C");
        });

        modelBuilder.Entity<Segment>(entity =>
        {
            entity.HasKey(e => e.SegmentId).HasName("PK__Segments__C680609B342A0A11");

            entity.HasOne(d => d.StationFrom).WithMany(p => p.SegmentStationFroms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Segments__Statio__3E52440B");

            entity.HasOne(d => d.StationTo).WithMany(p => p.SegmentStationTos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Segments__Statio__3F466844");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.StationId).HasName("PK__Stations__E0D8A6DD383E09B1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC66BA3F0B");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleID__398D8EEE");
        });

        modelBuilder.Entity<ScheduleEntry>(entity =>
        {
            entity.HasKey(e => e.ScheduleEntryId);

            entity.HasOne(e => e.Request)
                .WithMany()
                .HasForeignKey(e => e.RequestId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Station)
                .WithMany()
                .HasForeignKey(e => e.StationId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
