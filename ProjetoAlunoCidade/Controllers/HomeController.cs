using EM.Domain;
using EM.Repository;
using Microsoft.AspNetCore.Mvc;
using ProjetoAlunoCidade.Models;
using System.Diagnostics;

namespace ProjetoAlunoCidade.Controllers
{
    public class HomeController : Controller
    {
        readonly IRepositorioAluno<Aluno> _repositorioAluno;

        public HomeController(IRepositorioAluno<Aluno> repositorioAluno)
        {
            _repositorioAluno = repositorioAluno;
        }

        public IActionResult Index()
        {
            var  alunos = _repositorioAluno.GetAll();
            return View(alunos);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
