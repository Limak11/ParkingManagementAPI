using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ParkingManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChargeRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleTypeDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleType = table.Column<int>(type: "int", nullable: false),
                    ChargePerMinute = table.Column<double>(type: "float", nullable: false),
                    AdditionalCharge = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkingSpots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParkingSpotNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkingSpotAllocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VehicleRegistration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalCharge = table.Column<double>(type: "float", nullable: true),
                    ParkingSpotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChargeRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpotAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingSpotAllocations_ChargeRates_ChargeRateId",
                        column: x => x.ChargeRateId,
                        principalTable: "ChargeRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParkingSpotAllocations_ParkingSpots_ParkingSpotId",
                        column: x => x.ParkingSpotId,
                        principalTable: "ParkingSpots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ChargeRates",
                columns: new[] { "Id", "AdditionalCharge", "ChargePerMinute", "VehicleType", "VehicleTypeDisplayName" },
                values: new object[,]
                {
                    { new Guid("268d279a-e47e-4c93-9eb3-0a13142cad03"), 1.0, 0.10000000000000001, 1, "Small" },
                    { new Guid("b698623a-39ac-43a9-9b50-031f1a8605cb"), 1.0, 0.20000000000000001, 2, "Medium" },
                    { new Guid("c2ca03a5-e2f8-4c78-bde5-4eaa218f06da"), 1.0, 0.40000000000000002, 3, "Large" }
                });

            migrationBuilder.InsertData(
                table: "ParkingSpots",
                columns: new[] { "Id", "ParkingSpotNumber" },
                values: new object[,]
                {
                    { new Guid("495b53ed-2944-4e49-a2f2-087bfc8cb3c8"), 3 },
                    { new Guid("513c30f8-d92d-466b-ab88-1c1d5416f223"), 4 },
                    { new Guid("658f6909-fda6-4132-aefc-c66cf9d00ebe"), 1 },
                    { new Guid("8830eaf5-60bd-4715-9f3c-920379ba1d7c"), 2 },
                    { new Guid("bfcde73a-c71f-46f5-a2ab-9c89b50c0136"), 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpotAllocations_ChargeRateId",
                table: "ParkingSpotAllocations",
                column: "ChargeRateId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpotAllocations_ParkingSpotId",
                table: "ParkingSpotAllocations",
                column: "ParkingSpotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingSpotAllocations");

            migrationBuilder.DropTable(
                name: "ChargeRates");

            migrationBuilder.DropTable(
                name: "ParkingSpots");
        }
    }
}
