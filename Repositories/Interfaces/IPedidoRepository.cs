using LojaAthena.Models;

namespace LojaAthena.Repositories.Interfaces;

public interface IPedidoRepository
{
    public void CriarPedido(PedidoModel pedido);
}
