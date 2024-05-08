using EM.Domain;
using EM.Web.Controllers.Reports.ExtensionMethod;
using iTextSharp5.text.pdf;
using iTextSharp5.text;

namespace EM.Web.Controllers.Reports
{
    public static class CorpoDaTabela
    {
        public static PdfPTable CriarTabela(List<Aluno> alunos, string estado = "")
        {
            IEnumerable<Aluno> alunosFiltrados = alunos;
            if (!string.IsNullOrEmpty(estado))
            {
                alunosFiltrados = alunos.Where(a => a.Cidade.UF == estado);
            }
            PdfPTable table = new([12, 30, 19, 13, 8, 16]);
            table.WidthPercentage = 110;

            table.AdicioneCelulaDeCabecalho(new Phrase("Matrícula", Fontes.FonteCelulaCabecalho()));
            table.AdicioneCelulaDeCabecalho(new Phrase("Nome", Fontes.FonteCelulaCabecalho()));
            table.AdicioneCelulaDeCabecalho(new Phrase("CPF", Fontes.FonteCelulaCabecalho()));
            table.AdicioneCelulaDeCabecalho(new Phrase("Nascimento", Fontes.FonteCelulaCabecalho()));
            table.AdicioneCelulaDeCabecalho(new Phrase("Sexo", Fontes.FonteCelulaCabecalho()));
            table.AdicioneCelulaDeCabecalho(new Phrase("Cidade", Fontes.FonteCelulaCabecalho()));



            foreach (Aluno aluno in alunos)
            {
                Phrase matriculaPhrase = new Phrase(aluno.Matricula.ToString(), Fontes.FonteCelulaDados());
                table.AdicioneCelulaDeDado(matriculaPhrase);

                Phrase nomePhrase = new Phrase(aluno.Nome, Fontes.FonteCelulaDados());
                table.AdicioneCelulaDeDado(nomePhrase, horizontalAlignment: Element.ALIGN_LEFT);

                Phrase cpfPhrase = new Phrase(aluno.CPF, Fontes.FonteCelulaDados());
                table.AdicioneCelulaDeDado(cpfPhrase);

                DateTime dataNascimento = aluno.Nascimento;
                (int anos, int meses, int dias) idade = dataNascimento.CalcularIdade();
                string idadeFormatada = $"{idade.anos} anos, {idade.meses}m, {idade.dias}d";

                Phrase idadePhrase = new Phrase(idadeFormatada, Fontes.FonteCelulaDados());
                table.AdicioneCelulaDeDado(idadePhrase);

                Phrase sexoPhrase = new Phrase(aluno.Sexo.ToString(), Fontes.FonteCelulaDados());
                table.AdicioneCelulaDeDado(sexoPhrase);

                Phrase cidadePhrase = new Phrase(aluno.Cidade.Nome, Fontes.FonteCelulaDados());
                table.AdicioneCelulaDeDado(cidadePhrase);

            }

            return table;
        }

    }
}
