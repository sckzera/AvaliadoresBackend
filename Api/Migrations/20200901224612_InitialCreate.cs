using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avaliacoes",
                columns: table => new
                {
                    Codigo = table.Column<Guid>(nullable: false),
                    CodigoAvaliador = table.Column<Guid>(nullable: false),
                    CodigoResumo = table.Column<Guid>(nullable: false),
                    Relevancia = table.Column<float>(nullable: false),
                    Adequacao = table.Column<float>(nullable: false),
                    Coerencia = table.Column<float>(nullable: false),
                    ApresentacaoAdequada = table.Column<float>(nullable: false),
                    AvaliacaoResumo = table.Column<float>(nullable: false),
                    Qualidade = table.Column<float>(nullable: false),
                    ApresentacaoOral = table.Column<float>(nullable: false),
                    ContribuicaoDesenvolvimento = table.Column<float>(nullable: false),
                    ContribuicaoComunidade = table.Column<float>(nullable: false),
                    AvaliacaoGeral = table.Column<float>(nullable: false),
                    SomaDasNotas = table.Column<float>(nullable: false),
                    PremioTeixeira = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacoes", x => x.Codigo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacoes");
        }
    }
}
