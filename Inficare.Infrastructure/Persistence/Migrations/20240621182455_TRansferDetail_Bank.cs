using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inficare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TRansferDetail_Bank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransferAccountName",
                table: "TransactionDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransferBankName",
                table: "TransactionDetail",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransferAccountName",
                table: "TransactionDetail");

            migrationBuilder.DropColumn(
                name: "TransferBankName",
                table: "TransactionDetail");
        }
    }
}
