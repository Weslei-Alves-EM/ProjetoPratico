using Microsoft.AspNetCore.Mvc;

namespace EM.Web.Controllers
{
    public class AdministradorAlunoController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }
        public IActionResult Editar()
        {
            return View();
        }

    }
}
