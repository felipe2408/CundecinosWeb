using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CundecinosWeb.Migrations
{
    public partial class AddModelCalificationPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalificationPerson",
                columns: table => new
                {
                    CalificationPersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Calification = table.Column<string>(type: "varchar(100)", nullable: false),
                    Observations = table.Column<string>(type: "varchar(100)", nullable: false),
                    CalificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalificationPerson", x => x.CalificationPersonID);
                    table.ForeignKey(
                        name: "FK_CalificationPerson_People_PersonID",
                        column: x => x.PersonID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalificationPerson_PersonID",
                table: "CalificationPerson",
                column: "PersonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalificationPerson");
        }
    }
}
