using LojaAthena.Models;

namespace LojaAthena.ViewsModels;

public class RoupaListViewModel
{
    public IEnumerable<RoupaModel>? Roupas { get; set; }
    
    public string? CategoriaAtual { get; set; }

    public IEnumerable<CategoriaModel>? Categorias { get; set; }
}
