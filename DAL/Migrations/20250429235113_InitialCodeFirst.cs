using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCodeFirst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CargoTypes",
                columns: table => new
                {
                    CargoTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CargoTyp__87BCCBF95012A948", x => x.CargoTypeID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentMethodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentM__DC31C1F3A3248BC9", x => x.PaymentMethodID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__8AFACE3AC7A84CCE", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Stations__E0D8A6DD383E09B1", x => x.StationID);
                });

            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    CargoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CargoTypeID = table.Column<int>(type: "int", nullable: false),
                    CargoName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WeightKg = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cargo__B4E665EDD49C63BA", x => x.CargoID);
                    table.ForeignKey(
                        name: "FK__Cargo__CargoType__4BAC3F29",
                        column: x => x.CargoTypeID,
                        principalTable: "CargoTypes",
                        principalColumn: "CargoTypeID");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCAC66BA3F0B", x => x.UserID);
                    table.ForeignKey(
                        name: "FK__Users__RoleID__398D8EEE",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "DowntimeCosts",
                columns: table => new
                {
                    DowntimeCostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationID = table.Column<int>(type: "int", nullable: false),
                    CargoTypeID = table.Column<int>(type: "int", nullable: false),
                    CostPerHour = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    EffectiveDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Downtime__38336108767D0851", x => x.DowntimeCostID);
                    table.ForeignKey(
                        name: "FK__DowntimeC__Cargo__60A75C0F",
                        column: x => x.CargoTypeID,
                        principalTable: "CargoTypes",
                        principalColumn: "CargoTypeID");
                    table.ForeignKey(
                        name: "FK__DowntimeC__Stati__5FB337D6",
                        column: x => x.StationID,
                        principalTable: "Stations",
                        principalColumn: "StationID");
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    RouteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartStationID = table.Column<int>(type: "int", nullable: false),
                    EndStationID = table.Column<int>(type: "int", nullable: false),
                    TotalDistance = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    EstimatedTime = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Routes__80979AADAF8F2EDB", x => x.RouteID);
                    table.ForeignKey(
                        name: "FK__Routes__EndStati__4316F928",
                        column: x => x.EndStationID,
                        principalTable: "Stations",
                        principalColumn: "StationID");
                    table.ForeignKey(
                        name: "FK__Routes__StartSta__4222D4EF",
                        column: x => x.StartStationID,
                        principalTable: "Stations",
                        principalColumn: "StationID");
                });

            migrationBuilder.CreateTable(
                name: "Segments",
                columns: table => new
                {
                    SegmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationFromID = table.Column<int>(type: "int", nullable: false),
                    StationToID = table.Column<int>(type: "int", nullable: false),
                    DistanceKm = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Segments__C680609B342A0A11", x => x.SegmentID);
                    table.ForeignKey(
                        name: "FK__Segments__Statio__3E52440B",
                        column: x => x.StationFromID,
                        principalTable: "Stations",
                        principalColumn: "StationID");
                    table.ForeignKey(
                        name: "FK__Segments__Statio__3F466844",
                        column: x => x.StationToID,
                        principalTable: "Stations",
                        principalColumn: "StationID");
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CargoID = table.Column<int>(type: "int", nullable: false),
                    RouteID = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Requests__33A8519AC0D23075", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK__Requests__CargoI__5070F446",
                        column: x => x.CargoID,
                        principalTable: "Cargo",
                        principalColumn: "CargoID");
                    table.ForeignKey(
                        name: "FK__Requests__RouteI__5165187F",
                        column: x => x.RouteID,
                        principalTable: "Routes",
                        principalColumn: "RouteID");
                    table.ForeignKey(
                        name: "FK__Requests__UserID__4F7CD00D",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "RouteSegments",
                columns: table => new
                {
                    RouteSegmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteID = table.Column<int>(type: "int", nullable: false),
                    SegmentID = table.Column<int>(type: "int", nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RouteSeg__24697603FE73E308", x => x.RouteSegmentID);
                    table.ForeignKey(
                        name: "FK__RouteSegm__Route__45F365D3",
                        column: x => x.RouteID,
                        principalTable: "Routes",
                        principalColumn: "RouteID");
                    table.ForeignKey(
                        name: "FK__RouteSegm__Segme__46E78A0C",
                        column: x => x.SegmentID,
                        principalTable: "Segments",
                        principalColumn: "SegmentID");
                });

            migrationBuilder.CreateTable(
                name: "MovementStatus",
                columns: table => new
                {
                    MovementStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CurrentStationID = table.Column<int>(type: "int", nullable: true),
                    SattusDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Movement__88DF1D1FAAEEB3BE", x => x.MovementStatusID);
                    table.ForeignKey(
                        name: "FK__MovementS__Curre__5CD6CB2B",
                        column: x => x.CurrentStationID,
                        principalTable: "Stations",
                        principalColumn: "StationID");
                    table.ForeignKey(
                        name: "FK__MovementS__Reque__5BE2A6F2",
                        column: x => x.RequestID,
                        principalTable: "Requests",
                        principalColumn: "RequestID");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    PaymentMethodID = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payments__9B556A587D4CBE52", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK__Payments__Paymen__5812160E",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethodID");
                    table.ForeignKey(
                        name: "FK__Payments__Reques__571DF1D5",
                        column: x => x.RequestID,
                        principalTable: "Requests",
                        principalColumn: "RequestID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_CargoTypeID",
                table: "Cargo",
                column: "CargoTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_DowntimeCosts_CargoTypeID",
                table: "DowntimeCosts",
                column: "CargoTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_DowntimeCosts_StationID",
                table: "DowntimeCosts",
                column: "StationID");

            migrationBuilder.CreateIndex(
                name: "IX_MovementStatus_CurrentStationID",
                table: "MovementStatus",
                column: "CurrentStationID");

            migrationBuilder.CreateIndex(
                name: "IX_MovementStatus_RequestID",
                table: "MovementStatus",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentMethodID",
                table: "Payments",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_RequestID",
                table: "Payments",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CargoID",
                table: "Requests",
                column: "CargoID");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RouteID",
                table: "Requests",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UserID",
                table: "Requests",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_EndStationID",
                table: "Routes",
                column: "EndStationID");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_StartStationID",
                table: "Routes",
                column: "StartStationID");

            migrationBuilder.CreateIndex(
                name: "IX_RouteSegments_RouteID",
                table: "RouteSegments",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_RouteSegments_SegmentID",
                table: "RouteSegments",
                column: "SegmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_StationFromID",
                table: "Segments",
                column: "StationFromID");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_StationToID",
                table: "Segments",
                column: "StationToID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DowntimeCosts");

            migrationBuilder.DropTable(
                name: "MovementStatus");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "RouteSegments");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Segments");

            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CargoTypes");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
