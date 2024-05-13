using iTextSharp5.text.pdf;
using iTextSharp5.text;

namespace EM.Web.Controllers.Reports
{
    public class ImageWatermark : PdfPageEventHelper
    {
        private string _imagePath;

        public ImageWatermark(string imagePath)
        {
            _imagePath = imagePath;
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            try
            {
                
                Image image = Image.GetInstance(_imagePath);

                PdfGState gs = new PdfGState();
                gs.FillOpacity = 0.3f;
                writer.DirectContentUnder.SetGState(gs);

                
                image.SetAbsolutePosition((document.PageSize.Width - image.ScaledWidth) / 2, (document.PageSize.Height - image.ScaledHeight) / 2);

                
                PdfContentByte canvas = writer.DirectContentUnder;

                
                canvas.AddImage(image);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar marca d'água de imagem: " + ex.Message);
            }
        }
    }
}
