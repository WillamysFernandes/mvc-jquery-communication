using Dapper.Contrib.Extensions;

namespace LanchesMac.Models
{
	[Table("User")]
	public class User
	{
		[Key]
		public int Id { get; set; }
		public string Nome { get; set; }
		public int Idade { get; set; }
		public string DataNascimento { get; set; }
		public string GitHub { get; set; }
		public string Sexo { get; set; }
		public DateTime DataCriacao { get; set; }
		public DateTime DataAtualizacao { get; set; }
		public string Password { get; set; }
		public string Login { get; set; }


	}
}
