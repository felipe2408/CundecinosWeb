using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CundecinosWeb.Migrations
{
    public partial class FixModelPublicationComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PublicationID",
                table: "PublicationComments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PublicationComments_PublicationID",
                table: "PublicationComments",
                column: "PublicationID");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationComments_Publication_PublicationID",
                table: "PublicationComments",
                column: "PublicationID",
                principalTable: "Publication",
                principalColumn: "PublicationID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicationComments_Publication_PublicationID",
                table: "PublicationComments");

            migrationBuilder.DropIndex(
                name: "IX_PublicationComments_PublicationID",
                table: "PublicationComments");

            migrationBuilder.DropColumn(
                name: "PublicationID",
                table: "PublicationComments");
        }
    }
}
