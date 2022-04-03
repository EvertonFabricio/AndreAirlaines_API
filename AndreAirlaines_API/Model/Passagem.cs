using System;
using System.ComponentModel.DataAnnotations;

namespace AndreAirlaines_API.Model
{
    public class Passagem
    {
        public int Id { get; set; }

        [Required]
        public Voo Voo { get; set; }

        [Required]
        public Passageiro Passageiro { get; set; }

        [Required]
        public Classe Classe { get; set; }

        public double Desconto { get; set; }
        public double Valor { get; set; }
        public DateTime DataCompra { get; set; } = DateTime.Now;

    }
}
