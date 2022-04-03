using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndreAirlaines_API.Model
{
    public class Classe
    {
        public int Id { get; set; }

        [Required, Column("Descricao", TypeName = "varchar(50)")]
        public string Descricao { get; set; }

    }
}