using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndreAirlaines_API.Model
{
    public class Passageiro
    {
        [Key, Required, Column("Cpf", TypeName = "varchar(14)")]
        public string Cpf { get; set; }
        [Required, Column("Nome", TypeName = "varchar(100)")]
        public string Nome { get; set; }
        [Required, Column("Telefone", TypeName = "varchar(20)")]
        public string Telefone { get; set; }
        [Required, DataType(DataType.Date), DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }
        [Required, Column("Email", TypeName = "varchar(100)")]
        public string Email { get; set; }
        public Endereco Endereco { get; set; }

    }
}
