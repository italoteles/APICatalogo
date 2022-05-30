using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace APICatalogo.Models;
public class Produto : IValidatableObject
{
    [Key]
    public int ProdutoId { get; set; }
    [Required]
    [StringLength(80,ErrorMessage = "Nome deve ter entre 2 e 80 caracteres",MinimumLength = 2)]
    [PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }
    [Required]
    [StringLength(300)]
    public string? Descricao { get; set; }
    [Required]
    [Column(TypeName="decimal(10,2)")]
    public decimal Preco { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImagemURL { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }
    //JsonIgnore faz com que propriedades de navegação não sejam exibidas na serialiação do json no POST e PUT
    [JsonIgnore]
    public Categoria? Categoria { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(this.Descricao))
        {
            var primeiraLetra = this.Descricao.ToString()[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                yield return new ValidationResult("A primeira letra da descrição do produto deve ser maiúscula",
                    new[] 
                    {
                        nameof(this.Descricao)
                    });
            }

            if(this.Estoque <= 0)
            {
                yield return new ValidationResult("O estoque deve ser maior que 0",
                    new[]
                    {
                        nameof(this.Estoque)
                    });
            }
        }
    }
}

