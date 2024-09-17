using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Arcadia.Migrations
{
    /// <inheritdoc />
    public partial class Adding_EmployeeFeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeFeedings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAnimal = table.Column<int>(type: "int", nullable: false),
                    Food = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    IdWeightUnit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFeedings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeFeedings_Animals_IdAnimal",
                        column: x => x.IdAnimal,
                        principalTable: "Animals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeFeedings_WeightUnits_IdWeightUnit",
                        column: x => x.IdWeightUnit,
                        principalTable: "WeightUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFeedings_IdAnimal",
                table: "EmployeeFeedings",
                column: "IdAnimal");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFeedings_IdWeightUnit",
                table: "EmployeeFeedings",
                column: "IdWeightUnit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeFeedings");
        }
    }
}
