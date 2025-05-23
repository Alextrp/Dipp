﻿// <auto-generated />
using System;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(RailwayDbContext))]
    [Migration("20250513173215_InitFromDatabase")]
    partial class InitFromDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DAL.Models.Cargo", b =>
                {
                    b.Property<int>("CargoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CargoID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CargoId"));

                    b.Property<string>("AdditionalInfo")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CargoName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("CargoTypeId")
                        .HasColumnType("int")
                        .HasColumnName("CargoTypeID");

                    b.Property<decimal?>("WeightKg")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("CargoId")
                        .HasName("PK__Cargo__B4E665EDD49C63BA");

                    b.HasIndex("CargoTypeId");

                    b.ToTable("Cargo");
                });

            modelBuilder.Entity("DAL.Models.CargoType", b =>
                {
                    b.Property<int>("CargoTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CargoTypeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CargoTypeId"));

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CargoTypeId")
                        .HasName("PK__CargoTyp__87BCCBF95012A948");

                    b.ToTable("CargoTypes");
                });

            modelBuilder.Entity("DAL.Models.DowntimeCost", b =>
                {
                    b.Property<int>("DowntimeCostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DowntimeCostID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DowntimeCostId"));

                    b.Property<int>("CargoTypeId")
                        .HasColumnType("int")
                        .HasColumnName("CargoTypeID");

                    b.Property<decimal>("CostPerHour")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<DateOnly>("EffectiveDate")
                        .HasColumnType("date");

                    b.Property<int>("StationId")
                        .HasColumnType("int")
                        .HasColumnName("StationID");

                    b.HasKey("DowntimeCostId")
                        .HasName("PK__Downtime__38336108767D0851");

                    b.HasIndex("CargoTypeId");

                    b.HasIndex("StationId");

                    b.ToTable("DowntimeCosts");
                });

            modelBuilder.Entity("DAL.Models.MovementStatus", b =>
                {
                    b.Property<int>("MovementStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MovementStatusID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MovementStatusId"));

                    b.Property<int?>("CurrentStationId")
                        .HasColumnType("int")
                        .HasColumnName("CurrentStationID");

                    b.Property<int>("RequestId")
                        .HasColumnType("int")
                        .HasColumnName("RequestID");

                    b.Property<string>("SattusDescription")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("StatusDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("MovementStatusId")
                        .HasName("PK__Movement__88DF1D1FAAEEB3BE");

                    b.HasIndex("CurrentStationId");

                    b.HasIndex("RequestId");

                    b.ToTable("MovementStatus");
                });

            modelBuilder.Entity("DAL.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PaymentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<DateTime>("PaymentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("PaymentMethodId")
                        .HasColumnType("int")
                        .HasColumnName("PaymentMethodID");

                    b.Property<string>("PaymentStatus")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RequestId")
                        .HasColumnType("int")
                        .HasColumnName("RequestID");

                    b.HasKey("PaymentId")
                        .HasName("PK__Payments__9B556A587D4CBE52");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("RequestId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("DAL.Models.PaymentMethod", b =>
                {
                    b.Property<int>("PaymentMethodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PaymentMethodID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentMethodId"));

                    b.Property<string>("PaymentMethod1")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("PaymentMethod");

                    b.HasKey("PaymentMethodId")
                        .HasName("PK__PaymentM__DC31C1F3A3248BC9");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("DAL.Models.Request", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RequestID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"));

                    b.Property<int>("CargoId")
                        .HasColumnType("int")
                        .HasColumnName("CargoID");

                    b.Property<decimal?>("Cost")
                        .HasColumnType("decimal(12, 2)");

                    b.Property<string>("PaymentStatus")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("RequestDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("RouteId")
                        .HasColumnType("int")
                        .HasColumnName("RouteID");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("RequestId")
                        .HasName("PK__Requests__33A8519AC0D23075");

                    b.HasIndex("CargoId");

                    b.HasIndex("RouteId");

                    b.HasIndex("UserId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("DAL.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RoleID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RoleId")
                        .HasName("PK__Roles__8AFACE3AC7A84CCE");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("DAL.Models.Route", b =>
                {
                    b.Property<int>("RouteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RouteID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RouteId"));

                    b.Property<int>("EndStationId")
                        .HasColumnType("int")
                        .HasColumnName("EndStationID");

                    b.Property<int?>("EstimatedTime")
                        .HasColumnType("int");

                    b.Property<string>("RouteName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("StartStationId")
                        .HasColumnType("int")
                        .HasColumnName("StartStationID");

                    b.Property<decimal?>("TotalDistance")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("RouteId")
                        .HasName("PK__Routes__80979AADAF8F2EDB");

                    b.HasIndex("EndStationId");

                    b.HasIndex("StartStationId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("DAL.Models.RouteSegment", b =>
                {
                    b.Property<int>("RouteSegmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RouteSegmentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RouteSegmentId"));

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<int>("RouteId")
                        .HasColumnType("int")
                        .HasColumnName("RouteID");

                    b.Property<int>("SegmentId")
                        .HasColumnType("int")
                        .HasColumnName("SegmentID");

                    b.HasKey("RouteSegmentId")
                        .HasName("PK__RouteSeg__24697603FE73E308");

                    b.HasIndex("RouteId");

                    b.HasIndex("SegmentId");

                    b.ToTable("RouteSegments");
                });

            modelBuilder.Entity("DAL.Models.ScheduleEntry", b =>
                {
                    b.Property<int>("ScheduleEntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScheduleEntryId"));

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("Downtime")
                        .HasColumnType("time");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<int>("StationId")
                        .HasColumnType("int");

                    b.HasKey("ScheduleEntryId");

                    b.HasIndex("RequestId");

                    b.HasIndex("StationId");

                    b.ToTable("ScheduleEntries");
                });

            modelBuilder.Entity("DAL.Models.Segment", b =>
                {
                    b.Property<int>("SegmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SegmentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SegmentId"));

                    b.Property<decimal>("DistanceKm")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("StationFromId")
                        .HasColumnType("int")
                        .HasColumnName("StationFromID");

                    b.Property<int>("StationToId")
                        .HasColumnType("int")
                        .HasColumnName("StationToID");

                    b.HasKey("SegmentId")
                        .HasName("PK__Segments__C680609B342A0A11");

                    b.HasIndex("StationFromId");

                    b.HasIndex("StationToId");

                    b.ToTable("Segments");
                });

            modelBuilder.Entity("DAL.Models.Station", b =>
                {
                    b.Property<int>("StationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("StationID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StationId"));

                    b.Property<string>("Location")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("StationName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("StationId")
                        .HasName("PK__Stations__E0D8A6DD383E09B1");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("FullName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("RoleID");

                    b.HasKey("UserId")
                        .HasName("PK__Users__1788CCAC66BA3F0B");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL.Models.Cargo", b =>
                {
                    b.HasOne("DAL.Models.CargoType", "CargoType")
                        .WithMany("Cargos")
                        .HasForeignKey("CargoTypeId")
                        .IsRequired()
                        .HasConstraintName("FK__Cargo__CargoType__4BAC3F29");

                    b.Navigation("CargoType");
                });

            modelBuilder.Entity("DAL.Models.DowntimeCost", b =>
                {
                    b.HasOne("DAL.Models.CargoType", "CargoType")
                        .WithMany("DowntimeCosts")
                        .HasForeignKey("CargoTypeId")
                        .IsRequired()
                        .HasConstraintName("FK__DowntimeC__Cargo__60A75C0F");

                    b.HasOne("DAL.Models.Station", "Station")
                        .WithMany("DowntimeCosts")
                        .HasForeignKey("StationId")
                        .IsRequired()
                        .HasConstraintName("FK__DowntimeC__Stati__5FB337D6");

                    b.Navigation("CargoType");

                    b.Navigation("Station");
                });

            modelBuilder.Entity("DAL.Models.MovementStatus", b =>
                {
                    b.HasOne("DAL.Models.Station", "CurrentStation")
                        .WithMany("MovementStatuses")
                        .HasForeignKey("CurrentStationId")
                        .HasConstraintName("FK__MovementS__Curre__5CD6CB2B");

                    b.HasOne("DAL.Models.Request", "Request")
                        .WithMany("MovementStatuses")
                        .HasForeignKey("RequestId")
                        .IsRequired()
                        .HasConstraintName("FK__MovementS__Reque__5BE2A6F2");

                    b.Navigation("CurrentStation");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("DAL.Models.Payment", b =>
                {
                    b.HasOne("DAL.Models.PaymentMethod", "PaymentMethod")
                        .WithMany("Payments")
                        .HasForeignKey("PaymentMethodId")
                        .IsRequired()
                        .HasConstraintName("FK__Payments__Paymen__5812160E");

                    b.HasOne("DAL.Models.Request", "Request")
                        .WithMany("Payments")
                        .HasForeignKey("RequestId")
                        .IsRequired()
                        .HasConstraintName("FK__Payments__Reques__571DF1D5");

                    b.Navigation("PaymentMethod");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("DAL.Models.Request", b =>
                {
                    b.HasOne("DAL.Models.Cargo", "Cargo")
                        .WithMany("Requests")
                        .HasForeignKey("CargoId")
                        .IsRequired()
                        .HasConstraintName("FK__Requests__CargoI__5070F446");

                    b.HasOne("DAL.Models.Route", "Route")
                        .WithMany("Requests")
                        .HasForeignKey("RouteId")
                        .IsRequired()
                        .HasConstraintName("FK__Requests__RouteI__5165187F");

                    b.HasOne("DAL.Models.User", "User")
                        .WithMany("Requests")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__Requests__UserID__4F7CD00D");

                    b.Navigation("Cargo");

                    b.Navigation("Route");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.Route", b =>
                {
                    b.HasOne("DAL.Models.Station", "EndStation")
                        .WithMany("RouteEndStations")
                        .HasForeignKey("EndStationId")
                        .IsRequired()
                        .HasConstraintName("FK__Routes__EndStati__4316F928");

                    b.HasOne("DAL.Models.Station", "StartStation")
                        .WithMany("RouteStartStations")
                        .HasForeignKey("StartStationId")
                        .IsRequired()
                        .HasConstraintName("FK__Routes__StartSta__4222D4EF");

                    b.Navigation("EndStation");

                    b.Navigation("StartStation");
                });

            modelBuilder.Entity("DAL.Models.RouteSegment", b =>
                {
                    b.HasOne("DAL.Models.Route", "Route")
                        .WithMany("RouteSegments")
                        .HasForeignKey("RouteId")
                        .IsRequired()
                        .HasConstraintName("FK__RouteSegm__Route__45F365D3");

                    b.HasOne("DAL.Models.Segment", "Segment")
                        .WithMany("RouteSegments")
                        .HasForeignKey("SegmentId")
                        .IsRequired()
                        .HasConstraintName("FK__RouteSegm__Segme__46E78A0C");

                    b.Navigation("Route");

                    b.Navigation("Segment");
                });

            modelBuilder.Entity("DAL.Models.ScheduleEntry", b =>
                {
                    b.HasOne("DAL.Models.Request", "Request")
                        .WithMany()
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Station", "Station")
                        .WithMany()
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");

                    b.Navigation("Station");
                });

            modelBuilder.Entity("DAL.Models.Segment", b =>
                {
                    b.HasOne("DAL.Models.Station", "StationFrom")
                        .WithMany("SegmentStationFroms")
                        .HasForeignKey("StationFromId")
                        .IsRequired()
                        .HasConstraintName("FK__Segments__Statio__3E52440B");

                    b.HasOne("DAL.Models.Station", "StationTo")
                        .WithMany("SegmentStationTos")
                        .HasForeignKey("StationToId")
                        .IsRequired()
                        .HasConstraintName("FK__Segments__Statio__3F466844");

                    b.Navigation("StationFrom");

                    b.Navigation("StationTo");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.HasOne("DAL.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK__Users__RoleID__398D8EEE");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DAL.Models.Cargo", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("DAL.Models.CargoType", b =>
                {
                    b.Navigation("Cargos");

                    b.Navigation("DowntimeCosts");
                });

            modelBuilder.Entity("DAL.Models.PaymentMethod", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("DAL.Models.Request", b =>
                {
                    b.Navigation("MovementStatuses");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("DAL.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("DAL.Models.Route", b =>
                {
                    b.Navigation("Requests");

                    b.Navigation("RouteSegments");
                });

            modelBuilder.Entity("DAL.Models.Segment", b =>
                {
                    b.Navigation("RouteSegments");
                });

            modelBuilder.Entity("DAL.Models.Station", b =>
                {
                    b.Navigation("DowntimeCosts");

                    b.Navigation("MovementStatuses");

                    b.Navigation("RouteEndStations");

                    b.Navigation("RouteStartStations");

                    b.Navigation("SegmentStationFroms");

                    b.Navigation("SegmentStationTos");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Navigation("Requests");
                });
#pragma warning restore 612, 618
        }
    }
}
