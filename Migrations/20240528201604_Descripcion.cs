using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quotes_project.Migrations
{
    /// <inheritdoc />
    public partial class Descripcion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomersEntity",
                columns: table => new
                {
                    id_customer = table.Column<int>(type: "int", nullable: false),
                    customer_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersEntity", x => x.id_customer);
                });

            migrationBuilder.CreateTable(
                name: "ProductsEntity",
                columns: table => new
                {
                    id_Product = table.Column<int>(type: "int", nullable: false),
                    product_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsEntity", x => x.id_Product);
                });

            migrationBuilder.CreateTable(
                name: "QuotesEntity",
                columns: table => new
                {
                    id_quote = table.Column<int>(type: "int", nullable: false),
                    id_customer = table.Column<int>(type: "int", nullable: false),
                    id_product = table.Column<int>(type: "int", nullable: false),
                    id_user = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "money", nullable: false),
                    dDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotesEntity", x => x.id_quote);
                });

            migrationBuilder.CreateTable(
                name: "UsersEntity",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersEntity", x => x.id_user);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomersEntity");

            migrationBuilder.DropTable(
                name: "ProductsEntity");

            migrationBuilder.DropTable(
                name: "QuotesEntity");

            migrationBuilder.DropTable(
                name: "UsersEntity");
        }
    }
}
