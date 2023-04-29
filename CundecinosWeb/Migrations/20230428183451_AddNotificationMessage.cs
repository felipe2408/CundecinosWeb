using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CundecinosWeb.Migrations
{
    public partial class AddNotificationMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationMessage",
                columns: table => new
                {
                    NotificationMessageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "varchar(100)", nullable: false),
                    SenderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddresseeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    View = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationMessage", x => x.NotificationMessageID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationMessage");
        }
    }
}
