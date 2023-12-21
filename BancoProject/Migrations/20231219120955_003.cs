using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BancoProject.Migrations
{
    /// <inheritdoc />
    public partial class _003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescricaoOpcao1",
                table: "Questao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescricaoOpcao2",
                table: "Questao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescricaoOpcao3",
                table: "Questao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescricaoOpcao4",
                table: "Questao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescricaoOpcao5",
                table: "Questao",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescricaoOpcao1",
                table: "Questao");

            migrationBuilder.DropColumn(
                name: "DescricaoOpcao2",
                table: "Questao");

            migrationBuilder.DropColumn(
                name: "DescricaoOpcao3",
                table: "Questao");

            migrationBuilder.DropColumn(
                name: "DescricaoOpcao4",
                table: "Questao");

            migrationBuilder.DropColumn(
                name: "DescricaoOpcao5",
                table: "Questao");
        }
    }
}
