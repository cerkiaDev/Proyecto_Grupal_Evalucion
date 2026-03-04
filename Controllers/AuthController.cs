using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Grupal.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
