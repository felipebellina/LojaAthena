using System.ComponentModel.DataAnnotations;

namespace LojaAthena.Models;

public class TamanhoModel
{
    public int Id { get; set; }

    public string? Tamanho { get; set; }

    public int Quantidade { get; set; }

    public int RoupaId { get; set; }

    public RoupaModel? Roupa { get; set; }
}
