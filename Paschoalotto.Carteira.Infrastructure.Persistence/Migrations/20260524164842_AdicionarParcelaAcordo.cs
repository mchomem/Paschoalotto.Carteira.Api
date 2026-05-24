using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Paschoalotto.Carteira.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarParcelaAcordo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParcelaAcordoId",
                table: "Boletos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ParcelasAcordo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AcordoId = table.Column<int>(type: "integer", nullable: false),
                    NumeroParcela = table.Column<int>(type: "integer", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ValorPago = table.Column<decimal>(type: "numeric", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelasAcordo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParcelasAcordo_Acordos_AcordoId",
                        column: x => x.AcordoId,
                        principalTable: "Acordos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boletos_ParcelaAcordoId",
                table: "Boletos",
                column: "ParcelaAcordoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParcelasAcordo_AcordoId",
                table: "ParcelasAcordo",
                column: "AcordoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boletos_ParcelasAcordo_ParcelaAcordoId",
                table: "Boletos",
                column: "ParcelaAcordoId",
                principalTable: "ParcelasAcordo",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boletos_ParcelasAcordo_ParcelaAcordoId",
                table: "Boletos");

            migrationBuilder.DropTable(
                name: "ParcelasAcordo");

            migrationBuilder.DropIndex(
                name: "IX_Boletos_ParcelaAcordoId",
                table: "Boletos");

            migrationBuilder.DropColumn(
                name: "ParcelaAcordoId",
                table: "Boletos");
        }
    }
}
