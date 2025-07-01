using LojaAthena.Data;
using LojaAthena.Models;
using LojaAthena.Repositories.Interfaces;

namespace LojaAthena.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly BancoContext _bancoContext;

    public CategoriaRepository(BancoContext bancoContext)
    {
        _bancoContext = bancoContext;
    }

    public IEnumerable<CategoriaModel> Categorias => _bancoContext.Categorias;

}
