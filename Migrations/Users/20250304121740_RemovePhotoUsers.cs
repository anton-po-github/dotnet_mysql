using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_mysql.Migrations.Users
{
    /// <inheritdoc />
    public partial class RemovePhotoUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo_url",
                table: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "photo_url",
                table: "users",
                type: "longblob",
                nullable: true);
        }
    }
}
