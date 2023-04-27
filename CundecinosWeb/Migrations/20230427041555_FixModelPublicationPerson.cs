using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CundecinosWeb.Migrations
{
    public partial class FixModelPublicationPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicationID",
                table: "PublicationComments",
                newName: "PersonID");

            migrationBuilder.RenameColumn(
                name: "PublicationDate",
                table: "PublicationComments",
                newName: "CommentDate");

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

            migrationBuilder.AddColumn<string>(
                name: "ProductUrl",
                table: "PublicationComments",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EstimatedPrice",
                table: "Publication",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductDescription",
                table: "Publication",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationComments_PersonID",
                table: "PublicationComments",
                column: "PersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationComments_People_PersonID",
                table: "PublicationComments",
                column: "PersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicationComments_People_PersonID",
                table: "PublicationComments");

            migrationBuilder.DropIndex(
                name: "IX_PublicationComments_PersonID",
                table: "PublicationComments");

            migrationBuilder.DropColumn(
                name: "EstimatedPrice",
                table: "PublicationComments");

            migrationBuilder.DropColumn(
                name: "ProductDescription",
                table: "PublicationComments");

            migrationBuilder.DropColumn(
                name: "ProductUrl",
                table: "PublicationComments");

            migrationBuilder.DropColumn(
                name: "EstimatedPrice",
                table: "Publication");

            migrationBuilder.DropColumn(
                name: "ProductDescription",
                table: "Publication");

            migrationBuilder.RenameColumn(
                name: "PersonID",
                table: "PublicationComments",
                newName: "PublicationID");

            migrationBuilder.RenameColumn(
                name: "CommentDate",
                table: "PublicationComments",
                newName: "PublicationDate");
        }
    }
}
