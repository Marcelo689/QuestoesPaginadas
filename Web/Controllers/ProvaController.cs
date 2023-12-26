using DTO;
using DTO.Login;
using DTO.Login.EstudanteFolder;
using DTO.ProvaModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Web.Controllers
{
    public class ProvaController : Controller
    {
        public static EstudanteTO EstudanteLogado { get; set; }
        public static HttpClient HttpClient = new HttpClient();
        public static ProvaTO ProvaEmProgresso { get; set; }
        public ProvaController()
        {
            EstudanteLogado = new EstudanteTO
            {
                Id = 1,
                Name = "Marcelo",
            };
        }
        public IActionResult Index(UsuarioTO usuarioTO)
        {
            /// mandar usuario para backend para logar e pegar os resultados desse usuario
            const string getProvaURL = "https://localhost:7059/ProvaApi/GetProva";
            ProvaTO? provaModel = RequestFromURLGetProvaTO(getProvaURL);
            provaModel.Usuario = usuarioTO;

            ProvaEmProgresso = provaModel;
            return View(provaModel);
        }

        private static ProvaTO? RequestFromURLGetProvaTO(string url)
        {
            var jsonString = HttpClient.GetStringAsync(url).Result;
            ProvaTO? provaModel = JsonSerializer.Deserialize<ProvaTO>(jsonString);
            return provaModel;
        }

        [HttpPost]
        public ActionResult SaveAnswers(ProvaOpcoesMarcadasViewModel provaOpcoes)
        {
            ProvaEmProgresso.PreencherRespostas(provaOpcoes);
            const string saveAnswersURL = "https://localhost:7059/ProvaApi/SaveAnswers";

            HttpClient.PostAsJsonAsync(saveAnswersURL,ProvaEmProgresso ).Wait();
            return RedirectToAction("Index", ProvaEmProgresso);
        }

        [HttpPost]
        public ActionResult SalvarQuestoes(ProvaOpcoesMarcadasViewModel provaViewModel)
        {
            ProvaEmProgresso.PreencherRespostas(provaViewModel);
            return View("Index", ProvaEmProgresso);
        }

        [HttpPost]
        public ActionResult SalvarQuestoesDB(ProvaOpcoesMarcadasViewModel provaViewModel)
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
