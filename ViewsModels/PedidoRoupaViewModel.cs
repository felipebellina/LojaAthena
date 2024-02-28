using LojaAthena.Models;

namespace LojaAthena.ViewsModels;

public class PedidoRoupaViewModel
{
    public PedidoModel? Pedido { get; set; }

    public IEnumerable<PedidoDetalheModel>? PedidoDetalhes { get; set; }
}
