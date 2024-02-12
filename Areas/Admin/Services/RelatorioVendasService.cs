using LojaAthena.Data;
using LojaAthena.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace LojaAthena.Areas.Admin.Services;

public class RelatorioVendasService
{
    private readonly BancoContext _bancoContext;

    public RelatorioVendasService(BancoContext bancoContext)
    {
        _bancoContext = bancoContext;
    }

    public async Task<List<PedidoModel>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
    {
        var result = from obj in _bancoContext.Pedidos select obj;

        if (minDate.HasValue)
        {
            result = result.Where(x => x.PedidoEnviado >= minDate.Value);
        }

        if (maxDate.HasValue)
        {
            result = result.Where(x => x.PedidoEnviado <= maxDate.Value);
        }

        return await result.Include(r => r.PedidoItens).ThenInclude(r => r.Roupa).OrderByDescending(x=>x.PedidoEnviado).ToListAsync();

    }
}
