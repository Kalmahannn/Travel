using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel.Migrations
{
    /// <inheritdoc />
    public partial class PackageMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TourPackages");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "TourPackages",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "TourPackages");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TourPackages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
