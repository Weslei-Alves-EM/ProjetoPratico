using EM.Domain;
using iTextSharp5.text;
using iTextSharp5.text.pdf;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EM.Web.Controllers.Reports
{
    public class Relatorio
    {
        [Obsolete]
        public byte[] GerarPDF(List<Aluno> alunos, string ordem, string orientacao, bool zebrado)
        {
            // Verifica a ordem selecionada e ordena os alunos de acordo
            switch (ordem)
            {
                case "Nome":
                    alunos = alunos.OrderBy(a => a.Nome).ToList();
                    break;
                case "Nascimento":
                    alunos = alunos.OrderBy(a => a.Nascimento).ToList();
                    break;
                case "Cidade":
                    alunos = alunos.OrderBy(a => a.Cidade.Nome).ToList();
                    break;
                case "UF":
                    alunos = alunos.OrderBy(a => a.Cidade.UF).ToList();
                    break;
                default:
                    // Se nenhuma ordem específica for selecionada, mantenha a ordem padrão
                    alunos = alunos.OrderBy(a => a.Cidade.UF).ThenBy(a => a.Nome).ToList();
                    break;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                // Document doc = new Document();


                Document doc;
                if (orientacao == "paisagem")
                {
                    doc = new(PageSize.A4.Rotate());
                }
                else
                {
                    doc = new Document();
                }

                doc.SetMargins(doc.LeftMargin, doc.RightMargin, doc.TopMargin, doc.BottomMargin + 20);

                PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                writer.PageEvent = new ImageWatermark("C:\\Workspace Weslei\\ProjetoAlunoCidade\\ProjetoAlunoCidade\\Controllers\\Reports\\Imagens\\imgbackground2_480.png");
                writer.PageEvent = new HeaderFooter("C:\\Workspace Weslei\\ProjetoAlunoCidade\\ProjetoAlunoCidade\\wwwroot\\images\\escolar-manager-web-176.png");
                doc.Open();
                PdfPTable tabela = CorpoDaTabela.CriarTabela(alunos, zebrado);
                tabela.SpacingBefore = 20;
                tabela.HeaderRows = 1;
                
                doc.Add(tabela);

                doc.Close();

                return ms.ToArray();
            }
        }
    }
}
