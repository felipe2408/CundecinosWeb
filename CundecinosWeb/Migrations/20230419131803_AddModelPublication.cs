using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CundecinosWeb.Migrations
{
    public partial class AddModelPublication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image1",
                table: "PublicationAttachment");

            migrationBuilder.RenameColumn(
                name: "Image3",
                table: "PublicationAttachment",
                newName: "ImageThumbNail");

            migrationBuilder.RenameColumn(
                name: "Image2",
                table: "PublicationAttachment",
                newName: "ImageScreen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageThumbNail",
                table: "PublicationAttachment",
                newName: "Image3");

            migrationBuilder.RenameColumn(
                name: "ImageScreen",
                table: "PublicationAttachment",
                newName: "Image2");

            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "PublicationAttachment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
