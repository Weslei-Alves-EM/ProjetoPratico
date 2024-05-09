using iTextSharp5.text.pdf;
using iTextSharp5.text;

namespace EM.Web.Controllers.Reports.ExtensionMethod
{
    public static class MetodoExtensao
    {
        public static (int anos, int meses, int dias) CalcularIdade(this DateTime dataNascimento)
        {
            DateTime dataAtual = DateTime.Today;
            int anos = dataAtual.Year - dataNascimento.Year;
            int meses = dataAtual.Month - dataNascimento.Month;
            int dias = dataAtual.Day - dataNascimento.Day;

            if (meses < 0 || (meses == 0 && dias < 0))
            {
                anos--;
                meses = (dataAtual.Month + 12) - dataNascimento.Month;
            }

            if (dataAtual.Day < dataNascimento.Day)
            {
                meses--;
                int ultimoDiaMesAnterior = DateTime.DaysInMonth(dataAtual.Year, dataAtual.Month == 1 ? 12 : dataAtual.Month - 1);
                dias = ultimoDiaMesAnterior - dataNascimento.Day + dataAtual.Day;
            }

            return (anos, meses, dias);
        }

        public static void AdicioneCelulaDeDado(this PdfPTable table, Phrase phrase, BaseColor backgroundColor, float fixedHeight = 15, int horizontalAlignment = Element.ALIGN_CENTER, int verticalAlignment = Element.ALIGN_MIDDLE)
        {
            PdfPCell cell = new(phrase);
            cell.FixedHeight = fixedHeight;
            cell.HorizontalAlignment = horizontalAlignment;
            cell.VerticalAlignment = verticalAlignment;
            cell.BackgroundColor = backgroundColor;

            table.AddCell(cell);
        }


        public static void AdicioneCelulaDeCabecalho(this PdfPTable table, Phrase phrase, float fixedHeight = 25, int horizontalAlignment = Element.ALIGN_CENTER, int verticalAlignment = Element.ALIGN_MIDDLE)
        {
            PdfPCell cell = new PdfPCell(phrase);
            cell.FixedHeight = fixedHeight;
            cell.HorizontalAlignment = horizontalAlignment;
            cell.VerticalAlignment = verticalAlignment;
            cell.BackgroundColor = new(0, 100, 0); 

            table.AddCell(cell);
        }

        public static void ZebrarPDF(this PdfPTable table, BaseColor corPar, BaseColor corImpar)
        {
            bool isPar = false;
            for (int i = 1; i < table.Rows.Count; i++)
            {
                PdfPRow row = table.Rows[i];
                foreach (PdfPCell cell in row.GetCells())
                {
                    cell.BackgroundColor = isPar ? corPar : corImpar;
                }
                isPar = !isPar;
            }
        }

    }
}
