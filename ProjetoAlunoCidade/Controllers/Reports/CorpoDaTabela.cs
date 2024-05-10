using EM.Domain;
using EM.Web.Controllers.Reports.ExtensionMethod;
using iTextSharp5.text;
using iTextSharp5.text.pdf;

namespace EM.Web.Controllers.Reports
{
    public static class CorpoDaTabela
    {
        public static PdfPTable CriarTabela(List<Aluno> alunos, bool zebrado, string ordem = "")
        {
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
                    break;
            }

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
                   
                    backgroundColor = contLinhas % 2 == 0 ? new BaseColor(0,30,0,40) : null;
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
