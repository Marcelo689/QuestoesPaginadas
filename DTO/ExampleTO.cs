using System.Text.Json.Serialization;

namespace DTO
{
    public class ExampleTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }
    }
}
