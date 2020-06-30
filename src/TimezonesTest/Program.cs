using System;
using TimeZoneConverter;
using System.IO;
using System.Reflection;
using iText.Html2pdf;
using iText.Html2pdf.Resolver.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.StyledXmlParser.Css.Media;

namespace TimezonesTest
{
    class Program
    {
        /*static void Main(string[] args)
        {
            TimeZoneInfo tzi = TZConvert.GetTimeZoneInfo("Central Standard Time");
            TimeZoneInfo tzi2 = TZConvert.GetTimeZoneInfo("America/New_York");
            Console.WriteLine(tzi);
            Console.WriteLine(tzi2);
        }
*/
        static void Main(string[] args)
        {
            var path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var htmlPath = System.IO.Path.Combine(path, "htmlkCFDI.html");
            var htmlContent = File.ReadAllText(htmlPath);
            var pdfPath = System.IO.Path.Combine(path, "html.pdf");

            var result = Convert(htmlContent);
            File.WriteAllBytes(pdfPath, result);

            Console.WriteLine("Hello World!");
        }

        static byte[] Convert(string htmlContent)
        {
            byte[] pdf = null;
            using (var memoryStream = new MemoryStream())
            {
                var writterProperties = new WriterProperties()
                    .SetFullCompressionMode(true);
                using (PdfWriter writer = new PdfWriter(memoryStream, writterProperties))
                {
                    PdfDocument pdfDoc = new PdfDocument(writer);
                    pdfDoc.SetTagged();
                    PageSize pageSize = new PageSize(900, 900);
                    pdfDoc.SetDefaultPageSize(pageSize);

                    ConverterProperties converterProperties = new ConverterProperties();
                    converterProperties.SetCreateAcroForm(true);

                    var fp = new DefaultFontProvider(true, false, false);
                    converterProperties.SetFontProvider(fp);

                    MediaDeviceDescription mediaDescription = new MediaDeviceDescription(MediaType.SCREEN);
                    converterProperties.SetMediaDeviceDescription(mediaDescription);

                    HtmlConverter.ConvertToPdf(htmlContent, pdfDoc, converterProperties);
                    pdf = memoryStream.ToArray();

                    memoryStream.Close();
                    pdfDoc.Close();
                }
            }

            return pdf;
        }
    }
}
