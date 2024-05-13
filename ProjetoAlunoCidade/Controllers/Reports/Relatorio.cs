using EM.Domain;
using EM.Web.Controllers.Reports.ExtensionMethod;
using iTextSharp5.text;
using iTextSharp5.text.pdf;

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
                writer.PageEvent = new ImageWatermark(@".\Controllers\Reports\Imagens\imgbackground2_480.png");
                writer.PageEvent = new HeaderFooter(@".\Controllers\Reports\Imagens\imgLogo.png");
                doc.Open();
                PdfPTable tabela = CriarTabela(alunos, zebrado);
                tabela.SpacingBefore = 20;
                tabela.HeaderRows = 1;
                
                doc.Add(tabela);

                doc.Close();

                return ms.ToArray();
            }

        }
        public static PdfPTable CriarTabela(List<Aluno> alunos, bool zebrado, string ordem = "")
        {
           
            PdfPTable table = new([11, 25, 16, 15, 7, 15, 6]);
            table.WidthPercentage = 100;

            table.AdicioneCelulaDeCabecalho(new Phrase("Matrícula", Fontes.FonteCelulaCabecalho()));
            table.AdicioneCelulaDeCabecalho(new Phrase("Nome", Fontes.FonteCelulaCabecalho()));
            table.AdicioneCelulaDeCabecalho(new Phrase("CPF", Fontes.FonteCelulaCabecalho()));
            table.AdicioneCelulaDeCabecalho(new Phrase("Nascimento", Fontes.FonteCelulaCabecalho()));
            table.AdicioneCelulaDeCabecalho(new Phrase("Sexo", Fontes.FonteCelulaCabecalho()));
            table.AdicioneCelulaDeCabecalho(new Phrase("Cidade", Fontes.FonteCelulaCabecalho()));
            table.AdicioneCelulaDeCabecalho(new Phrase("UF", Fontes.FonteCelulaCabecalho()));


            int contLinhas = 0;

            foreach (Aluno aluno in alunos)
            {
                BaseColor? backgroundColor = null;

                if (zebrado)
                {

                    backgroundColor = contLinhas % 2 == 0 ? new BaseColor(0, 30, 0, 40) : null;
                }

                Phrase matriculaPhrase = new Phrase(aluno.Matricula.ToString(), Fontes.FonteCelulaDados());
                table.AdicioneCelulaDeDado(matriculaPhrase, backgroundColor);

                Phrase nomePhrase = new Phrase(aluno.Nome, Fontes.FonteCelulaDados());
                table.AdicioneCelulaDeDado(nomePhrase, backgroundColor, horizontalAlignment: Element.ALIGN_LEFT);

                Phrase cpfPhrase = new Phrase(aluno.CPF, Fontes.FonteCelulaDados());
                table.AdicioneCelulaDeDado(cpfPhrase, backgroundColor);

                DateTime dataNascimento = aluno.Nascimento;
                (int anos, int meses, int dias) idade = dataNascimento.CalcularIdade();
                string idadeFormatada = $"{idade.anos} anos, {idade.meses}m, {idade.dias}d";

                Phrase idadePhrase = new Phrase(idadeFormatada, Fontes.FonteCelulaDados());
                table.AdicioneCelulaDeDado(idadePhrase, backgroundColor, horizontalAlignment: Element.ALIGN_LEFT);

                Phrase sexoPhrase = new Phrase(aluno.Sexo.ToString(), Fontes.FonteCelulaDados());
                table.AdicioneCelulaDeDado(sexoPhrase, backgroundColor);

                Phrase cidadePhrase = new Phrase(aluno.Cidade.Nome, Fontes.FonteCelulaDados());
                table.AdicioneCelulaDeDado(cidadePhrase, backgroundColor, horizontalAlignment: Element.ALIGN_LEFT);

                Phrase UFPhrase = new Phrase(aluno.Cidade.UF, Fontes.FonteCelulaDados());
                table.AdicioneCelulaDeDado(UFPhrase, backgroundColor);

                contLinhas++;
            }
            return table;
        }
    }
}
