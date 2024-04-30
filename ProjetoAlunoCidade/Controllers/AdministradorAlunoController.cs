using EM.Domain;
using EM.Repository;
using Microsoft.AspNetCore.Mvc;
using ProjetoAlunoCidade.Models;
using System.Diagnostics;

namespace EM.Web.Controllers
{
    public class AdministradorAlunoController : Controller
    {
        readonly IRepositorioCidade<Cidade> _repositorioCidade;
        readonly IRepositorioAluno<Aluno> _repositorioAluno;


        public AdministradorAlunoController(IRepositorioCidade<Cidade> repositorioCidade, IRepositorioAluno<Aluno> repositorioAluno)
        {
            _repositorioCidade = repositorioCidade;
            _repositorioAluno = repositorioAluno;
        }
        public IActionResult Index()
        {
            var alunos = _repositorioAluno.GetAll();
            return View(alunos);
        }

        public IActionResult CadastroAluno(int? id)
        {
            ViewBag.Cidades = _repositorioCidade.GetAll().ToList();

            if (id != null)
            {
                var aluno = _repositorioAluno.Get(a => a.Id_Alunos == id).FirstOrDefault();
                if (aluno == null)
                {
                    return NotFound();
                }
                ViewBag.IsEdicao = true;
                return View(aluno);
            }


            ViewBag.IsEdicao = false;
            return View(new Aluno());

        }

        [HttpPost]
        public IActionResult CadastroAluno(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                if (aluno.Id_Alunos > 0)
                {
                    _repositorioAluno.Update(aluno);
                }
                else
                {
                    _repositorioAluno.Add(aluno);
                }
                return RedirectToAction("Index");
            }

            ViewBag.IsEdicao = aluno.Id_Alunos > 0;
            ViewBag.Cidades = _repositorioCidade.GetAll().ToList();
            return View(aluno);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
