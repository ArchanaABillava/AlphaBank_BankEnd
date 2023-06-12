using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAuth.Migrations
{
    /// <inheritdoc />
    public partial class v240 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkExps",
                table: "customer_accountAlpha",
                newName: "WorkExp");

            migrationBuilder.RenameColumn(
                name: "UserNames",
                table: "customer_accountAlpha",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "PanNums",
                table: "customer_accountAlpha",
                newName: "PanNum");

            migrationBuilder.RenameColumn(
                name: "OrganizationNames",
                table: "customer_accountAlpha",
                newName: "OrganizationName");

            migrationBuilder.RenameColumn(
                name: "OrganizationAddresss",
                table: "customer_accountAlpha",
                newName: "OrganizationAddress");

            migrationBuilder.RenameColumn(
                name: "OccupationNames",
                table: "customer_accountAlpha",
                newName: "OccupationName");

            migrationBuilder.RenameColumn(
                name: "OccupationAddresss",
                table: "customer_accountAlpha",
                newName: "OccupationAddress");

            migrationBuilder.RenameColumn(
                name: "Genders",
                table: "customer_accountAlpha",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "EmpTypes",
                table: "customer_accountAlpha",
                newName: "EmpType");

            migrationBuilder.RenameColumn(
                name: "Emails",
                table: "customer_accountAlpha",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "DOBs",
                table: "customer_accountAlpha",
                newName: "DOB");

            migrationBuilder.RenameColumn(
                name: "ContactNums",
                table: "customer_accountAlpha",
                newName: "ContactNum");

            migrationBuilder.RenameColumn(
                name: "Addressss",
                table: "customer_accountAlpha",
                newName: "Addresss");

            migrationBuilder.RenameColumn(
                name: "AccountNums",
                table: "customer_accountAlpha",
                newName: "AccountNum");

            migrationBuilder.RenameColumn(
                name: "AadharNums",
                table: "customer_accountAlpha",
                newName: "AadharNum");

            migrationBuilder.RenameColumn(
                name: "CustomerAccountIds",
                table: "customer_accountAlpha",
                newName: "CustomerAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkExp",
                table: "customer_accountAlpha",
                newName: "WorkExps");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "customer_accountAlpha",
                newName: "UserNames");

            migrationBuilder.RenameColumn(
                name: "PanNum",
                table: "customer_accountAlpha",
                newName: "PanNums");

            migrationBuilder.RenameColumn(
                name: "OrganizationName",
                table: "customer_accountAlpha",
                newName: "OrganizationNames");

            migrationBuilder.RenameColumn(
                name: "OrganizationAddress",
                table: "customer_accountAlpha",
                newName: "OrganizationAddresss");

            migrationBuilder.RenameColumn(
                name: "OccupationName",
                table: "customer_accountAlpha",
                newName: "OccupationNames");

            migrationBuilder.RenameColumn(
                name: "OccupationAddress",
                table: "customer_accountAlpha",
                newName: "OccupationAddresss");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "customer_accountAlpha",
                newName: "Genders");

            migrationBuilder.RenameColumn(
                name: "EmpType",
                table: "customer_accountAlpha",
                newName: "EmpTypes");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "customer_accountAlpha",
                newName: "Emails");

            migrationBuilder.RenameColumn(
                name: "DOB",
                table: "customer_accountAlpha",
                newName: "DOBs");

            migrationBuilder.RenameColumn(
                name: "ContactNum",
                table: "customer_accountAlpha",
                newName: "ContactNums");

            migrationBuilder.RenameColumn(
                name: "Addresss",
                table: "customer_accountAlpha",
                newName: "Addressss");

            migrationBuilder.RenameColumn(
                name: "AccountNum",
                table: "customer_accountAlpha",
                newName: "AccountNums");

            migrationBuilder.RenameColumn(
                name: "AadharNum",
                table: "customer_accountAlpha",
                newName: "AadharNums");

            migrationBuilder.RenameColumn(
                name: "CustomerAccountId",
                table: "customer_accountAlpha",
                newName: "CustomerAccountIds");
        }
    }
}
