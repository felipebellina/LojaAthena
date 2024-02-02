using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaAthena.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoNomeColuna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quatidade",
                table: "CarrinhoCompraItens",
                newName: "Quantidade");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantidade",
                table: "CarrinhoCompraItens",
                newName: "Quatidade");
        }
    }
}
