using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CundecinosWeb.Migrations
{
    public partial class FixModelCommentISACTIVE2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCalificationPerson",
                table: "PublicationComments");

            migrationBuilder.AddColumn<bool>(
                name: "IsCalificationPerson",
                table: "Publication",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCalificationPerson",
                table: "Publication");

            migrationBuilder.AddColumn<bool>(
                name: "IsCalificationPerson",
                table: "PublicationComments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
