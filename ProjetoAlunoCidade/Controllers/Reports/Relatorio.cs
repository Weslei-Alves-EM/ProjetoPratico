using EM.Domain;
using EM.Web.Controllers.Reports.ExtensionMethod;
using iTextSharp5.text;
using iTextSharp5.text.pdf;


namespace EM.Web.Controllers.Reports
{
    public class Relatorio
    {

        public byte[] GerarPDF(List<Aluno> alunos)
        {
            alunos = alunos.OrderBy(aluno => aluno.Cidade.UF).ThenBy(aluno => aluno.Nome).ToList();

            using (MemoryStream ms = new MemoryStream())
            {

                Document doc = new Document();
                doc.SetMargins(doc.LeftMargin, doc.RightMargin, doc.TopMargin, doc.BottomMargin + 20);

                PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                writer.PageEvent = new ImageWatermark("C:\\Workspace Weslei\\ProjetoAlunoCidade\\ProjetoAlunoCidade\\Controllers\\Reports\\Imagens\\imgbackground2_480.png");
                writer.PageEvent = new HeaderFooter("C:\\Workspace Weslei\\ProjetoAlunoCidade\\ProjetoAlunoCidade\\wwwroot\\images\\escolar-manager-web-176.png");

                doc.Open();

                PdfPTable tabela = CorpoDaTabela.CriarTabela(alunos);
                tabela.SpacingBefore = 20;
                tabela.HeaderRows = 1;
                doc.Add(tabela);

                doc.Close();

                return ms.ToArray();
            }
        }
    }
}