using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace lapr5_masterdata_viagens.Migrations
{
    public partial class FirstTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CarPlateCode = table.Column<string>(type: "TEXT", nullable: false),
                    VIN = table.Column<string>(type: "TEXT", nullable: true),
                    ServiceStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    VehicleTypeID = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CarPlateCode",
                table: "Vehicles",
                column: "CarPlateCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
