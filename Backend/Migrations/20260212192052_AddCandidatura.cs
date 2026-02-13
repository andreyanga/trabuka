using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabukaApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCandidatura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pontuacao",
                table: "Testes",
                newName: "TempoLimiteMinutos");

            migrationBuilder.RenameColumn(
                name: "DataRealizacao",
                table: "Testes",
                newName: "DataCriacao");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Testes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Testes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PontuacaoMaxima",
                table: "Testes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PontuacaoMinima",
                table: "Testes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Candidaturas",
                columns: table => new
                {
                    CandidaturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ProjetoId = table.Column<int>(type: "int", nullable: false),
                    DataCandidatura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidaturas", x => x.CandidaturaId);
                    table.ForeignKey(
                        name: "FK_Candidaturas_Projetos_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projetos",
                        principalColumn: "ProjetoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidaturas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questoes",
                columns: table => new
                {
                    QuestaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TesteId = table.Column<int>(type: "int", nullable: false),
                    Enunciado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpcaoA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpcaoB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpcaoC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpcaoD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RespostaCorreta = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Pontuacao = table.Column<int>(type: "int", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questoes", x => x.QuestaoId);
                    table.ForeignKey(
                        name: "FK_Questoes_Testes_TesteId",
                        column: x => x.TesteId,
                        principalTable: "Testes",
                        principalColumn: "TesteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidaturas_ProjetoId",
                table: "Candidaturas",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidaturas_UsuarioId",
                table: "Candidaturas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Questoes_TesteId",
                table: "Questoes",
                column: "TesteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidaturas");

            migrationBuilder.DropTable(
                name: "Questoes");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Testes");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Testes");

            migrationBuilder.DropColumn(
                name: "PontuacaoMaxima",
                table: "Testes");

            migrationBuilder.DropColumn(
                name: "PontuacaoMinima",
                table: "Testes");

            migrationBuilder.RenameColumn(
                name: "TempoLimiteMinutos",
                table: "Testes",
                newName: "Pontuacao");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Testes",
                newName: "DataRealizacao");
        }
    }
}
