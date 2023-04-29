using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CundecinosWeb.Migrations
{
    public partial class AddStatusPublication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedPrice",
                table: "PublicationComments");

            migrationBuilder.DropColumn(
                name: "ProductDescription",
                table: "PublicationComments");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Publication",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Publication");

            migrationBuilder.AddColumn<string>(
                name: "EstimatedPrice",
                table: "PublicationComments",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductDescription",
                table: "PublicationComments",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }
    }
}
