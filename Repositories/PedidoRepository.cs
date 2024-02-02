using LojaAthena.Data;
using LojaAthena.Models;
using LojaAthena.Repositories.Interfaces;

namespace LojaAthena.Repositories;

public class PedidoRepository : IPedidoRepository
{

    private readonly BancoContext _bancoContext;
    private readonly CarrinhoCompraModel _carrinhoCompra;

    public PedidoRepository(BancoContext bancoContext, CarrinhoCompraModel carrinhoCompra)
    {
        _bancoContext = bancoContext;
        _carrinhoCompra = carrinhoCompra;
    }

    public void CriarPedido(PedidoModel pedido)
    {
        pedido.PedidoEnviado = DateTime.Now;
        _bancoContext.Pedidos.Add(pedido);
        _bancoContext.SaveChanges();

        var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItens;

        foreach (var carrinhoItem in carrinhoCompraItens)
        {
            var pedidoDetail = new PedidoDetalheModel
            {
                Quantidade = carrinhoItem.Quantidade,
                RoupaId = carrinhoItem.Roupa.Id,
                PedidoId = pedido.Id,
                Preco = carrinhoItem.Roupa.Preco
            };

            _bancoContext.PedidosDetalhe.Add(pedidoDetail);

        }

        _bancoContext.SaveChanges();
    }
}
