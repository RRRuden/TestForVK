using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1,
                column: "Code",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2,
                column: "Code",
                value: 2);

            migrationBuilder.UpdateData(
                table: "UsersState",
                keyColumn: "Id",
                keyValue: 1,
                column: "Code",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UsersState",
                keyColumn: "Id",
                keyValue: 2,
                column: "Code",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1,
                column: "Code",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2,
                column: "Code",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UsersState",
                keyColumn: "Id",
                keyValue: 1,
                column: "Code",
                value: 0);

            migrationBuilder.UpdateData(
                table: "UsersState",
                keyColumn: "Id",
                keyValue: 2,
                column: "Code",
                value: 1);
        }
    }
}
