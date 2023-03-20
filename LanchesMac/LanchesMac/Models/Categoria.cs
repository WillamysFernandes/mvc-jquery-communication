﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        [StringLength(100, ErrorMessage ="O tamanho máximo é de 100 caracteres")]
        [Required(ErrorMessage ="Informe o nome da Categoria")]
        [Display(Name ="Nome da Categoria")]
        public string CategoriaNome { get; set; }
        [StringLength(255, ErrorMessage = "O tamanho máximo é de 255 caracteres")]
        [Required(ErrorMessage = "Informe a Descrição da Categoria")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public  List<Lanche> Lanches { get; set; }//definindo o relacionamento de 1 para N

    }
}
