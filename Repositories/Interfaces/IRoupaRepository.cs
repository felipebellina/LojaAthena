using LojaAthena.Models;

namespace LojaAthena.Repositories.Interfaces;

public interface IRoupaRepository
{
    IEnumerable<RoupaModel> Roupas { get; }
    IEnumerable<RoupaModel> IsRoupaPreferida { get;}
    RoupaModel GetRoupaById(int roupaId);
}
