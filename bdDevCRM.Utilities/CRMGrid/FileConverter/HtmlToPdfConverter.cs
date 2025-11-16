//using SelectPdf;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Utilities.FileConverter
//{
//    public class HtmlToPdfConverter
//    {

//        public string BaseUrl { get; set; }
//        public PdfPageSize PageSize { get; set; }
//        public PdfPageOrientation Orientation { get; set; }
//        public int PageWidth = 0;
//        public int PageHeight = 0;




//        public HtmlToPdfConverter()
//        {
//            PageSize = PdfPageSize.A4;
//        }

//        public PdfDocument ConvertToPDF(string html)
//        {
//            string htmlString = html;
//            string baseUrl = BaseUrl;
//            PdfPageSize pageSize = PageSize;
//            PdfPageOrientation pdfOrientation = Orientation;




//            // instantiate a html to pdf converter object
//            HtmlToPdf converter = new HtmlToPdf();

//            // set converter options
//            converter.Options.PdfPageSize = pageSize;
//            converter.Options.PdfPageOrientation = pdfOrientation;
//            converter.Options.WebPageFixedSize = false;
//            converter.Options.WebPageWidth = 800;
//            converter.Options.WebPageHeight = PageHeight;
//            converter.Options.MarginLeft = 72;
//            converter.Options.MarginTop = 144;
//            converter.Options.MarginRight = 72;
//            converter.Options.MarginBottom = 72;
//            converter.Options.EmbedFonts = false;
//            // create a new pdf document converting an url
//            PdfDocument doc = converter.ConvertHtmlString(htmlString, baseUrl);

//            //// save pdf document
//            //doc.Save(Response, false, DateTime.Now.Ticks + ".pdf");

//            //// close pdf document
//            //doc.Close();
//            return doc;
//        }



//    }
//}
