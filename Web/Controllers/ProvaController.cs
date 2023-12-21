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
            var jsonString = new HttpClient().GetStringAsync("https://localhost:7059/api/Prova/GetProva").Result;
            ProvaTO? provaModel = JsonSerializer.Deserialize<ProvaTO>(jsonString);
            provaModel.Usuario = usuarioTO;

            ProvaEmProgresso = provaModel;
            return View(provaModel);
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
