using Microsoft.EntityFrameworkCore.Migrations;

namespace itec_backend.Migrations
{
    public partial class removedenities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationEntities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocationEntities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(767)", nullable: false),
                    CountryEntityId = table.Column<string>(type: "varchar(767)", nullable: true),
                    Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<float>(type: "float", nullable: false),
                    XCoordonate = table.Column<string>(type: "text", nullable: true),
                    YCoordonate = table.Column<string>(type: "text", nullable: true)
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
    }
}
