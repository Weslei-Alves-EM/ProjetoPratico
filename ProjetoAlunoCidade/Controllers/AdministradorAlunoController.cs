using EM.Domain;
using EM.Domain.Enuns;
using EM.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EM.Web.Controllers
{
    public class AdministradorAlunoController : Controller
    {
        readonly IRepositorioGeral<Cidade> _repositorioGeralCidade;
        readonly IRepositorioGeral<Aluno> _repositorioGeralAluno;
        readonly IRepositorioAluno<Aluno> _repositorioAluno;


        public AdministradorAlunoController(IRepositorioGeral<Cidade> repositorioGeralCidade, IRepositorioAluno<Aluno> repositorioAluno, IRepositorioGeral<Aluno> repositorioGeralAluno)
        {
            _repositorioGeralCidade = repositorioGeralCidade;
            _repositorioAluno = repositorioAluno;
            _repositorioGeralAluno = repositorioGeralAluno;
        }

        public IActionResult Index()
        {
            var alunos = _repositorioGeralAluno.GetAll();
            return View(alunos);
        }

        public IActionResult Buscar(string matricula, string nome, string estado)
        {
            IEnumerable<Aluno> alunos = null;

            // Verifica se a busca é por matrícula
            if (!string.IsNullOrEmpty(matricula))
            {
                int matriculaInt;
                if (int.TryParse(matricula, out matriculaInt))
                {
                    var aluno = _repositorioAluno.GetByMatricula(matriculaInt);
                    if (aluno != null)
                    {
                        alunos = new List<Aluno> { aluno };
                    }
                }
            }
            // Verifica se a busca é por nome
            else if (!string.IsNullOrEmpty(nome))
            {
                alunos = _repositorioAluno.GetByContendoNoNome(nome);
            }
            // Verifica se a busca é por estado
            else if (!string.IsNullOrEmpty(estado))
            {
                alunos = _repositorioAluno.GetByEstado(estado);
            }
            // Caso nenhum parâmetro tenha sido fornecido, carrega todos os alunos
            else
            {
                alunos = _repositorioGeralAluno.GetAll();
            }

            return View("Index", alunos);
        }


        public IActionResult CadastroAluno(int? id)
        {
            ViewBag.Cidades = _repositorioGeralCidade.GetAll().ToList();

            if (id != null)
            {
                var aluno = _repositorioGeralAluno.Get(a => a.Id_Alunos == id).FirstOrDefault();
                if (aluno == null)
                {
                    return NotFound();
                }
                ViewBag.IsEdicao = true;
                if (aluno.Nascimento == default(DateTime))
                {
                    aluno.Nascimento = DateTime.Today;
                }
                if (aluno.Sexo == 0 || aluno.Sexo == (EnumeradorSexo)0)
                {
                    aluno.Sexo = EnumeradorSexo.Masculino; // ou qualquer valor padrão que você queira
                }
                return View(aluno);
            }
            ViewBag.IsEdicao = false;
            return View(new Aluno());

        }

        [HttpPost]
        public IActionResult CadastroAluno(Aluno aluno)
        {
            ViewBag.Cidades = _repositorioGeralCidade.GetAll().ToList();

            if (ModelState.IsValid)
            {
                if (aluno.Id_Alunos > 0)
                {
                    _repositorioGeralAluno.Update(aluno);
                }
                else
                {
                    _repositorioGeralAluno.Add(aluno);
                }
                return RedirectToAction("Index");
            }
            return View(aluno);
        }

        [HttpPost]
        public IActionResult RemoverAluno(int id)
        {
            var aluno = _repositorioGeralAluno.Get(a => a.Id_Alunos == id).FirstOrDefault();
            if (aluno == null)
            {
                return NotFound();
            }

            _repositorioAluno.Remove(aluno);
            return RedirectToAction("Index");
        }
    }
}
