
using EM.Domain;
using EM.Repository;
using EM.Web.Controllers.Reports;
using Microsoft.AspNetCore.Mvc;
using System;


namespace EM.Web.Controllers
{
    public class ReportsController : Controller
    {
        readonly IRepositorioGeral<Aluno> _repositorioGeralAluno;

        public ReportsController(IRepositorioGeral<Aluno> repositorioGeralAluno)
        {
            _repositorioGeralAluno = repositorioGeralAluno;
        }


        [HttpGet("Reports/GerarPDF", Name = "GerarPDF")]
        public ActionResult GerarPDF(string estadoId, string Ordem, string orientacao, bool zebrado)
        {
            List<Aluno> alunos = _repositorioGeralAluno.GetAll().ToList();

            // Aplicar filtro de estado, se selecionado
            if (!string.IsNullOrEmpty(estadoId))
            {
                alunos = alunos.Where(a => a.Cidade.UF == estadoId).ToList();
            }

            // Chamar a classe TabelaRelatorio para gerar o PDF
            Relatorio tabelaRelatorio = new Relatorio();
            byte[] pdfBytes = tabelaRelatorio.GerarPDF(alunos, Ordem, orientacao, zebrado);

            // Retornar o PDF como um arquivo para download
            return File(pdfBytes, "application/pdf", "Relatorio_Alunos.pdf");
        }
    }
}
