using DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace QuestoesApi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
             _logger = logger;
        }

        public void OnGet()
        {

            Questoes = new List<QuestaoTO>{
                new QuestaoTO(new List<string> { "a","a","a","a","a" }){
                    Id = 1,
                    Name = "a",
                }
                
            }
            var provaJson = new HttpClient().GetStringAsync("https://localhost:7059").Result;

            DTO.ProvaTO prova = JsonSerializer.Deserialize<DTO.ProvaTO>(provaJson);
        }
    }
}