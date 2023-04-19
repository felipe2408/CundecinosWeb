using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CundecinosWeb.Migrations
{
    public partial class Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicationAttachment_Publication_PublicationID",
                table: "PublicationAttachment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PublicationAttachment",
                table: "PublicationAttachment");

            migrationBuilder.RenameTable(
                name: "PublicationAttachment",
                newName: "PublicationAttachments");

            migrationBuilder.RenameIndex(
                name: "IX_PublicationAttachment_PublicationID",
                table: "PublicationAttachments",
                newName: "IX_PublicationAttachments_PublicationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublicationAttachments",
                table: "PublicationAttachments",
                column: "PublicationAttachmentID");

            migrationBuilder.CreateTable(
                name: "PublicationComments",
                columns: table => new
                {
                    PublicationCommentsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "varchar(100)", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationComments", x => x.PublicationCommentsID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationAttachments_Publication_PublicationID",
                table: "PublicationAttachments",
                column: "PublicationID",
                principalTable: "Publication",
                principalColumn: "PublicationID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicationAttachments_Publication_PublicationID",
                table: "PublicationAttachments");

            migrationBuilder.DropTable(
                name: "PublicationComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PublicationAttachments",
                table: "PublicationAttachments");

            migrationBuilder.RenameTable(
                name: "PublicationAttachments",
                newName: "PublicationAttachment");

            migrationBuilder.RenameIndex(
                name: "IX_PublicationAttachments_PublicationID",
                table: "PublicationAttachment",
                newName: "IX_PublicationAttachment_PublicationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublicationAttachment",
                table: "PublicationAttachment",
                column: "PublicationAttachmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationAttachment_Publication_PublicationID",
                table: "PublicationAttachment",
                column: "PublicationID",
                principalTable: "Publication",
                principalColumn: "PublicationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
