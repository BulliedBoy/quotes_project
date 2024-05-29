using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quotes_project.Migrations
{
    /// <inheritdoc />
    public partial class Cambiodenombredecolumna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "customername",
                table: "QuoteEntity",
                newName: "customer_name");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dDate",
                table: "QuoteEntity",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "customer_name",
                table: "QuoteEntity",
                newName: "customername");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "dDate",
                table: "QuoteEntity",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
