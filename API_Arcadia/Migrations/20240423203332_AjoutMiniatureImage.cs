using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Arcadia.Migrations
{
    /// <inheritdoc />
    public partial class AjoutMiniatureImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MiniSlug",
                table: "HabitatImages",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiniSlug",
                table: "AnimalImages",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiniSlug",
                table: "HabitatImages");

            migrationBuilder.DropColumn(
                name: "MiniSlug",
                table: "AnimalImages");
        }
    }
}
