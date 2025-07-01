using LojaAthena.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LojaAthena.Data;

public class BancoContext : IdentityDbContext<IdentityUser>
{
    public BancoContext(DbContextOptions<BancoContext> options) : base(options)
    {
    }

    public DbSet<CategoriaModel> Categorias { get; set; }
    public DbSet<RoupaModel> Roupas { get; set; }
    public DbSet<CarrinhoCompraItemModel> CarrinhoCompraItens { get; set; }
    public DbSet<PedidoModel> Pedidos { get; set; }
    public DbSet<PedidoDetalheModel> PedidosDetalhe { get; set; }
    public DbSet<TamanhoModel> Tamanhos { get; set; }
}
