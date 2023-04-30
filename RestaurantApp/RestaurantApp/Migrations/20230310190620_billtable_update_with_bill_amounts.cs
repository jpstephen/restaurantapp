using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantApp.Migrations
{
    /// <inheritdoc />
    public partial class billtable_update_with_bill_amounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "billAmountWithVat",
                table: "allBills",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "billAmountWithoutVat",
                table: "allBills",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "vatAmount",
                table: "allBills",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "billAmountWithVat",
                table: "allBills");

            migrationBuilder.DropColumn(
                name: "billAmountWithoutVat",
                table: "allBills");

            migrationBuilder.DropColumn(
                name: "vatAmount",
                table: "allBills");
        }
    }
}
