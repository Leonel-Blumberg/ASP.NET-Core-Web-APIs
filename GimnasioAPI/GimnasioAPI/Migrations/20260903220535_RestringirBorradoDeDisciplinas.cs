using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GimnasioAPI.Migrations
{
    /// <inheritdoc />
    public partial class RestringirBorradoDeDisciplinas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clases_Disciplinas_DisciplinaId",
                table: "Clases");

            migrationBuilder.AddForeignKey(
                name: "FK_Clases_Disciplinas_DisciplinaId",
                table: "Clases",
                column: "DisciplinaId",
                principalTable: "Disciplinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clases_Disciplinas_DisciplinaId",
                table: "Clases");

            migrationBuilder.AddForeignKey(
                name: "FK_Clases_Disciplinas_DisciplinaId",
                table: "Clases",
                column: "DisciplinaId",
                principalTable: "Disciplinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
