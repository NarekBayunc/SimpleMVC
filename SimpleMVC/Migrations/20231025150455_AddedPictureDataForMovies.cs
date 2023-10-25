using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddedPictureDataForMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Movies");

            migrationBuilder.AddColumn<byte[]>(
                name: "PictureData",
                table: "Movies",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureData",
                table: "Movies");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
