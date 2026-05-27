using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paschoalotto.Carteira.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_TipoCredito_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoCredito",
                table: "Contratos",
                type: "integer",
                nullable: false,
                defaultValueSql: "99");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoCredito",
                table: "Contratos");
        }
    }
}
