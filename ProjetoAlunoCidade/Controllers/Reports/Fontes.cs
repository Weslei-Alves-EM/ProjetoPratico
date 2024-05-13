using iTextSharp5.text.pdf;
using iTextSharp5.text;

namespace EM.Web.Controllers.Reports
{
    public static class Fontes
    {
        private static BaseFont FontePadrao() => BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

        public static Font FonteCelulaCabecalho() => new(FontePadrao(), 12, Font.BOLD, BaseColor.WHITE);
        public static Font FonteCelulaDados() => new(FontePadrao(), 10, Font.NORMAL);
        public static Font FontTituloCabecalho() => new(FontePadrao(), 15, Font.BOLD, new(0, 100, 0));
        public static Font FontInformacaoRodape() => new(FontePadrao(), 10, Font.BOLD, new(169, 169, 169));


    }
}
