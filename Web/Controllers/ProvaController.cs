using DTO;
using DTO.Login;
using DTO.Login.EstudanteFolder;
using DTO.ProvaModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Web.Models;

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
            PreencheProvaEmProgresso(usuarioTO);
            return View(ProvaEmProgresso);
        }

        private static void PreencheProvaEmProgresso(UsuarioTO usuarioTO)
        {
            const string getProvaURL = "https://localhost:7059/ProvaApi/GetProva";
            ProvaEmProgresso = RequestFromURLGetProvaTO(getProvaURL);
            ProvaEmProgresso.Usuario = usuarioTO;
        }

        public ActionResult Menu(UsuarioTO usuarioTO)
        {
            PreencheProvaEmProgresso(usuarioTO);
            return View(usuarioTO); 
        }
        public List<ProvaTO> GetListaProva()
        {
            const string urlListProvas = "https://localhost:7059/ProvaApi/ListProvas";
            List<ProvaTO> listProvaTO = GetListaProvaFromApi(urlListProvas);
            return listProvaTO;
        }

        public ActionResult ListarProvas()
        {
            bool isTeacher = ProvaEmProgresso.Usuario.UserIsTeacher;

            if (isTeacher)
            {
                return RedirectToAction("ListaProvaProfessor");
            }
            else
            {
                return RedirectToAction("ListaProvaEstudante");
            }
        }

        public ActionResult ListaProvaProfessor()
        {
            return ReturnViewListViewModelProva("ListProvaProfessor");
        }

        private ActionResult ReturnViewListViewModelProva(string nameMethodAction)
        {
            List<ProvaTO> listProvaTO = GetListaProva();
            ListProvaViewModel listProvaViewModel = new ListProvaViewModel
            {
                ListProvaTO = listProvaTO,
                IsTeacher = true
            };
            return View(nameMethodAction, listProvaViewModel);
        }

        public ActionResult ListaProvaEstudante()
        {
            return ReturnViewListViewModelProva("ListProvaProfessor");
        }
        private List<ProvaTO> GetListaProvaFromApi(string urlListProvas)
        {
            string jsonContent = HttpClient.GetStringAsync(urlListProvas).Result;

            List<ProvaTO> listProvaTO = JsonSerializer.Deserialize<List<ProvaTO>>(jsonContent);
            return listProvaTO;
        }

        public ActionResult CriarProva(ProvaTO provaTO)
        {
            const string urlCriarProva = "https://localhost:7059/ProvaApi/CriarProva";
            HttpClient.PostAsJsonAsync(urlCriarProva, provaTO);
            return View();
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
