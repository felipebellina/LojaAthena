using Newtonsoft.Json;

namespace LojaAthena.Models;

public class PaymentResponseDto
{
    public string? Id { get; set; }
    public string? Status{ get; set; }
    public string? Detail { get; set; }
}
