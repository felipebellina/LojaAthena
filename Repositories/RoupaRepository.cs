using LojaAthena.Data;
using LojaAthena.Models;
using LojaAthena.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojaAthena.Repositories;

public class RoupaRepository : IRoupaRepository
{
    private readonly BancoContext _bancoContext;

    public RoupaRepository(BancoContext bancoContext)
    {
        _bancoContext = bancoContext;
    }

    public IEnumerable<RoupaModel> Roupas => _bancoContext.Roupas.Include(x => x.Categoria);

    public IEnumerable<RoupaModel> IsRoupaPreferida => _bancoContext.Roupas.Where(x => x.RoupaPreferida).Include(x => x.Categoria);

    public RoupaModel GetRoupaById(int roupaId)
    {
        return _bancoContext.Roupas.FirstOrDefault(x => x.Id == roupaId);
    }
}
