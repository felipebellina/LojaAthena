using LojaAthena.Models;
using LojaAthena.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LojaAthena.Controllers;

public class PagamentoController : Controller
{
    private readonly IMercadoPagoRepository _mercadoPagoRepository;

    public PagamentoController(IMercadoPagoRepository mercadoPagoRepository)
    {
        _mercadoPagoRepository = mercadoPagoRepository;
    }


    [HttpPost]
    public IActionResult CreatePayment([FromBody]CreatePaymentRequestDto paymentDto)
    {
        if (paymentDto == null)
        {
            return BadRequest("Dados de pagamento não fornecidos.");
        }

        var response = _mercadoPagoRepository.CreatePayment(paymentDto);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Success()
    {
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }
}
