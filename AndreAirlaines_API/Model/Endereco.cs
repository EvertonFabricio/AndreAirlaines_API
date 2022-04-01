using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndreAirlaines_API.Model
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }
        [Column("Logradouro", TypeName = "varchar(150)")]
        public string Logradouro { get; set; }

        public int Numero { get; set; }
        [Column("Bairro", TypeName = "varchar(50)")]
        public string Bairro { get; set; }
        [Column("Cidade", TypeName = "varchar(30)")]
        public string Cidade { get; set; }
        [Column("Uf", TypeName = "varchar(2)")]
        public string Uf { get; set; }
        [Column("Pais", TypeName = "varchar(20)")]
        public string Pais { get; set; }
        [Column("Cep", TypeName = "varchar(10)")]
        public string Cep { get; set; }
    }
}
