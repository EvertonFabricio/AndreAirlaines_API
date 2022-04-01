using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndreAirlaines_API.Model
{
    public class Aeronave
    {
        [Key, Required, Column("Id", TypeName = "varchar(10)")]
        public string Id { get; set; }

        [Required, Column("Nome", TypeName = "varchar(50)")]
        public string Nome { get; set; }
        [Required]
        public int Capacidade { get; set; }

    }
}
