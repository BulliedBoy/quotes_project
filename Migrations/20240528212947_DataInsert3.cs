using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quotes_project.Migrations
{
    /// <inheritdoc />
    public partial class DataInsert3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "customername",
                table: "QuoteEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "customername",
                table: "QuoteEntity");
        }
    }
}
