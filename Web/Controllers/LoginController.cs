using BancoProject.Login;
using DTO.Login;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UsuarioTO usuarioTO)
        {
            UsuarioTO usuarioTo= LoginDB.Logar(usuarioTO);

            bool usuarioExiste = usuarioTO != null;
            if (usuarioExiste) {
                return RedirectToAction("Index", "Prova", usuarioTo);
            }
            return View(usuarioTo);
        }
    }
}
