using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CundecinosWeb.Migrations
{
    public partial class AddModelAttachmentComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductUrl",
                table: "PublicationComments");

            migrationBuilder.AddColumn<int>(
                name: "StatusInnofer",
                table: "PublicationComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CommentAttachment",
                columns: table => new
                {
                    CommentAttachmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicationCommentsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageScreen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageThumbNail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentAttachment", x => x.CommentAttachmentID);
                    table.ForeignKey(
                        name: "FK_CommentAttachment_PublicationComments_PublicationCommentsID",
                        column: x => x.PublicationCommentsID,
                        principalTable: "PublicationComments",
                        principalColumn: "PublicationCommentsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentAttachment_PublicationCommentsID",
                table: "CommentAttachment",
                column: "PublicationCommentsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentAttachment");

            migrationBuilder.DropColumn(
                name: "StatusInnofer",
                table: "PublicationComments");

            migrationBuilder.AddColumn<string>(
                name: "ProductUrl",
                table: "PublicationComments",
                type: "varchar(100)",
                nullable: true);
        }
    }
}
