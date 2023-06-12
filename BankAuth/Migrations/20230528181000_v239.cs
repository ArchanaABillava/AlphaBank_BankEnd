using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAuth.Migrations
{
    /// <inheritdoc />
    public partial class v239 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer_accountAlpha",
                columns: table => new
                {
                    CustomerAccountIds = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNums = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNums = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AadharNums = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PanNums = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOBs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genders = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Addressss = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkExps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OccupationNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OccupationAddresss = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationAddresss = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_accountAlpha", x => x.CustomerAccountIds);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer_accountAlpha");
        }
    }
}
