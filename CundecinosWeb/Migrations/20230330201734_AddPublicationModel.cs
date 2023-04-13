using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CundecinosWeb.Migrations
{
    public partial class AddPublicationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Publication",
                columns: table => new
                {
                    PublicationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Qualification = table.Column<string>(type: "varchar(100)", nullable: false),
                    Content = table.Column<string>(type: "varchar(100)", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publication", x => x.PublicationID);
                });

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

            migrationBuilder.CreateTable(
                name: "PublicationAttachment",
                columns: table => new
                {
                    PublicationAttachmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationAttachment", x => x.PublicationAttachmentID);
                    table.ForeignKey(
                        name: "FK_PublicationAttachment_Publication_PublicationID",
                        column: x => x.PublicationID,
                        principalTable: "Publication",
                        principalColumn: "PublicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonPublication_PublicationID",
                table: "PersonPublication",
                column: "PublicationID");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationAttachment_PublicationID",
                table: "PublicationAttachment",
                column: "PublicationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonPublication");

            migrationBuilder.DropTable(
                name: "PublicationAttachment");

            migrationBuilder.DropTable(
                name: "Publication");
        }
    }
}
