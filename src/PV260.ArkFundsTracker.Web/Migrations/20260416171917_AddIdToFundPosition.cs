using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PV260.ArkFundsTracker.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddIdToFundPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_fund_positions",
                table: "fund_positions");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "fund_positions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_fund_positions",
                table: "fund_positions",
                column: "Id");
        }
        private static readonly string[] columns = new[] { "Date", "Ticker" };

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_fund_positions",
                table: "fund_positions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "fund_positions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_fund_positions",
                table: "fund_positions",
                columns: columns);
        }
    }
}
