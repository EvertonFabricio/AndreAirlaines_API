using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndreAirlaines_API.Model
{
    public class PrecoBase
    {

        public int Id { get; set; }

        [Required]
        public Aeroporto Origem { get; set; }

        [Required]
        public Aeroporto Destino { get; set; }

        [Required]
        public double Valor { get; set; }

        public DateTime DataInclusao { get; set; } = DateTime.Now;

    }
}
