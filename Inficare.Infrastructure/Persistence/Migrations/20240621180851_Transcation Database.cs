using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inficare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TranscationDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Balance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AmountCurrency = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Balance_Profiles_UserId",
                        column: x => x.UserId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrAmount = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    CrAmount = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TranscationCurrency = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TranscationRate = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionDetail_Profiles_UserId",
                        column: x => x.UserId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Balance_UserId",
                table: "Balance",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetail_UserId",
                table: "TransactionDetail",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Balance");

            migrationBuilder.DropTable(
                name: "TransactionDetail");
        }
    }
}
