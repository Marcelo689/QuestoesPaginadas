using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BancoProject.Migrations
{
    /// <inheritdoc />
    public partial class provaestudante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstudanteId",
                table: "Prova",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prova_EstudanteId",
                table: "Prova",
                column: "EstudanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prova_Estudante_EstudanteId",
                table: "Prova",
                column: "EstudanteId",
                principalTable: "Estudante",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prova_Estudante_EstudanteId",
                table: "Prova");

            migrationBuilder.DropIndex(
                name: "IX_Prova_EstudanteId",
                table: "Prova");

            migrationBuilder.DropColumn(
                name: "EstudanteId",
                table: "Prova");
        }
    }
}
