using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Retail.Data.Migrations
{
    /// <inheritdoc />
    public partial class Mdew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternalTransfer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromAccount = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ToAccount = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    SchedulingOption = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    TransferDate = table.Column<DateTime>(type: "date", nullable: true),
                    Frequency = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Delivery = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    EndBy = table.Column<DateTime>(type: "date", nullable: true),
                    NumberOfTransfers = table.Column<long>(type: "bigint", nullable: true),
                    Memo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalTransfer", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalTransfer");
        }
    }
}
