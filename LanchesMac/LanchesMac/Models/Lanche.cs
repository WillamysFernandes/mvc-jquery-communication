using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Lanche")]
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }
        [Required(ErrorMessage = "Infome o nome do Lanche")]
        [Display(Name ="Nome do Lanche")]
        [MinLength(10, ErrorMessage ="Nome deve conter mínimo de 10 caracteres")]
        [MaxLength(100, ErrorMessage ="Nome deve conter máximo de 100 caracteres")]
        public string NomeLanche { get; set; }
        [Required(ErrorMessage = "Informe a descrição curta do lanche")]
        [Display(Name = "Descrição Curta")]
        [MinLength(10, ErrorMessage = "Descrição deve conter mínimo de 10 caracteres")]
        [MaxLength(80, ErrorMessage = "Descrição deve conter máximo de 80 caracteres")]
        public string DescricaoCurta { get; set; }
        [Required(ErrorMessage = "Infome os detalhes do lanche")]
        [Display(Name = "Descrição Detalhada")]
        [MinLength(10, ErrorMessage = "Descrição deve conter mínimo de 10 caracteres")]
        [MaxLength(255, ErrorMessage = "descição deve conter máximo de 255 caracteres")]
        public string DescricaoDetalhada { get; set;}
        [Required(ErrorMessage ="Infome o preço")]
        [Display(Name ="Preço")]
        [Column(TypeName ="decimal(10,2)")]
        public decimal Preco { get; set; }
        [Display(Name = "Caminho da Imagem Normal")]
        public string ImagemThumbaiUrl { get; set;}
        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }
        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }
        
        public int CategoriaId { get; set; } //definindo o relacionamento entre as tabelas Lanche e Categoria
        public virtual Categoria Categoria { get; set; } 

    }
}
