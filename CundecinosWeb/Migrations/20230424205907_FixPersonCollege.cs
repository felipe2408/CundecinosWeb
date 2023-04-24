using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CundecinosWeb.Migrations
{
    public partial class FixPersonCollege : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_People_CollegeCareerId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_ExtensionId",
                table: "People");

            migrationBuilder.CreateIndex(
                name: "IX_People_CollegeCareerId",
                table: "People",
                column: "CollegeCareerId");

            migrationBuilder.CreateIndex(
                name: "IX_People_ExtensionId",
                table: "People",
                column: "ExtensionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_People_CollegeCareerId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_ExtensionId",
                table: "People");

            migrationBuilder.CreateIndex(
                name: "IX_People_CollegeCareerId",
                table: "People",
                column: "CollegeCareerId",
                unique: true,
                filter: "[CollegeCareerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_People_ExtensionId",
                table: "People",
                column: "ExtensionId",
                unique: true);
        }
    }
}
