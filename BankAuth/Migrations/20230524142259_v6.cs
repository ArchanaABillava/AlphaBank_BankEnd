using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAuth.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AadharNum",
                table: "users",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "23452345234590");

            migrationBuilder.AddColumn<string>(
                name: "Addresss",
                table: "users",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "1676 Simons Hollow Road,Hazleton,Pennsylvania,PIN:18201");

            migrationBuilder.AddColumn<string>(
                name: "DOB",
                table: "users",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "1/07/1994");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "users",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Male");

            migrationBuilder.AddColumn<string>(
                name: "PanNum",
                table: "users",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "KWBPS2301H");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AadharNum",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Addresss",
                table: "users");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PanNum",
                table: "users");
        }
    }
}
