using BancoProject.Login;
using DTO.BancoClasses.Login;
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
            usuarioTO = LoginDB.Logar(usuarioTO);

            bool usuarioExiste = usuarioTO != null;
            if (usuarioExiste) { 
                //return RedirectToAction("Index", "Prova", usuarioTo);
                return RedirectToAction("Menu", "Prova", usuarioTO);
            }
            return View(usuarioTO);
        }
    }
}
