using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CundecinosWeb.Migrations
{
    public partial class FixModels2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publication_Category_CategoryID",
                table: "Publication");

            migrationBuilder.DropForeignKey(
                name: "FK_Publication_InofferPublications_InofferPublicationsInofferPublicationID",
                table: "Publication");

            migrationBuilder.DropIndex(
                name: "IX_Publication_InofferPublicationsInofferPublicationID",
                table: "Publication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "InofferPublicationsInofferPublicationID",
                table: "Publication");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "categories");

            migrationBuilder.RenameColumn(
                name: "offer",
                table: "InofferPublications",
                newName: "Offer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Publication_categories_CategoryID",
                table: "Publication",
                column: "CategoryID",
                principalTable: "categories",
                principalColumn: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publication_categories_CategoryID",
                table: "Publication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "Offer",
                table: "InofferPublications",
                newName: "offer");

            migrationBuilder.AddColumn<Guid>(
                name: "InofferPublicationsInofferPublicationID",
                table: "Publication",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_InofferPublicationsInofferPublicationID",
                table: "Publication",
                column: "InofferPublicationsInofferPublicationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Publication_Category_CategoryID",
                table: "Publication",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Publication_InofferPublications_InofferPublicationsInofferPublicationID",
                table: "Publication",
                column: "InofferPublicationsInofferPublicationID",
                principalTable: "InofferPublications",
                principalColumn: "InofferPublicationID");
        }
    }
}
