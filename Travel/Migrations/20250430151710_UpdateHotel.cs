using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Hotels");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Hotels",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Hotels");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
