//using System.Diagnostics;
//using Microsoft.Office.Interop.Excel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Utilities
//{
//    public class AzExportExcelToPdf
//    {
//        public string ConvertToPdf(string xlFilePath)
//        {
//            string physicalPath = System.Web.HttpContext.Current.Server.MapPath(xlFilePath);
//            Application app = new Application();
//            Workbook wkb = app.Workbooks.Open(physicalPath);
//            try
//            {
//                foreach (Worksheet ws in wkb.Worksheets.OfType<Worksheet>())
//                {
//                    ws.PageSetup.Orientation = XlPageOrientation.xlLandscape;
//                    ws.PageSetup.Zoom = false;
//                    ws.PageSetup.FitToPagesWide = 1;
//                    ws.PageSetup.FitToPagesTall = false;
//                }

//                string pdfFilePthe = xlFilePath.Contains(".xls")
//                    ? xlFilePath.Replace(".xls", ".pdf")
//                    : xlFilePath.Replace(".xlsx", ".pdf");
//                string physicalPDfPath = System.Web.HttpContext.Current.Server.MapPath(pdfFilePthe);
//                wkb.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, physicalPDfPath);

//                return pdfFilePthe;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//            }
//        }

//        public string ConvertToPdf(string xlFilePath, XlPaperSize paperSize)
//        {
//            string physicalPath = System.Web.HttpContext.Current.Server.MapPath(xlFilePath);
//            Application app = new Application();
//            Workbook wkb = app.Workbooks.Open(physicalPath);
//            try
//            {
//                string path;
//                if (GeneratePdf(xlFilePath, paperSize, wkb, out path)) return path;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                try
//                {
//                    if (wkb != null)
//                        wkb.Close(true);
//                    if (app != null)
//                    {
//                        if (app.Workbooks != null)
//                            app.Workbooks.Close();
//                        app.Quit();
//                    }

//                    Process[] pProcess;
//                    pProcess = Process.GetProcessesByName("Excel");
//                    foreach (var process in pProcess)
//                    {
//                        process.Kill();
//                    }
//                }
//                catch (Exception exception)
//                {


//                }

//            }
//            return "";
//        }

//        private static bool GeneratePdf(string xlFilePath, XlPaperSize paperSize, Workbook wkb, out string path)
//        {
//            foreach (Worksheet ws in wkb.Worksheets.OfType<Worksheet>())
//            {
//                ws.PageSetup.Orientation = XlPageOrientation.xlLandscape;
//                ws.PageSetup.Zoom = 100;
//                if (!paperSize.Equals(null))
//                {
//                    try
//                    {
//                        ws.PageSetup.PaperSize = paperSize;

//                    }
//                    catch
//                    {
//                    }
//                }

//                ws.PageSetup.Zoom = false;
//                ws.PageSetup.FitToPagesWide = 1;
//                ws.PageSetup.FitToPagesTall = false;
//            }

//            string pdfFilePthe = xlFilePath.Contains(".xls")
//                ? xlFilePath.Replace(".xls", ".pdf")
//                : xlFilePath.Replace(".xlsx", ".pdf");
//            string physicalPDfPath = System.Web.HttpContext.Current.Server.MapPath(pdfFilePthe);
//            wkb.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, physicalPDfPath);

//            path = pdfFilePthe;
//            return true;
            
//        }
//    }
//}