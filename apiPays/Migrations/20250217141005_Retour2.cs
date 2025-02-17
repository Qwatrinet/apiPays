using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPays.Migrations
{
    /// <inheritdoc />
    public partial class Retour2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "code_pays",
                table: "pays",
                type: "char(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "code_pays",
                table: "pays",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(2)",
                oldMaxLength: 2);
        }
    }
}
