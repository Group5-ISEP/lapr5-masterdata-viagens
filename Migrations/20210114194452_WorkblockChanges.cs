using Microsoft.EntityFrameworkCore.Migrations;

namespace lapr5_masterdata_viagens.Migrations
{
    public partial class WorkblockChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Workblocks_WorkblockId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_WorkblockId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "WorkblockId",
                table: "Trips");

            migrationBuilder.AddColumn<string>(
                name: "DriverDutyId",
                table: "Workblocks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TripsIDs",
                table: "Workblocks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DriverDuties",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverDuties", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workblocks_DriverDutyId",
                table: "Workblocks",
                column: "DriverDutyId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDuties_Name",
                table: "VehicleDuties",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverDuties_Name",
                table: "DriverDuties",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Workblocks_DriverDuties_DriverDutyId",
                table: "Workblocks",
                column: "DriverDutyId",
                principalTable: "DriverDuties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workblocks_DriverDuties_DriverDutyId",
                table: "Workblocks");

            migrationBuilder.DropTable(
                name: "DriverDuties");

            migrationBuilder.DropIndex(
                name: "IX_Workblocks_DriverDutyId",
                table: "Workblocks");

            migrationBuilder.DropIndex(
                name: "IX_VehicleDuties_Name",
                table: "VehicleDuties");

            migrationBuilder.DropColumn(
                name: "DriverDutyId",
                table: "Workblocks");

            migrationBuilder.DropColumn(
                name: "TripsIDs",
                table: "Workblocks");

            migrationBuilder.AddColumn<string>(
                name: "WorkblockId",
                table: "Trips",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_WorkblockId",
                table: "Trips",
                column: "WorkblockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Workblocks_WorkblockId",
                table: "Trips",
                column: "WorkblockId",
                principalTable: "Workblocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
