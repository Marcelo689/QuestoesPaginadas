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
        public static UsuarioTO UsuarioLogado { get; set; }
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
            const string getProvaURL = $"https://localhost:7059/ProvaApi/GetProva";
            
            ProvaEmProgresso = RequestFromURLGetProvaTO(getProvaURL);
            UsuarioLogado = usuarioTO;
            ProvaEmProgresso.Usuario = usuarioTO;
        }
        public ActionResult Menu(UsuarioTO? usuarioTO)
        {
            if(usuarioTO is not null)
            {
                PreencheProvaEmProgresso(usuarioTO);
                return View(usuarioTO);
            }
            else
            {
                if (UsuarioLogado is not null)
                    return View(UsuarioLogado);
                else
                    return View("Error");
            }
        }
        public List<ProvaTO> GetListaProva()
        {
            const string urlListProvas = "https://localhost:7059/ProvaApi/ListProvas";
            List<ProvaTO> listProvaTO = GetListaProvaFromApi(urlListProvas);
            return listProvaTO;
        }
        public ActionResult CriarProva()
        {
            return View(new ProvaTO());
        }
        public ActionResult EditarProva(int provaId)
        {
            string urlEditarProva = $"https://localhost:7059/ProvaApi/EditarProva/?provaId={provaId}";
            ProvaTO provaTO = GetProvaById(urlEditarProva);
            provaTO.Usuario = UsuarioLogado;
            return View(new EditarProvaViewModel
            {
                Prova = provaTO,
                IsTeacher = UsuarioLogado.IsTeacher
            });   
        }

        [HttpPost]
        public ActionResult AtualizarProva(ProvaOpcoesMarcadasViewModel provaOpcoes)
        {
            AtualizarProvaOpcoes(provaOpcoes);
            return RedirectToAction("Menu", UsuarioLogado);
        }

        [HttpPost("DeletarQuestao")]
        public void DeletarQuestao(QuestaoTO questaoTO)
        {
            const string urlDeletarQuestao = $"https://localhost:7059/ProvaApi/DeletarQuestao";
            HttpClient.PostAsJsonAsync(urlDeletarQuestao, questaoTO);
        }
        private void AtualizarProvaOpcoes(ProvaOpcoesMarcadasViewModel provaOpcoes)
        {
            ProvaEmProgresso.PreencherRespostas(provaOpcoes, true);
            const string urlEditarProva = "https://localhost:7059/ProvaApi/EditAnswers";
            
            HttpClient.PostAsJsonAsync(urlEditarProva, ProvaEmProgresso);
        }
        private ProvaTO GetProvaById(string urlEditarProva)
        {
            string jsonContent = HttpClient.GetStringAsync(urlEditarProva).Result;
            ProvaTO provaTO = JsonSerializer.Deserialize<ProvaTO>(jsonContent);

            return provaTO;
        }
        public ActionResult ListarProvas()
        {
            bool isTeacher = ProvaEmProgresso.Usuario.IsTeacher;

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
            return ReturnViewListViewModelProva("ListaProvaProfessor");
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
            return ReturnViewListViewModelProva("ListaProvaEstudante");
        }
        private List<ProvaTO> GetListaProvaFromApi(string urlListProvas)
        {
            string jsonContent = HttpClient.GetStringAsync(urlListProvas).Result;

            List<ProvaTO> listProvaTO = JsonSerializer.Deserialize<List<ProvaTO>>(jsonContent);
            return listProvaTO;
        }

        [HttpPost]
        public ActionResult CriarProva(ProvaTO provaTO)
        {
            const string urlCriarProva = "https://localhost:7059/ProvaApi/CriarProva";
            PreencheProvaTOComUsuario(provaTO);
            HttpClient.PostAsJsonAsync(urlCriarProva, provaTO);
            return RedirectToAction("Menu", UsuarioLogado);
        }

        [HttpPost]
        public void CriarQuestaoAjax(ProvaTO provaTO)
        {
            const string urlCriarQuestao = $"";
            HttpClient.PostAsJsonAsync(urlCriarQuestao, provaTO);
        }
        private void PreencheProvaTOComUsuario(ProvaTO provaTO)
        {
            provaTO.Usuario = UsuarioLogado;
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
