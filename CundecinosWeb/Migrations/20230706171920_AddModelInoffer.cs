using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CundecinosWeb.Migrations
{
    public partial class AddModelInoffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InofferPublicationsInofferPublicationID",
                table: "Publication",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InofferPublications",
                columns: table => new
                {
                    InofferPublicationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusInnofer = table.Column<int>(type: "int", nullable: false),
                    PublicationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    offer = table.Column<string>(type: "varchar(100)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "IX_Publication_InofferPublicationsInofferPublicationID",
                table: "Publication",
                column: "InofferPublicationsInofferPublicationID");

            migrationBuilder.CreateIndex(
                name: "IX_InofferPublications_PersonID",
                table: "InofferPublications",
                column: "PersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Publication_InofferPublications_InofferPublicationsInofferPublicationID",
                table: "Publication",
                column: "InofferPublicationsInofferPublicationID",
                principalTable: "InofferPublications",
                principalColumn: "InofferPublicationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publication_InofferPublications_InofferPublicationsInofferPublicationID",
                table: "Publication");

            migrationBuilder.DropTable(
                name: "InofferPublications");

            migrationBuilder.DropIndex(
                name: "IX_Publication_InofferPublicationsInofferPublicationID",
                table: "Publication");

            migrationBuilder.DropColumn(
                name: "InofferPublicationsInofferPublicationID",
                table: "Publication");
        }
    }
}
