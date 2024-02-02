using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaAthena.Migrations
{
    /// <inheritdoc />
    public partial class PopularCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias(Nome, Descricao) " +
                "VALUES('Normal','Roupa normal')");
            migrationBuilder.Sql("INSERT INTO Categorias(Nome, Descricao) " +
                "VALUES('Algodão','Roupa de algodão')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}
