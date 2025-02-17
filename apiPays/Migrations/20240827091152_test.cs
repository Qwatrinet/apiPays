using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPays.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pays",
                columns: table => new
                {
                    PaysId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code_pays = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nom_pays = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pays", x => x.PaysId);
                });

            migrationBuilder.CreateTable(
                name: "Personnes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ville",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code_postal = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nom_ville = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PaysId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ville", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ville_pays_PaysId",
                        column: x => x.PaysId,
                        principalTable: "pays",
                        principalColumn: "PaysId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trajets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateDepart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateArrivee = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VilleDepartId = table.Column<int>(type: "int", maxLength: 60, nullable: false),
                    VilleArriveeId = table.Column<int>(type: "int", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trajets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trajets_ville_VilleArriveeId",
                        column: x => x.VilleArriveeId,
                        principalTable: "ville",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Trajets_ville_VilleDepartId",
                        column: x => x.VilleDepartId,
                        principalTable: "ville",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PersonneTrajets",
                columns: table => new
                {
                    PersonneId = table.Column<int>(type: "int", nullable: false),
                    TrajetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonneTrajets", x => new { x.PersonneId, x.TrajetId });
                    table.ForeignKey(
                        name: "FK_PersonneTrajets_Personnes_PersonneId",
                        column: x => x.PersonneId,
                        principalTable: "Personnes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonneTrajets_Trajets_TrajetId",
                        column: x => x.TrajetId,
                        principalTable: "Trajets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pays_code_pays",
                table: "pays",
                column: "code_pays",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pays_nom_pays",
                table: "pays",
                column: "nom_pays",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonneTrajets_TrajetId",
                table: "PersonneTrajets",
                column: "TrajetId");

            migrationBuilder.CreateIndex(
                name: "IX_Trajets_VilleArriveeId",
                table: "Trajets",
                column: "VilleArriveeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trajets_VilleDepartId",
                table: "Trajets",
                column: "VilleDepartId");

            migrationBuilder.CreateIndex(
                name: "IX_ville_code_postal_nom_ville",
                table: "ville",
                columns: new[] { "code_postal", "nom_ville" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ville_PaysId",
                table: "ville",
                column: "PaysId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonneTrajets");

            migrationBuilder.DropTable(
                name: "Personnes");

            migrationBuilder.DropTable(
                name: "Trajets");

            migrationBuilder.DropTable(
                name: "ville");

            migrationBuilder.DropTable(
                name: "pays");
        }
    }
}
