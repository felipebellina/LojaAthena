using LojaAthena.Models;
using LojaAthena.Repositories.Interfaces;
using LojaAthena.ViewsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaAthena.Controllers;
public class PedidoController : Controller
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly CarrinhoCompraModel _carrinhoCompra;

    public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompraModel carrinhoCompra)
    {
        _pedidoRepository = pedidoRepository;
        _carrinhoCompra = carrinhoCompra;
    }

    [Authorize]
    public IActionResult Checkout()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public IActionResult Checkout(PedidoModel pedido)
    {
        int totalItensPedido = 0;
        decimal precoTotalPedido = 0.0m;

        //obtem os itens do carrinho de compra do cliente
        List<CarrinhoCompraItemModel> itens = _carrinhoCompra.GetCarrinhoCompraItens();
        _carrinhoCompra.CarrinhoCompraItens = itens;

        //verifica se existem itens de pedido
        if (_carrinhoCompra.CarrinhoCompraItens.Count == 0)
        {
            ModelState.AddModelError("", "Seu carrinho está vazio, que tal incluir algum item...");
        }

        //calcular total dos itens e total do pedido
        foreach (var item in itens)
        {
            totalItensPedido += item.Quantidade;
            precoTotalPedido += (item.Roupa.Preco * item.Quantidade);
        }

        //atribuir os valores obtidos ao pedido
        pedido.TotalItensPedido = totalItensPedido;
        pedido.PedidoTotal = precoTotalPedido;

        //validar dados do pedido
        if (ModelState.IsValid)
        {
            //criar pedido e os detalhes
            _pedidoRepository.CriarPedido(pedido);

            //define mensagem ao cliente
            ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
            ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();
            ViewBag.TransactionAmount = _carrinhoCompra.GetCarrinhoCompraTotal();

            //limpar carrinho do cliente
            _carrinhoCompra.LimparCarrinho();



            //exibe a view com dados do cliente e do pedido
            return View("~/Views/Pagamento/Index.cshtml");
        }

        return View(pedido);

    }

}
