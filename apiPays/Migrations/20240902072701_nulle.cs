using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPays.Migrations
{
    /// <inheritdoc />
    public partial class Nulle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ville_code_postal_nom_ville",
                table: "ville");

            migrationBuilder.AddColumn<bool>(
                name: "estConducteur",
                table: "PersonneTrajets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ville_code_postal_nom_ville_PaysId",
                table: "ville",
                columns: new[] { "code_postal", "nom_ville", "PaysId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ville_code_postal_nom_ville_PaysId",
                table: "ville");

            migrationBuilder.DropColumn(
                name: "estConducteur",
                table: "PersonneTrajets");

            migrationBuilder.CreateIndex(
                name: "IX_ville_code_postal_nom_ville",
                table: "ville",
                columns: new[] { "code_postal", "nom_ville" },
                unique: true);
        }
    }
}
