using Newtonsoft.Json;

namespace LojaAthena.Models;

public class PayerDto
{
    [JsonProperty("email")]
    public string? Email { get; set; }
}
