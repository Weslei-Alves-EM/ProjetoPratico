using EM.Domain;
using iTextSharp5.text;
using iTextSharp5.text.pdf;


namespace EM.Web.Controllers.Reports
{
    public class TabelaRelatorio
    {

        public byte[] GerarPDF(List<Aluno> alunos)
        {
            try
            {
               
                using (MemoryStream ms = new MemoryStream())
                {
                    
                    Document doc = new Document();

                    PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    writer.PageEvent = new ImageWatermark("C:\\Workspace Weslei\\ProjetoAlunoCidade\\ProjetoAlunoCidade\\wwwroot\\images\\escolar_manager_logo.png");
                    writer.PageEvent = new HeaderFooter("C:\\Workspace Weslei\\ProjetoAlunoCidade\\ProjetoAlunoCidade\\wwwroot\\images\\escolar-manager-web-176.png");

                    doc.Open();

                 
                    BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    Font fonteTitulo = new(bf, 20, Font.BOLD);

                    Paragraph title = new("Relatório de Alunos", fonteTitulo);
                    title.Alignment = Element.ALIGN_CENTER;
                    //doc.Add(title);

                    

                    PdfPTable tabela = CriarTabela(alunos);
                    tabela.SpacingBefore = 10;
                    tabela.HeaderRows = 1;
                    doc.Add(tabela);

                    // Fecha o documento
                    doc.Close();

                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao gerar PDF: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
                throw;
            }
        }


        static PdfPTable CriarTabela(List<Aluno> alunos)
        {

            BaseColor corFundoTitulo = new(0, 128, 128);
            BaseColor corFundoTabela = new(255, 255, 255);
            BaseColor corFonteTitulo = BaseColor.WHITE;

            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fonteTitulo = new(bf, 12, Font.BOLD, corFonteTitulo);

            PdfPTable table = new([12, 30, 19, 13, 8, 16]);
            table.WidthPercentage = 110;
            table.DefaultCell.BackgroundColor = corFundoTitulo;
            table.DefaultCell.FixedHeight = 30;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            table.AddCell(new Phrase("Matrícula", fonteTitulo));
            table.AddCell(new Phrase("Nome", fonteTitulo));
            table.AddCell(new Phrase("CPF", fonteTitulo));
            table.AddCell(new Phrase("Nascimento", fonteTitulo));
            table.AddCell(new Phrase("Sexo", fonteTitulo));
            table.AddCell(new Phrase("Cidade", fonteTitulo));

         

            foreach (Aluno aluno in alunos)
            {
                Font fonteConteudo = new(bf, 9, Font.NORMAL);               
               
               
                Phrase matriculaPhrase = new(aluno.Matricula.ToString(), fonteConteudo);
                PdfPCell matriculaCell = new(matriculaPhrase);
                matriculaCell.FixedHeight = 20;
                matriculaCell.HorizontalAlignment = Element.ALIGN_CENTER;
                matriculaCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(matriculaCell);

                Phrase nomePhrase = new Phrase(aluno.Nome, fonteConteudo);
                PdfPCell nomeCell = new PdfPCell(nomePhrase);
                nomeCell.FixedHeight = 20;
                nomeCell.HorizontalAlignment = Element.ALIGN_LEFT;
                nomeCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(nomeCell);

                Phrase cpfPhrase = new Phrase(aluno.CPF, fonteConteudo);
                PdfPCell cpfCell = new PdfPCell(cpfPhrase);
                cpfCell.FixedHeight = 20;
                cpfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                cpfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cpfCell);

                Phrase nascimentoPhrase = new Phrase(aluno.Nascimento.ToString("dd/MM/yyyy"), fonteConteudo);
                PdfPCell nascimentoCell = new PdfPCell(nascimentoPhrase);
                nascimentoCell.FixedHeight = 20;
                nascimentoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                nascimentoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(nascimentoCell);

                Phrase sexoPhrase = new Phrase(aluno.Sexo.ToString(), fonteConteudo);
                PdfPCell sexoCell = new PdfPCell(sexoPhrase);
                sexoCell.FixedHeight = 20;
                sexoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                sexoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(sexoCell);

                Phrase cidadePhrase = new Phrase(aluno.Cidade.Nome, fonteConteudo);
                PdfPCell cidadeCell = new PdfPCell(cidadePhrase);
                cidadeCell.FixedHeight = 20;
                cidadeCell.HorizontalAlignment = Element.ALIGN_CENTER;
                cidadeCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cidadeCell);

            }

            return table;
        }
       
    }
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
                // Carrega a imagem
                iTextSharp5.text.Image image = iTextSharp5.text.Image.GetInstance(_imagePath);
                //image.ScaleToFit(document.PageSize.Width, document.PageSize.Height); 
                image.ScaleAbsolute(document.PageSize.Width * 0.7f, document.PageSize.Height * 0.5f); // Define o tamanho da imagem como 50% da largura e altura da página

                PdfGState gs = new PdfGState();
                gs.FillOpacity = 0.3f;
                writer.DirectContentUnder.SetGState(gs);

                // Define a posição da imagem (centrada na página)
                image.SetAbsolutePosition((document.PageSize.Width - image.ScaledWidth) / 2, (document.PageSize.Height - image.ScaledHeight) / 2);

                // Obtém o conteúdo do canvas
                PdfContentByte canvas = writer.DirectContentUnder;

                // Adiciona a imagem ao canvas
                canvas.AddImage(image);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar marca d'água de imagem: " + ex.Message);
            }
        }
    }
}












