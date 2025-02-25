using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaAthena.Models;

[Table("Roupas")]
public class RoupaModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(80, MinimumLength = 5, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2}")]
    [Display(Name = "Nome da Roupa")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "A descrição da roupa deve ser informada")]
    [Display(Name = "Descrição da roupa")]
    [MinLength(5, ErrorMessage = "Descrição da roupa deve ter no mínimo {1} caracteres")]
    [MaxLength(200, ErrorMessage = "Descrição da roupa deve ter no máximo {1} caracteres")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "Informe o preço da roupa")]
    [Display(Name = "Preço")]
    [Column(TypeName ="decimal(10,2)")]
    [Range(1,999.99,ErrorMessage = "O preço deve ser entre R$ 1,00 e R$ 999,99")]
    public decimal Preco { get; set; }

    [Display(Name = "Caminho Imagem Normal")]
    [StringLength(200, ErrorMessage = "o {0} deve ter no máximo {1} caracteres")]
    public string? ImagemUrl { get; set; }

    [Display(Name = "Caminho Imagem Miniatura")]
    [StringLength(200, ErrorMessage = "o {0} deve ter no máximo {1} caracteres")]
    public string? ImagemThumbnailUrl { get; set; }

    [Display(Name = "Preferido?")]
    public bool RoupaPreferida { get; set; }

    [Display(Name = "Estoque")]
    public bool Estoque { get; set; }

    public int CategoriaId { get; set; }
    public CategoriaModel? Categoria { get; set; }

    public List<TamanhoModel>? Tamanhos { get; set; }

}
