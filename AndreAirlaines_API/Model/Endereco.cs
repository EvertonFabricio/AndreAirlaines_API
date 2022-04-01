using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace AndreAirlaines_API.Model
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }
        [JsonProperty,Column("Logradouro", TypeName = "varchar(150)")]
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        [JsonProperty,Column("Bairro", TypeName = "varchar(50)")]
        public string Bairro { get; set; }
        [JsonProperty, Column("Localidade", TypeName = "varchar(30)")]
        public string Localidade { get; set; }
        [JsonProperty, Column("Uf", TypeName = "varchar(2)")]
        public string Uf { get; set; }
        [JsonProperty, Column("Pais", TypeName = "varchar(20)")]
        public string Pais { get; set; }
        [Column("Cep", TypeName = "varchar(10)")]
        public string Cep { get; set; }
    }
}
