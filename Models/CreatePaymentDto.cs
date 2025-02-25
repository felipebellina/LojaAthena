namespace LojaAthena.Models;

public class CreatePaymentDto
{
    public string? Description { get; set; }
    public int Installments { get; set; }
    public string? Token { get; set; }
    public PayerDto? Payer { get; set; }
    public string? PaymentMethodId { get; set; }
    public decimal TransactionAmount { get; set; }
}
