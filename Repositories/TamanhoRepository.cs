using LojaAthena.Data;
using LojaAthena.Models;
using LojaAthena.Repositories.Interfaces;

namespace LojaAthena.Repositories;

public class TamanhoRepository : ITamanhoRepository
{
    private readonly BancoContext _bancoContext;

    public TamanhoRepository(BancoContext bancoContext)
    {
        _bancoContext = bancoContext;
    }

    public IEnumerable<TamanhoModel> Tamanhos => _bancoContext.Tamanhos;

}
