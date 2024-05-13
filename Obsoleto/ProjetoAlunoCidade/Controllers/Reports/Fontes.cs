using iTextSharp5.text.pdf;
using iTextSharp5.text;

namespace EM.Web.Controllers.Reports
{
    public static class Fontes
    {
        public static Font FonteCelulaCabecalho() => 
            new(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12, Font.BOLD, BaseColor.WHITE);
         public static Font FonteCelulaDados() => 
            new(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 10, Font.NORMAL);
        


    }
}
