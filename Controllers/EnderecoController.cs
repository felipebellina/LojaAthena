using LojaAthena.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaAthena.Controllers;
public class EnderecoController : Controller
{
    private readonly IEnderecoRepository _enderecoRepository;

    public EnderecoController(IEnderecoRepository enderecoRepository)
    {
        _enderecoRepository = enderecoRepository;
    }

    public JsonResult GetEndereco(string cep)
    {
        var endereco = _enderecoRepository.GetEndereco(cep);
        return Json(endereco);
    }

    public IActionResult Index()
    {
        return View();
    }

}
