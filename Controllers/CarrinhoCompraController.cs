using LojaAthena.Models;
using LojaAthena.Repositories.Interfaces;
using LojaAthena.ViewsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaAthena.Controllers;
public class CarrinhoCompraController : Controller
{
    private readonly IRoupaRepository _roupaRepository;
    private readonly CarrinhoCompraModel _carrinhoCompra;

    public CarrinhoCompraController(IRoupaRepository roupaRepository, CarrinhoCompraModel carrinhoCompra)
    {
        _roupaRepository = roupaRepository;
        _carrinhoCompra = carrinhoCompra;
    }

    public IActionResult Index()
    {
        var itens = _carrinhoCompra.GetCarrinhoCompraItens();
        _carrinhoCompra.CarrinhoCompraItens = itens;

        var carrinhoCompraVM = new CarrinhoCompraViewModel
        {
            CarrinhoCompra = _carrinhoCompra,
            CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
        };

        return View(carrinhoCompraVM);
    }

    [Authorize]
    public IActionResult AdicionarNoCarrinho(int id)
    {
        var roupaSelecionada = _roupaRepository.Roupas.FirstOrDefault(x => x.Id == id);
        if (roupaSelecionada != null)
        {
            _carrinhoCompra.AdicionarAoCarrinho(roupaSelecionada);
            
        }
        return RedirectToAction("Index");
    }

    [Authorize]
    public IActionResult RemoverDoCarrinho(int id)
    {
        var roupaSelecionada = _roupaRepository.Roupas.FirstOrDefault(x => x.Id == id);
        if (roupaSelecionada != null)
        {
            _carrinhoCompra.RemoverDoCarrinho(roupaSelecionada);
        }
        return RedirectToAction("Index");
    }
}
