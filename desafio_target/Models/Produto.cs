using System.Text.Json.Serialization;

namespace desafio_target.Models
{
    public class Produto
    {
        [JsonPropertyName("codigoProduto")]
        public int Codigo { get; set; }
        [JsonPropertyName("descricaoProduto")]
        public string Descricao { get; set; }
        public int Estoque { get; set; }
    }
}
