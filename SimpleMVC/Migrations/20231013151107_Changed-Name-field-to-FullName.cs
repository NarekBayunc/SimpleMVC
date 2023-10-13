using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleMVC.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNamefieldtoFullName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Producers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Actors",
                newName: "FullName");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Producers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Actors",
                newName: "Name");
        }
    }
}
