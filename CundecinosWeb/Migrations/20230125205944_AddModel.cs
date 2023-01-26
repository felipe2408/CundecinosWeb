using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CundecinosWeb.Migrations
{
    public partial class AddModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstName = table.Column<string>(type: "varchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(100)", nullable: false),
                    IdentificationNumber = table.Column<string>(type: "varchar(20)", nullable: false),
                    CellPhone = table.Column<string>(type: "varchar(20)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    BirthYear = table.Column<short>(type: "smallint", nullable: true),
                    Company = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    ARLID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "CollegeCareer",
                columns: table => new
                {
                    CollegeCareerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegeCareer", x => x.CollegeCareerId);
                    table.ForeignKey(
                        name: "FK_CollegeCareer_People_PersonID",
                        column: x => x.PersonID,
                        principalTable: "People",
                        principalColumn: "PersonID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollegeCareer_PersonID",
                table: "CollegeCareer",
                column: "PersonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollegeCareer");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
