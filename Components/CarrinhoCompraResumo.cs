using LojaAthena.Models;
using LojaAthena.ViewsModels;
using Microsoft.AspNetCore.Mvc;

namespace LojaAthena.Components;

public class CarrinhoCompraResumo : ViewComponent
{
    private readonly CarrinhoCompraModel _carrinhoCompra;

    public CarrinhoCompraResumo(CarrinhoCompraModel carrinhoCompra)
    {
        _carrinhoCompra = carrinhoCompra;
    }

    public IViewComponentResult Invoke()
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
}
