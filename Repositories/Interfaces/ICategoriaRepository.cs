using LojaAthena.Models;

namespace LojaAthena.Repositories.Interfaces;

public interface ICategoriaRepository
{
    IEnumerable<CategoriaModel> Categorias { get; }
}
