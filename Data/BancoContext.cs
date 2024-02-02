using LojaAthena.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaAthena.Data;

public class BancoContext : DbContext
{
    public BancoContext(DbContextOptions<BancoContext> options) : base(options)
    {
    }

    public DbSet<CategoriaModel> Categorias { get; set; }
    public DbSet<RoupaModel> Roupas { get; set; }
    public DbSet<CarrinhoCompraItemModel> CarrinhoCompraItens { get; set; }
    public DbSet<PedidoModel> Pedidos { get; set; }
    public DbSet<PedidoDetalheModel> PedidosDetalhe { get; set; }
}
