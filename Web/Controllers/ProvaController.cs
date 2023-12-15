using DTO;
using DTO.ProvaModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Web.Controllers
{
    public class ProvaController : Controller
    {
        public static Estudante EstudanteLogado { get; set; }
        public static ProvaTO ProvaEmProgresso { get; set; }
        public ProvaController()
        {
            EstudanteLogado = new Estudante
            {
                Id = 1,
                Name = "Marcelo",
            };
        }
        public IActionResult Index()
        {
            var jsonString = new HttpClient().GetStringAsync("https://localhost:7059/api/Prova").Result;

            var jsonOptions = new JsonSerializerOptions();
            jsonOptions.IncludeFields = true;
            ProvaTO? provaModel = JsonSerializer.Deserialize<ProvaTO>(jsonString, jsonOptions);
            ProvaEmProgresso = provaModel;
            return View(provaModel);
        }
        [HttpPost]
        public ActionResult SalvarQuestoes(ProvaOpcoesMarcadasViewModel provaViewModel)
        {
            ProvaEmProgresso.PreencherRespostas(provaViewModel);
            return View("Index", ProvaEmProgresso);
        }
        public ActionResult Resultados()
        {
            ProvaResultado resultado = ProvaEmProgresso.GetResultados();
            return View(resultado);
        }
    }
}
