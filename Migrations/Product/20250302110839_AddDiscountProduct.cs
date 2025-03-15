using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_mysql.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "discount",
                table: "products",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "discount",
                table: "products");
        }
    }
}
