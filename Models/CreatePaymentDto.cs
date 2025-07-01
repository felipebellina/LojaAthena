using Newtonsoft.Json;

namespace LojaAthena.Models;

public class CreatePaymentRequestDto
{   
    
    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("installments")]
    public int Installments { get; set; }

    [JsonProperty("token")]
    public string? Token { get; set; }

    [JsonProperty("payer")]
    public PayerDto? Payer { get; set; }

    [JsonProperty("payment_method_id")]
    public string? PaymentMethodId { get; set; }

    [JsonProperty("transaction_amount")]
    public decimal TransactionAmount { get; set; }

    [JsonProperty("issuer_id")]
    public string? IssuerID { get; set; }


}
