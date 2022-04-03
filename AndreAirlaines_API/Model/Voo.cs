using System;
using System.ComponentModel.DataAnnotations;

namespace AndreAirlaines_API.Model
{
    public class Voo
    {
        public int Id { get; set; }
        [Required]
        public Aeroporto Destino { get; set; }
        [Required]
        public Aeroporto Origem { get; set; }
        [Required]
        public Aeronave Aeronave { get; set; }
        [Required]
        public DateTime HoraEmbarque { get; set; }
        [Required]
        public DateTime HoraDesembarque { get; set; }

    }
}
