using Dapper.Contrib.Extensions;

namespace LanchesMac.Models
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
    }
}
