using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaAthena.Migrations
{
    /// <inheritdoc />
    public partial class PopularRoupas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Roupas(CategoriaId,Descricao,Estoque,RoupaPreferida,Nome,Preco) VALUES(1,'Vestido azul',1, 0,'Vestido azul', 12.50)");
            migrationBuilder.Sql("INSERT INTO Roupas(CategoriaId,Descricao,Estoque,RoupaPreferida,Nome,Preco) VALUES(1,'Vestido rosa',1, 0,'Vestido rosa', 8.00)");
            migrationBuilder.Sql("INSERT INTO Roupas(CategoriaId,Descricao,Estoque,RoupaPreferida,Nome,Preco) VALUES(1,'Vestido roxo',1, 0,'Vestido roxo', 11.00)");
            migrationBuilder.Sql("INSERT INTO Roupas(CategoriaId,Descricao,Estoque,RoupaPreferida,Nome,Preco) VALUES(2,'Calça jeans',1, 1,'Calça jeans', 15.00)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Roupas");
        }
    }
}
