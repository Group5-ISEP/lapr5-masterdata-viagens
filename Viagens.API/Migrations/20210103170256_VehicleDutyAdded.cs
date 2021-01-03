using Microsoft.EntityFrameworkCore.Migrations;

namespace lapr5_masterdata_viagens.Migrations
{
    public partial class VehicleDutyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VehicleDutyId",
                table: "Trips",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkblockId",
                table: "Trips",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VehicleDuties",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleDuties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workblocks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    StartTime = table.Column<int>(type: "INTEGER", nullable: false),
                    EndTime = table.Column<int>(type: "INTEGER", nullable: false),
                    VehicleDutyId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workblocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workblocks_VehicleDuties_VehicleDutyId",
                        column: x => x.VehicleDutyId,
                        principalTable: "VehicleDuties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_VehicleDutyId",
                table: "Trips",
                column: "VehicleDutyId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_WorkblockId",
                table: "Trips",
                column: "WorkblockId");

            migrationBuilder.CreateIndex(
                name: "IX_Workblocks_VehicleDutyId",
                table: "Workblocks",
                column: "VehicleDutyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_VehicleDuties_VehicleDutyId",
                table: "Trips",
                column: "VehicleDutyId",
                principalTable: "VehicleDuties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Workblocks_WorkblockId",
                table: "Trips",
                column: "WorkblockId",
                principalTable: "Workblocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_VehicleDuties_VehicleDutyId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Workblocks_WorkblockId",
                table: "Trips");

            migrationBuilder.DropTable(
                name: "Workblocks");

            migrationBuilder.DropTable(
                name: "VehicleDuties");

            migrationBuilder.DropIndex(
                name: "IX_Trips_VehicleDutyId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_WorkblockId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "VehicleDutyId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "WorkblockId",
                table: "Trips");
        }
    }
}
