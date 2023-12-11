using DTO.ProvaModels;
using System.Text.Json.Serialization;

namespace DTO
{
    public class ProvaTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("description")]
        public string Description { get; set; } = "";
        
        [JsonPropertyName("questoes")]
        public QuestaoTO[]  Questoes { get; set; }

        public ProvaResultado GetResultados()
        {
            return new ProvaResultado
            {
                Questoes = Questoes
            };
        }
    }
}
