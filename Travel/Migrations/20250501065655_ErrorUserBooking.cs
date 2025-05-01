using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel.Migrations
{
    /// <inheritdoc />
    public partial class ErrorUserBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelBookings_UserModel_UserId",
                table: "HotelBookings");

            migrationBuilder.DropTable(
                name: "UserModel");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelBookings_AspNetUsers_UserId",
                table: "HotelBookings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelBookings_AspNetUsers_UserId",
                table: "HotelBookings");

            migrationBuilder.CreateTable(
                name: "UserModel",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.Username);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_HotelBookings_UserModel_UserId",
                table: "HotelBookings",
                column: "UserId",
                principalTable: "UserModel",
                principalColumn: "Username");
        }
    }
}
