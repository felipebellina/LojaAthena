using System.ComponentModel.DataAnnotations.Schema;

namespace LojaAthena.Models;

public class PedidoDetalheModel
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public int RoupaId { get; set; }
    public int Quantidade { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Preco { get; set; }

    public virtual RoupaModel? Roupa {  get; set; }
    public virtual PedidoModel? Pedido { get; set; }
}
