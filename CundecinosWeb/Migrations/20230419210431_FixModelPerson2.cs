using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CundecinosWeb.Migrations
{
    public partial class FixModelPerson2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonPublication");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_PersonID",
                table: "Publication",
                column: "PersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Publication_People_PersonID",
                table: "Publication",
                column: "PersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publication_People_PersonID",
                table: "Publication");

            migrationBuilder.DropIndex(
                name: "IX_Publication_PersonID",
                table: "Publication");

            migrationBuilder.CreateTable(
                name: "PersonPublication",
                columns: table => new
                {
                    PersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPublication", x => new { x.PersonID, x.PublicationID });
                    table.ForeignKey(
                        name: "FK_PersonPublication_People_PersonID",
                        column: x => x.PersonID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonPublication_Publication_PublicationID",
                        column: x => x.PublicationID,
                        principalTable: "Publication",
                        principalColumn: "PublicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonPublication_PublicationID",
                table: "PersonPublication",
                column: "PublicationID");
        }
    }
}
