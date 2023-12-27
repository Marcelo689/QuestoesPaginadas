using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BancoProject.Migrations
{
    /// <inheritdoc />
    public partial class questaoOpcao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvaQuestaoResposta_QuestaoOpcao_OpcaoSelecionadaId",
                table: "ProvaQuestaoResposta");

            migrationBuilder.RenameColumn(
                name: "OpcaoSelecionadaId",
                table: "ProvaQuestaoResposta",
                newName: "QuestaoOpcaoId");

            migrationBuilder.RenameIndex(
                name: "IX_ProvaQuestaoResposta_OpcaoSelecionadaId",
                table: "ProvaQuestaoResposta",
                newName: "IX_ProvaQuestaoResposta_QuestaoOpcaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvaQuestaoResposta_QuestaoOpcao_QuestaoOpcaoId",
                table: "ProvaQuestaoResposta",
                column: "QuestaoOpcaoId",
                principalTable: "QuestaoOpcao",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvaQuestaoResposta_QuestaoOpcao_QuestaoOpcaoId",
                table: "ProvaQuestaoResposta");

            migrationBuilder.RenameColumn(
                name: "QuestaoOpcaoId",
                table: "ProvaQuestaoResposta",
                newName: "OpcaoSelecionadaId");

            migrationBuilder.RenameIndex(
                name: "IX_ProvaQuestaoResposta_QuestaoOpcaoId",
                table: "ProvaQuestaoResposta",
                newName: "IX_ProvaQuestaoResposta_OpcaoSelecionadaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvaQuestaoResposta_QuestaoOpcao_OpcaoSelecionadaId",
                table: "ProvaQuestaoResposta",
                column: "OpcaoSelecionadaId",
                principalTable: "QuestaoOpcao",
                principalColumn: "Id");
        }
    }
}
