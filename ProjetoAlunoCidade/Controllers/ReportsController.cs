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

        public ActionResult GerarPDF()
        {
            List<Aluno> alunos = _repositorioGeralAluno.GetAll().ToList();

            // Chama a classe TabelaRelatorio para gerar o PDF
            Relatorio tabelaRelatorio = new Relatorio();
            byte[] pdfBytes = tabelaRelatorio.GerarPDF(alunos);

            // Retorna o PDF como um arquivo para download
            return File(pdfBytes, "application/pdf", "Relatorio_Alunos.pdf");
        }
    }
}
