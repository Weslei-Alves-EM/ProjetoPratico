using iTextSharp5.text.pdf;
using iTextSharp5.text;

namespace EM.Web.Controllers.Reports
{
    public class HeaderFooter : PdfPageEventHelper
    {
        private string _imagePath;

        public HeaderFooter(string imagePath)
        {
            _imagePath = imagePath;
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);

            PdfPTable header = new(2);
            header.WidthPercentage = 110;
            header.DefaultCell.Border = PdfPCell.NO_BORDER;

            PdfPCell imageCell = new();
            
            Image image = Image.GetInstance(_imagePath);
            image.ScaleToFit(200f, 100f);
            imageCell.AddElement(image);
            imageCell.HorizontalAlignment = Element.ALIGN_LEFT;
            imageCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            imageCell.Border = PdfPCell.NO_BORDER;
            
            header.AddCell(imageCell);


            Phrase textPhrase = new Phrase("RELAÇÃO GERAL DE ALUNOS", new Font(Font.FontFamily.HELVETICA, 15, Font.BOLD));
            header.DefaultCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            header.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            header.AddCell(textPhrase);

            document.Add(header);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            PdfPTable footer = new PdfPTable(1);
            footer.TotalWidth = document.PageSize.Width;
            footer.DefaultCell.Border = PdfPCell.NO_BORDER;
            footer.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            footer.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            // Adicione o conteúdo do rodapé aqui
            Phrase footerPhrase = new Phrase("Relatório Geral de Alunos", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD));
            footer.AddCell(footerPhrase);

            footer.WriteSelectedRows(0, -1, 0, document.BottomMargin, writer.DirectContent);
        }
    }

}
