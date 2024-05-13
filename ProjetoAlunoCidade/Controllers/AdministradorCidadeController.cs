using EM.Domain;
using EM.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EM.Web.Controllers
{
    public class AdministradorCidadeController : Controller
    {

        readonly IRepositorioGeral<Cidade> _repositorioCidade;

        public AdministradorCidadeController(IRepositorioGeral<Cidade> repositorioCidade)
        {
            _repositorioCidade = repositorioCidade;
        }

        public IActionResult Index()
        {
            var cidades = _repositorioCidade.GetAll();
            return View(cidades);
        }
        public IActionResult CadastroCidade(int? id)
        {
            if (id != null)
            {
                var cidade = _repositorioCidade.Get(c => c.Id_cidade == id).FirstOrDefault();
                if (cidade == null)
                {
                    return NotFound();
                }

                ViewBag.IsEdicao = true;
                return View(cidade);
            }
            ViewBag.IsEdicao = false;
            return View(new Cidade());
        }

        [HttpPost]
        public IActionResult CadastroCidade(Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                if (cidade.Id_cidade > 0)
                {
                    _repositorioCidade.Update(cidade);
                }
                else
                {
                    _repositorioCidade.Add(cidade);
                }
                return RedirectToAction("Index");
            }
            return View(cidade);
        }
    }
}
