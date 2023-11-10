using Microsoft.AspNetCore.Mvc;

namespace Login_App.Controllers
{
    public class InicioController : Controller
    {
        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrarse()
        {
            return View();
        }
    }
}
