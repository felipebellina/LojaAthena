using Microsoft.AspNetCore.Mvc;

namespace LojaAthena.Controllers;
public class ContatoController : Controller
{
    public IActionResult Index()
    {
       return View();
    }
}
