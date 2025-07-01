using System.ComponentModel.DataAnnotations;

namespace LojaAthena.Models;

public class CarrinhoCompraItemModel
{
    public int Id { get; set; }
    public RoupaModel? Roupa { get; set; }
    public int Quantidade { get; set; }

    [StringLength(200)]
    public string? CarrinhoCompraId { get; set; }

}
