using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quotes_project.Migrations
{
    /// <inheritdoc />
    public partial class DataInsert1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersEntity",
                table: "UsersEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuotesEntity",
                table: "QuotesEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsEntity",
                table: "ProductsEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomersEntity",
                table: "CustomersEntity");

            migrationBuilder.RenameTable(
                name: "UsersEntity",
                newName: "UserEntity");

            migrationBuilder.RenameTable(
                name: "QuotesEntity",
                newName: "QuoteEntity");

            migrationBuilder.RenameTable(
                name: "ProductsEntity",
                newName: "ProductEntity");

            migrationBuilder.RenameTable(
                name: "CustomersEntity",
                newName: "CustomerEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEntity",
                table: "UserEntity",
                column: "id_user");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuoteEntity",
                table: "QuoteEntity",
                column: "id_quote");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductEntity",
                table: "ProductEntity",
                column: "id_Product");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerEntity",
                table: "CustomerEntity",
                column: "id_customer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEntity",
                table: "UserEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuoteEntity",
                table: "QuoteEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductEntity",
                table: "ProductEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerEntity",
                table: "CustomerEntity");

            migrationBuilder.RenameTable(
                name: "UserEntity",
                newName: "UsersEntity");

            migrationBuilder.RenameTable(
                name: "QuoteEntity",
                newName: "QuotesEntity");

            migrationBuilder.RenameTable(
                name: "ProductEntity",
                newName: "ProductsEntity");

            migrationBuilder.RenameTable(
                name: "CustomerEntity",
                newName: "CustomersEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersEntity",
                table: "UsersEntity",
                column: "id_user");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuotesEntity",
                table: "QuotesEntity",
                column: "id_quote");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsEntity",
                table: "ProductsEntity",
                column: "id_Product");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomersEntity",
                table: "CustomersEntity",
                column: "id_customer");
        }
    }
}
