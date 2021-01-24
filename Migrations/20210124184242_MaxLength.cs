using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace lapr5_masterdata_viagens.Migrations
{
    public partial class MaxLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DriverDuties",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverDuties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DriverLicenseNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DriverLicenseExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypesIDs = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleDuties",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleDuties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CarPlateCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleTypeID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PathID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LineID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orientation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleDutyId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_VehicleDuties_VehicleDutyId",
                        column: x => x.VehicleDutyId,
                        principalTable: "VehicleDuties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Workblocks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartTime = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<int>(type: "int", nullable: false),
                    TripsIDs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverDutyId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VehicleDutyId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workblocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workblocks_DriverDuties_DriverDutyId",
                        column: x => x.DriverDutyId,
                        principalTable: "DriverDuties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Workblocks_VehicleDuties_VehicleDutyId",
                        column: x => x.VehicleDutyId,
                        principalTable: "VehicleDuties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PassingTimes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeInstant = table.Column<int>(type: "int", nullable: false),
                    NodeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TripId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassingTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassingTimes_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverDuties_Name",
                table: "DriverDuties",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_DriverLicenseNumber",
                table: "Drivers",
                column: "DriverLicenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PassingTimes_TripId",
                table: "PassingTimes",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_VehicleDutyId",
                table: "Trips",
                column: "VehicleDutyId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDuties_Name",
                table: "VehicleDuties",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CarPlateCode",
                table: "Vehicles",
                column: "CarPlateCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workblocks_DriverDutyId",
                table: "Workblocks",
                column: "DriverDutyId");

            migrationBuilder.CreateIndex(
                name: "IX_Workblocks_VehicleDutyId",
                table: "Workblocks",
                column: "VehicleDutyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "PassingTimes");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Workblocks");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "DriverDuties");

            migrationBuilder.DropTable(
                name: "VehicleDuties");
        }
    }
}
