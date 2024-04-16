using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Arcadia.Data.Migrations
{
    /// <inheritdoc />
    public partial class DbCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diets",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Habitats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Healths",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Healths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpeningHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MorningOpening = table.Column<TimeOnly>(type: "time", nullable: false),
                    MorningClosing = table.Column<TimeOnly>(type: "time", nullable: false),
                    AfternoonOpening = table.Column<TimeOnly>(type: "time", nullable: false),
                    AfternoonClosing = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningHours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pseudo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsValidated = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SizeUnits",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Abbr = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeightUnits",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Abbr = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZooServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullPrice = table.Column<float>(type: "real", nullable: false),
                    ChildPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZooServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HabitatImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slug = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IdHabitat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitatImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabitatImages_Habitats_IdHabitat",
                        column: x => x.IdHabitat,
                        principalTable: "Habitats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Speciess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ScientificName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaleMaxSize = table.Column<float>(type: "real", nullable: false),
                    FemaleMaxSize = table.Column<float>(type: "real", nullable: false),
                    MaleMaxWeight = table.Column<float>(type: "real", nullable: false),
                    FemaleMaxWeight = table.Column<float>(type: "real", nullable: false),
                    IdSizeUnit = table.Column<byte>(type: "tinyint", nullable: false),
                    IdWeightUnit = table.Column<byte>(type: "tinyint", nullable: false),
                    Lifespan = table.Column<byte>(type: "tinyint", nullable: false),
                    IdDiet = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speciess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Speciess_Diets_IdDiet",
                        column: x => x.IdDiet,
                        principalTable: "Diets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Speciess_SizeUnits_IdSizeUnit",
                        column: x => x.IdSizeUnit,
                        principalTable: "SizeUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Speciess_WeightUnits_IdWeightUnit",
                        column: x => x.IdWeightUnit,
                        principalTable: "WeightUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsMale = table.Column<bool>(type: "bit", nullable: false),
                    IdSpecies = table.Column<int>(type: "int", nullable: false),
                    IdHealth = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animals_Healths_IdHealth",
                        column: x => x.IdHealth,
                        principalTable: "Healths",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Animals_Speciess_IdSpecies",
                        column: x => x.IdSpecies,
                        principalTable: "Speciess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpeciesHabitats",
                columns: table => new
                {
                    IdSpecies = table.Column<int>(type: "int", nullable: false),
                    IdHabitat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeciesHabitats", x => new { x.IdHabitat, x.IdSpecies });
                    table.ForeignKey(
                        name: "FK_SpeciesHabitats_Habitats_IdHabitat",
                        column: x => x.IdHabitat,
                        principalTable: "Habitats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpeciesHabitats_Speciess_IdSpecies",
                        column: x => x.IdSpecies,
                        principalTable: "Speciess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimalImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slug = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IdAnimal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalImages_Animals_IdAnimal",
                        column: x => x.IdAnimal,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VetVisits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Food = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FoodWeight = table.Column<float>(type: "real", nullable: false),
                    IdWeightUnit = table.Column<byte>(type: "tinyint", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAnimal = table.Column<int>(type: "int", nullable: false),
                    IdVet = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VetVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VetVisits_Animals_IdAnimal",
                        column: x => x.IdAnimal,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VetVisits_WeightUnits_IdWeightUnit",
                        column: x => x.IdWeightUnit,
                        principalTable: "WeightUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalImages_IdAnimal",
                table: "AnimalImages",
                column: "IdAnimal");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_IdHealth",
                table: "Animals",
                column: "IdHealth");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_IdSpecies",
                table: "Animals",
                column: "IdSpecies");

            migrationBuilder.CreateIndex(
                name: "IX_HabitatImages_IdHabitat",
                table: "HabitatImages",
                column: "IdHabitat");

            migrationBuilder.CreateIndex(
                name: "IX_SpeciesHabitats_IdSpecies",
                table: "SpeciesHabitats",
                column: "IdSpecies");

            migrationBuilder.CreateIndex(
                name: "IX_Speciess_IdDiet",
                table: "Speciess",
                column: "IdDiet");

            migrationBuilder.CreateIndex(
                name: "IX_Speciess_IdSizeUnit",
                table: "Speciess",
                column: "IdSizeUnit");

            migrationBuilder.CreateIndex(
                name: "IX_Speciess_IdWeightUnit",
                table: "Speciess",
                column: "IdWeightUnit");

            migrationBuilder.CreateIndex(
                name: "IX_VetVisits_IdAnimal",
                table: "VetVisits",
                column: "IdAnimal");

            migrationBuilder.CreateIndex(
                name: "IX_VetVisits_IdWeightUnit",
                table: "VetVisits",
                column: "IdWeightUnit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalImages");

            migrationBuilder.DropTable(
                name: "HabitatImages");

            migrationBuilder.DropTable(
                name: "OpeningHours");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SpeciesHabitats");

            migrationBuilder.DropTable(
                name: "VetVisits");

            migrationBuilder.DropTable(
                name: "ZooServices");

            migrationBuilder.DropTable(
                name: "Habitats");

            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Healths");

            migrationBuilder.DropTable(
                name: "Speciess");

            migrationBuilder.DropTable(
                name: "Diets");

            migrationBuilder.DropTable(
                name: "SizeUnits");

            migrationBuilder.DropTable(
                name: "WeightUnits");
        }
    }
}
