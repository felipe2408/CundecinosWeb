using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CundecinosWeb.Migrations
{
    public partial class AddModelCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryID",
                table: "Publication",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publication_CategoryID",
                table: "Publication",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Publication_Category_CategoryID",
                table: "Publication",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publication_Category_CategoryID",
                table: "Publication");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Publication_CategoryID",
                table: "Publication");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Publication");
        }
    }
}
