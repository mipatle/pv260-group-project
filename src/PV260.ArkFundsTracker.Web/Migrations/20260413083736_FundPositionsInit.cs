using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PV260.ArkFundsTracker.Web.Migrations
{
    /// <inheritdoc />
    public partial class FundPositionsInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fund_positions",
                columns: table => new
                {
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Ticker = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Fund = table.Column<string>(type: "text", nullable: false, defaultValue: "ARKK"),
                    Company = table.Column<string>(type: "text", nullable: false),
                    Cusip = table.Column<string>(type: "text", nullable: false),
                    Shares = table.Column<decimal>(type: "numeric", nullable: false),
                    MarketValue = table.Column<decimal>(type: "numeric", nullable: false),
                    WeightPercentage = table.Column<decimal>(type: "numeric", nullable: false),
                    AdminId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fund_positions", x => new { x.Date, x.Ticker });
                    table.CheckConstraint("ck_fund_positions_fund", "\"Fund\" = 'ARKK'");
                });

            migrationBuilder.CreateIndex(
                name: "idx_fund_positions_ticker",
                table: "fund_positions",
                column: "Ticker");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fund_positions");
        }
    }
}
