using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CundecinosWeb.Migrations
{
    public partial class FixModelPublicationStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InofferPublications");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Publication",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Publication");

            migrationBuilder.CreateTable(
                name: "InofferPublications",
                columns: table => new
                {
                    InofferPublicationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Offer = table.Column<string>(type: "varchar(100)", nullable: false),
                    PublicationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusInnofer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InofferPublications", x => x.InofferPublicationID);
                    table.ForeignKey(
                        name: "FK_InofferPublications_People_PersonID",
                        column: x => x.PersonID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InofferPublications_PersonID",
                table: "InofferPublications",
                column: "PersonID");
        }
    }
}
