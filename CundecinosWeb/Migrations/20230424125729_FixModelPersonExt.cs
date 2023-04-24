using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CundecinosWeb.Migrations
{
    public partial class FixModelPersonExt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollegeCareer_People_PersonID",
                table: "CollegeCareer");

            migrationBuilder.DropForeignKey(
                name: "FK_Extensions_People_PersonID",
                table: "Extensions");

            migrationBuilder.DropIndex(
                name: "IX_Extensions_PersonID",
                table: "Extensions");

            migrationBuilder.DropIndex(
                name: "IX_CollegeCareer_PersonID",
                table: "CollegeCareer");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "Extensions");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "CollegeCareer");

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

            migrationBuilder.AddForeignKey(
                name: "FK_People_CollegeCareer_CollegeCareerId",
                table: "People",
                column: "CollegeCareerId",
                principalTable: "CollegeCareer",
                principalColumn: "CollegeCareerId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Extensions_ExtensionId",
                table: "People",
                column: "ExtensionId",
                principalTable: "Extensions",
                principalColumn: "ExtensionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_CollegeCareer_CollegeCareerId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Extensions_ExtensionId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_CollegeCareerId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_ExtensionId",
                table: "People");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonID",
                table: "Extensions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonID",
                table: "CollegeCareer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Extensions_PersonID",
                table: "Extensions",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_CollegeCareer_PersonID",
                table: "CollegeCareer",
                column: "PersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_CollegeCareer_People_PersonID",
                table: "CollegeCareer",
                column: "PersonID",
                principalTable: "People",
                principalColumn: "PersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Extensions_People_PersonID",
                table: "Extensions",
                column: "PersonID",
                principalTable: "People",
                principalColumn: "PersonID");
        }
    }
}
