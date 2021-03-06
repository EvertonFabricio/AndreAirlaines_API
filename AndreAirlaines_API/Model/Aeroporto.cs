using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndreAirlaines_API.Model
{
    public class Aeroporto
    {
        [Key, Required, Column("Sigla", TypeName = "varchar(5)")]
        public string Sigla { get; set; }
        [Required, Column("Nome", TypeName ="Varchar(20)")]
        public string Nome { get; set; }

        public Endereco Endereco { get; set; }

    }
}
