using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Retail.Data.Migrations
{
    /// <inheritdoc />
    public partial class BalBa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BA",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SocialSecurityNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BAD",
                columns: table => new
                {
                    AccountNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AccountBalance = table.Column<decimal>(type: "decimal(13,2)", precision: 13, scale: 2, nullable: false),
                    AccountAvailableBalance = table.Column<decimal>(type: "decimal(13,2)", precision: 13, scale: 2, nullable: false),
                    AccountHoldBalance = table.Column<decimal>(type: "decimal(13,2)", precision: 13, scale: 2, nullable: false),
                    AccountHolds = table.Column<decimal>(type: "decimal(13,2)", precision: 13, scale: 2, nullable: false),
                    AccountDescription = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AccountLabel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BAId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAD", x => x.AccountNumber);
                    table.ForeignKey(
                        name: "FK_BAD_BA_BAId",
                        column: x => x.BAId,
                        principalTable: "BA",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BAD_BAId",
                table: "BAD",
                column: "BAId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BAD");

            migrationBuilder.DropTable(
                name: "BA");
        }
    }
}
