using LojaAthena.Models;

namespace LojaAthena.Repositories.Interfaces;

public interface IMercadoPagoRepository
{
    PaymentResponseDto CreatePayment(CreatePaymentRequestDto paymentDto);
}
