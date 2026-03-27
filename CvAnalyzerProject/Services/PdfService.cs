using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

namespace CvAnalyzerProject.Services
{
    public class PdfService
    {
        public string ExtractText(Stream pdfStream)
        {
            using (var reader = new PdfReader(pdfStream))
            using (var pdf = new PdfDocument(reader))
            {
                string text = "";

                for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
                {
                    var page = pdf.GetPage(i);
                    text += PdfTextExtractor.GetTextFromPage(page) + "\n";
                }

                return text;
            }
        }
    }
}