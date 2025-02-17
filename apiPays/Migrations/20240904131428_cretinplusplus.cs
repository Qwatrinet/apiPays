using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPays.Migrations
{
    /// <inheritdoc />
    public partial class Cretinplusplus : Migration
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
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateArrivee",
                table: "Trajets",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_Trajets_ville_VilleArriveeId",
                table: "Trajets",
                column: "VilleArriveeId",
                principalTable: "ville",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateArrivee",
                table: "Trajets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trajets_ville_VilleArriveeId",
                table: "Trajets",
                column: "VilleArriveeId",
                principalTable: "ville",
                principalColumn: "Id");
        }
    }
}
