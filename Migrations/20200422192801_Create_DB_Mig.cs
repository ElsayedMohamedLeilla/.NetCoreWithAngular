using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Migrations
{
    public partial class Create_DB_Mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Table_Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    IsEmailConfirmed = table.Column<bool>(nullable: false),
                    EmailConfirmationToken = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Table_Users",
                columns: new[] { "UserId", "AddedDate", "Email", "EmailConfirmationToken", "IsEmailConfirmed", "LastName", "Name", "Password", "PhoneNumber", "Username" },
                values: new object[] { 1, new DateTime(2020, 4, 22, 21, 28, 0, 351, DateTimeKind.Local).AddTicks(8319), "elsayed.mohammed70@gmail.com", "74888817", false, "TestLeilla", "Test Elsayed Mohammed", "123", "01069879123", "TestElsyed_Leilla" });

            migrationBuilder.InsertData(
                table: "Table_Users",
                columns: new[] { "UserId", "AddedDate", "Email", "EmailConfirmationToken", "IsEmailConfirmed", "LastName", "Name", "Password", "PhoneNumber", "Username" },
                values: new object[] { 2, new DateTime(2020, 4, 22, 21, 28, 0, 351, DateTimeKind.Local).AddTicks(9631), "elsayed.mohammed70@gmail.com", "708fa6f", false, "TestLeilla", "Test Elsayed Ali", "1321", "01076566765", "TestElsyed_Ali" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Table_Users");
        }
    }
}
