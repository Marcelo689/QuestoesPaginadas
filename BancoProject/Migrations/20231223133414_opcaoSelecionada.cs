using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BancoProject.Migrations
{
    /// <inheritdoc />
    public partial class opcaoSelecionada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OpcaoSelecionadaId",
                table: "Questao",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questao_OpcaoSelecionadaId",
                table: "Questao",
                column: "OpcaoSelecionadaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questao_QuestaoOpcao_OpcaoSelecionadaId",
                table: "Questao",
                column: "OpcaoSelecionadaId",
                principalTable: "QuestaoOpcao",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questao_QuestaoOpcao_OpcaoSelecionadaId",
                table: "Questao");

            migrationBuilder.DropIndex(
                name: "IX_Questao_OpcaoSelecionadaId",
                table: "Questao");

            migrationBuilder.DropColumn(
                name: "OpcaoSelecionadaId",
                table: "Questao");
        }
    }
}
