using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Arcadia.Migrations
{
    /// <inheritdoc />
    public partial class Ajout_Images_ZooServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pic",
                table: "ZooServices",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pic",
                table: "ZooServices");
        }
    }
}
