using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAuth.Migrations
{
    /// <inheritdoc />
    public partial class v65 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Modified_At",
                table: "loan_details",
                newName: "Modified_at");

            migrationBuilder.RenameColumn(
                name: "Created_At",
                table: "loan_details",
                newName: "Created_at");

            migrationBuilder.AddColumn<string>(
                name: "PropertyArea",
                table: "loan_details",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyLoc",
                table: "loan_details",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyValue",
                table: "loan_details",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PropertyArea",
                table: "loan_details");

            migrationBuilder.DropColumn(
                name: "PropertyLoc",
                table: "loan_details");

            migrationBuilder.DropColumn(
                name: "PropertyValue",
                table: "loan_details");

            migrationBuilder.RenameColumn(
                name: "Modified_at",
                table: "loan_details",
                newName: "Modified_At");

            migrationBuilder.RenameColumn(
                name: "Created_at",
                table: "loan_details",
                newName: "Created_At");
        }
    }
}
