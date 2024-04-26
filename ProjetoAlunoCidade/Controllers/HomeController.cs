using EM.Domain;
using EM.Repository;
using Microsoft.AspNetCore.Mvc;
using ProjetoAlunoCidade.Models;
using System.Diagnostics;

namespace ProjetoAlunoCidade.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositorioAluno<Aluno> _repositorioAluno;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IRepositorioAluno<Aluno> repositorioAluno)
        {
            _logger = logger;
            _repositorioAluno = repositorioAluno;
        }

        public IActionResult Index()
        {
            var  alunos = _repositorioAluno.GetAll();
            return View(alunos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
