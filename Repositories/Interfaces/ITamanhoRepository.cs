using LojaAthena.Models;

namespace LojaAthena.Repositories.Interfaces;

public interface ITamanhoRepository
{
    IEnumerable<TamanhoModel> Tamanhos {  get; }

}
