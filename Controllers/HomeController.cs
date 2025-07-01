using LojaAthena.Models;
using LojaAthena.Repositories.Interfaces;
using LojaAthena.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LojaAthena.Controllers;
public class HomeController : Controller
{
    private readonly IRoupaRepository _roupaRepository;

    public HomeController(IRoupaRepository roupaRepository)
    {
        _roupaRepository = roupaRepository;
    }

    public IActionResult Index()
    {
        var homeViewModel = new HomeViewModel
        {
            RoupasPreferidas = _roupaRepository.IsRoupaPreferida
        };

        return View(homeViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
