using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace itec_backend.Migrations
{
    public partial class addcountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountryEntities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CovidVaccinationRate = table.Column<float>(nullable: false),
                    CovidVaccinationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationEntities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<float>(nullable: false),
                    XCoordonate = table.Column<string>(nullable: true),
                    YCoordonate = table.Column<string>(nullable: true),
                    CountryEntityId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationEntities_CountryEntities_CountryEntityId",
                        column: x => x.CountryEntityId,
                        principalTable: "CountryEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationEntities_CountryEntityId",
                table: "LocationEntities",
                column: "CountryEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationEntities");

            migrationBuilder.DropTable(
                name: "CountryEntities");
        }
    }
}
