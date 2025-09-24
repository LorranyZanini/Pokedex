using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models;

[Table("Pokemon")]
public class Pokemon
{
    [Key]
    [Display(Name = "Número")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public uint Numero { get; set; }

    [Required(ErrorMessage ="Por favor, informe a região")]
    public uint RegiaoId { get; set; }
    [ForeignKey("RegiaoId")]
    [Display(Name = "Região")]
    public Regiao Regiao { get; set; }

    [Required(ErrorMessage ="Por favor, informe o genero")]

    public uint GeneroId { get; set; }
    [ForeignKey("GeneroId")]
    public Genero Genero { get; set; }

    [StringLength(30)]
    [Required(ErrorMessage = "Por favor, informe o nome")]
    public string Nome { get; set; }

    [StringLength(1000)]
    public string Descricao { get; set; }
    [Display(Name = "Descrição")]

    [Column(TypeName = "double(5,2)")]
    [Required(ErrorMessage ="Por favor, informe a altura")]
    public double Altura { get; set; }

    [Column(TypeName = "double(7,3)")]
    [Required(ErrorMessage ="Por favor, informe o peso")]
    public double Peso { get; set; }

    [StringLength(200)]
    public string Imagem { get; set; }

    [StringLength(400)]
    public string Animacao { get; set; }

    public ICollection<PokemonTipo> Tipos { get; set; }
}