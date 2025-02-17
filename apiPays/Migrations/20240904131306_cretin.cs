using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPays.Migrations
{
    /// <inheritdoc />
    public partial class Cretin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trajets_ville_VilleArriveeId",
                table: "Trajets");

            migrationBuilder.AlterColumn<int>(
                name: "VilleArriveeId",
                table: "Trajets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Trajets_ville_VilleArriveeId",
                table: "Trajets",
                column: "VilleArriveeId",
                principalTable: "ville",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trajets_ville_VilleArriveeId",
                table: "Trajets");

            migrationBuilder.AlterColumn<int>(
                name: "VilleArriveeId",
                table: "Trajets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trajets_ville_VilleArriveeId",
                table: "Trajets",
                column: "VilleArriveeId",
                principalTable: "ville",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
