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
            header.WidthPercentage = 100;
            header.DefaultCell.Border = PdfPCell.NO_BORDER;

            PdfPCell imageCell = new();
            
            Image image = Image.GetInstance(_imagePath);
            image.ScaleToFit(200f, 100f);
            imageCell.AddElement(image);
            imageCell.HorizontalAlignment = Element.ALIGN_LEFT;
            imageCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            imageCell.Border = PdfPCell.NO_BORDER;
            
            header.AddCell(imageCell);
                        
            Phrase textPhrase = new Phrase("RELAÇÃO GERAL DE ALUNOS", Fontes.FontTituloCabecalho());
            header.DefaultCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            header.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            header.AddCell(textPhrase);

            document.Add(header);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {

            base.OnEndPage(writer, document);

            PdfPTable footer = new PdfPTable(2);
            footer.TotalWidth = document.PageSize.Width;
            footer.DefaultCell.Border = PdfPCell.NO_BORDER;

            PdfPCell messageCell = new PdfPCell(new Phrase("Escolar Manager Softwares para Gestão Escolar", Fontes.FontInformacaoRodape()));
            messageCell.HorizontalAlignment = Element.ALIGN_LEFT;
            messageCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            messageCell.Border = PdfPCell.NO_BORDER;
            footer.AddCell(messageCell);

            PdfPCell dateCell = new PdfPCell(new Phrase("Emissão\n" + DateTime.Now.ToString("dd/MM/yyyy"), Fontes.FontInformacaoRodape()));
            dateCell.HorizontalAlignment = Element.ALIGN_CENTER;
            dateCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            dateCell.Border = PdfPCell.NO_BORDER;
            footer.AddCell(dateCell);

            float footerPosition = document.BottomMargin - 20; 
            footer.WriteSelectedRows(0, -1, 0, footerPosition, writer.DirectContent);
        }
    }

}
