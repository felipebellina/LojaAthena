using LojaAthena.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace LojaAthena.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminRelatoriosVendasController : Controller
{
    private readonly RelatorioVendasService _relatorioVendasService;

    public AdminRelatoriosVendasController(RelatorioVendasService relatorioVendasService)
    {
        _relatorioVendasService = relatorioVendasService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> RelatorioVendasSimples(DateTime? minDate, DateTime? maxDate)
    {
        if(!minDate.HasValue)
        {
            minDate = new DateTime(DateTime.Now.Year, 1, 1);
        }
        if(!maxDate.HasValue)
        {
            maxDate = DateTime.Now;
        }

        ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
        ViewData["maxDate"] = minDate.Value.ToString("yyyy-MM-dd");

        var result = await _relatorioVendasService.FindByDateAsync(minDate, maxDate);
        return View(result);
    }

}
