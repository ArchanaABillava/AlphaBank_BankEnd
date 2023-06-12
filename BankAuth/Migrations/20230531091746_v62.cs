using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAuth.Migrations
{
    /// <inheritdoc />
    public partial class v62 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "loan_details",
                columns: table => new
                {
                    LoanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoanType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoanAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Interest = table.Column<float>(type: "real", nullable: true),
                    Tenure = table.Column<int>(type: "int", nullable: true),
                    LoanEmi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoanTotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthlyIncome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnualIncome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherEmi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoanStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoanEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loan_details", x => x.LoanId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "loan_details");
        }
    }
}
