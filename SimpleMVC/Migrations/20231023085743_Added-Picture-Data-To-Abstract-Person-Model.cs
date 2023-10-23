using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddedPictureDataToAbstractPersonModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureURL",
                table: "Producers");

            migrationBuilder.DropColumn(
                name: "ProfilePictureURL",
                table: "Actors");

            migrationBuilder.AddColumn<byte[]>(
                name: "PictureData",
                table: "Producers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PictureData",
                table: "Actors",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureData",
                table: "Producers");

            migrationBuilder.DropColumn(
                name: "PictureData",
                table: "Actors");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureURL",
                table: "Producers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureURL",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
