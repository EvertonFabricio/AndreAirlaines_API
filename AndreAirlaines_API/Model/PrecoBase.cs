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
        public double Preco { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataInclusao { get; set; } = DateTime.Now;

    }
}
