using EM.Domain;
using EM.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EM.Web.Controllers
{
    public class AdministradorCidadeController : Controller
    {

        readonly IRepositorioCidade<Cidade> _reposiitorioCidade;

        public AdministradorCidadeController(IRepositorioCidade<Cidade> reposiitorioCidade)
        {
            _reposiitorioCidade = reposiitorioCidade;
        }
        public IActionResult Index()
        {
            var cidades = _reposiitorioCidade.GetAll();
            return View(cidades);
        }
        public IActionResult CadastroCidade(int? id)
        {
            if (id != null)
            {
                var cidade = _reposiitorioCidade.Get(c => c.Id_cidade == id).FirstOrDefault();
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
                if (cidade.Id_cidade != 0)
                {
                    _reposiitorioCidade.Update(cidade);
                }
                else
                {
                    _reposiitorioCidade.Add(cidade);
                }
                return RedirectToAction("Index");
            }
            return View(cidade);
        }

    }
}
