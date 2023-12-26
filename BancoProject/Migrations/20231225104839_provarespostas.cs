using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BancoProject.Migrations
{
    /// <inheritdoc />
    public partial class provarespostas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProvaQuestaoResposta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvaId = table.Column<int>(type: "int", nullable: true),
                    EstudanteId = table.Column<int>(type: "int", nullable: true),
                    DataRespondida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpcaoSelecionadaId = table.Column<int>(type: "int", nullable: true),
                    QuestaoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvaQuestaoResposta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProvaQuestaoResposta_Estudante_EstudanteId",
                        column: x => x.EstudanteId,
                        principalTable: "Estudante",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProvaQuestaoResposta_Prova_ProvaId",
                        column: x => x.ProvaId,
                        principalTable: "Prova",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProvaQuestaoResposta_QuestaoOpcao_OpcaoSelecionadaId",
                        column: x => x.OpcaoSelecionadaId,
                        principalTable: "QuestaoOpcao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProvaQuestaoResposta_Questao_QuestaoId",
                        column: x => x.QuestaoId,
                        principalTable: "Questao",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProvaQuestaoResposta_EstudanteId",
                table: "ProvaQuestaoResposta",
                column: "EstudanteId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvaQuestaoResposta_OpcaoSelecionadaId",
                table: "ProvaQuestaoResposta",
                column: "OpcaoSelecionadaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvaQuestaoResposta_ProvaId",
                table: "ProvaQuestaoResposta",
                column: "ProvaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvaQuestaoResposta_QuestaoId",
                table: "ProvaQuestaoResposta",
                column: "QuestaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProvaQuestaoResposta");
        }
    }
}
