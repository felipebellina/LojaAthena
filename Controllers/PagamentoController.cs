using LojaAthena.Models;
using LojaAthena.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaAthena.Controllers;

public class PagamentoController : Controller
{
    private readonly IMercadoPagoRepository _mercadoPagoRepository;

    public PagamentoController(IMercadoPagoRepository mercadoPagoRepository)
    {
        _mercadoPagoRepository = mercadoPagoRepository;
    }

    [HttpPost]
    public IActionResult CreatePayment(CreatePaymentRequestDto paymentDto)
    {
        if (paymentDto == null)
        {
            return BadRequest("Dados de pagamento não fornecidos.");
        }

        var response = _mercadoPagoRepository.CreatePayment(paymentDto);

        if (response.Status == "approved")
        {
            return View("Success");
        }

        return View("Failure");
        
    }

    public IActionResult Index()
    {
        return View();
    }
}
