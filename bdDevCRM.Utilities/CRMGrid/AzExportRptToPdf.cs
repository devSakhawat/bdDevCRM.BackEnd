//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
//using Utilities.Model;
//using Utilities.StaticData;

//namespace Utilities
//{
//    public class AzExportRptToPdf
//    {
//        public static string ExportToPdf<T>(List<T> dataSource, string reportName, string exportPath)
//        {
//            bool isDevMode = false;
//            string reportRoot = "";
//            ReportDocument rd = new ReportDocument();
//            string rv = "";
//            try
//            {


//                try
//                {
//                    isDevMode = Convert.ToBoolean(ConfigurationManager.AppSettings["IsDevelopmentMode"]);

//                }
//                catch { }


//                string fullPath = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
//                if (isDevMode)
//                {
//                    string newPath = Path.GetFullPath(Path.Combine(fullPath, @"..\..\"));


//                    //get the folder that's in
//                    // string theDirectory = Path.GetDirectoryName(fullPath);
//                    reportRoot = Path.Combine(newPath, "Empress.Reports.Controllers\\Reports\\Rpt\\");

//                }
//                else
//                {
//                    string newPath = Path.GetFullPath(Path.Combine(fullPath, @"..\"));

//                    reportRoot = Path.Combine(newPath, "Reports//Rpt//");

//                }
//                var systemPath = AppDomain.CurrentDomain.BaseDirectory;

//                //exportPath = "D:\\Empress Application\\Empress Live\\Import\\HrDocument";
//                //exportPath = "D:\\Project\\Empress_v5_5_0 Intertek, Trust Bank, MBL & HRD\\Empress\\Import\\HrDocument";

//                string fileName = DateTime.Now.Ticks.ToString() + ".pdf";
//                string reportFullPath = Path.Combine(reportRoot, reportName);
//                string exportLocation = Path.Combine(exportPath, fileName);
//                rd.Load(reportFullPath);
//                rd.SetDataSource(dataSource);
//                rd.ExportToDisk(ExportFormatType.PortableDocFormat, exportLocation);
//                rv = fileName;
//            }
//            catch (Exception ex)
//            {

//                rv = ex.Message;
//                throw ex;
//            }
//            finally
//            {
//                rd.Close();
//                rd.Dispose();
//                GC.Collect();

//            }
//            return rv;
//        }

//        public static string ExportToPdfWithSubReports<T1, T2>(List<T1> dataSource, List<T2> subReportDataSource, string reportName, string exportPath, bool isSubReport)
//        {
//            bool isDevMode = false;
//            string reportRoot = "";
//            ReportDocument rd = new ReportDocument();
//            string rv = "";
//            try
//            {


//                try
//                {
//                    isDevMode = Convert.ToBoolean(ConfigurationManager.AppSettings["IsDevelopmentMode"]);

//                }
//                catch { }


//                string fullPath = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
//                if (isDevMode)
//                {
//                    string newPath = Path.GetFullPath(Path.Combine(fullPath, @"..\..\"));
//                    reportRoot = Path.Combine(newPath, "Empress.Reports.Controllers\\Reports\\Rpt\\");
//                }
//                else
//                {
//                    string newPath = Path.GetFullPath(Path.Combine(fullPath, @"..\"));
//                    reportRoot = Path.Combine(newPath, "Reports//Rpt//");

//                }

//                string fileName = DateTime.Now.Ticks.ToString() + ".pdf";
//                string reportFullPath = Path.Combine(reportRoot, reportName);
//                string exportLocation = Path.Combine(exportPath, fileName);
//                rd.Load(reportFullPath);
//                rd.SetDataSource(dataSource);
//                if (isSubReport)
//                {
//                    rd.Subreports[0].SetDataSource(subReportDataSource);
//                    // rd.Subreports[1].SetDataSource(subReportDataSource);
//                }
//                rd.ExportToDisk(ExportFormatType.PortableDocFormat, exportLocation);
//                rv = fileName;
//            }
//            catch (Exception ex)
//            {

//                rv = ex.Message;
//                throw ex;
//            }
//            return rv;
//        }


//        public static string ExportToPdfWithSubReportsForCanteenOrderSummary<T1, T2>(List<T1> dataSource, List<T2> subReportDataSource, string reportName, string exportPath, bool isSubReport)
//        {
//            bool isDevMode = false;
//            string reportRoot = "";
//            ReportDocument rd = new ReportDocument();
//            string rv = "";
//            try
//            {


//                try
//                {
//                    isDevMode = Convert.ToBoolean(ConfigurationManager.AppSettings["IsDevelopmentMode"]);

//                }
//                catch { }


//                string fullPath = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
//                if (isDevMode)
//                {
//                    string newPath = Path.GetFullPath(Path.Combine(fullPath, @"..\..\"));
//                    reportRoot = Path.Combine(newPath, "Empress.Reports.Controllers\\Reports\\Rpt\\");
//                }
//                else
//                {
//                    string newPath = Path.GetFullPath(Path.Combine(fullPath, @"..\"));
//                    reportRoot = Path.Combine(newPath, "Reports//Rpt//");

//                }

//                string fileName = DateTime.Now.Ticks.ToString() + ".pdf";
//                string reportFullPath = Path.Combine(reportRoot, reportName);
//                string exportLocation = Path.Combine(exportPath, fileName);
//                rd.Load(reportFullPath);
//                rd.SetDataSource(dataSource);
//                if (isSubReport)
//                {
//                    rd.Subreports[0].SetDataSource(subReportDataSource);
//                    // rd.Subreports[1].SetDataSource(subReportDataSource);
//                }
//                rd.ExportToDisk(ExportFormatType.PortableDocFormat, exportLocation);
//                rv = fileName;
//            }
//            catch (Exception ex)
//            {

//                rv = ex.Message;
//                throw ex;
//            }
//            return rv;
//        }

//        public static string ExportToPdfWithSubReportsForIncremnt<T1, T2>(List<T1> dataSource, List<T2> subReportDataSource, string reportName, string exportPath, bool isSubReport)
//        {
//            bool isDevMode = false;
//            string reportRoot = "";
//            ReportDocument rd = new ReportDocument();
//            string rv = "";
//            try
//            {


//                try
//                {
//                    isDevMode = Convert.ToBoolean(ConfigurationManager.AppSettings["IsDevelopmentMode"]);

//                }
//                catch { }


//                string fullPath = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
//                if (isDevMode)
//                {
//                    string newPath = Path.GetFullPath(Path.Combine(fullPath, @"..\..\"));
//                    reportRoot = Path.Combine(newPath, "Empress.Reports.Controllers\\Reports\\Rpt\\");
//                }
//                else
//                {
//                    string newPath = Path.GetFullPath(Path.Combine(fullPath, @"..\"));
//                    reportRoot = Path.Combine(newPath, "Reports//Rpt//");

//                }

//                string fileName = DateTime.Now.Ticks.ToString() + ".pdf";
//                string reportFullPath = Path.Combine(reportRoot, reportName);
//                string exportLocation = Path.Combine(exportPath, fileName);
//                rd.Load(reportFullPath);
//                rd.SetDataSource(dataSource);
//                if (isSubReport)
//                {
//                    rd.Subreports[0].SetDataSource(subReportDataSource);
//                    rd.Subreports[1].SetDataSource(subReportDataSource);
//                }
//                rd.ExportToDisk(ExportFormatType.PortableDocFormat, exportLocation);
//                rv = fileName;
//            }
//            catch (Exception ex)
//            {

//                rv = ex.Message;
//                throw ex;
//            }
//            return rv;
//        }
//        public string Export<T>(ReportFileParam<T> param, int ExportType)
//        {
//            bool isDevMode = false;

//            ReportDocument rd = new ReportDocument();
//            string rv = "";
//            try
//            {
//                string reportFullPath = Path.Combine(param.BaseFolderPath, param.ReportFileName);
//                string exportLocation = Path.Combine(param.ExportPath, param.ExportFileName);
//                rd.Load(reportFullPath);
//                rd.SetDataSource(param.DataSource);
//                switch (ExportType)
//                {
//                    case 1:
//                        {
//                            rd.ExportToDisk(ExportFormatType.WordForWindows, exportLocation);

//                            break;
//                        }
//                    case 2:
//                        {
//                            rd.ExportToDisk(ExportFormatType.Excel, exportLocation);

//                            break;
//                        }
//                    case 3:
//                        {
//                            rd.ExportToDisk(ExportFormatType.PortableDocFormat, exportLocation);

//                            break;
//                        }


//                }

//                rv = exportLocation;

//            }
//            catch (Exception ex)
//            {


//            }
//            return rv;
//        }
//        public Stream ExportStream<T>(ReportFileParam<T> param, List<string> rptParams)
//        {
//            bool isDevMode = false;

//            ReportDocument rd = new ReportDocument();
//            string rv = "";
//            try
//            {
//                string reportFullPath = Path.Combine(param.BaseFolderPath, param.ReportFileName);
//                // string exportLocation = Path.Combine(param.ExportPath, param.ExportFileName);
//                rd.Load(reportFullPath);
//                rd.SetDataSource(param.DataSource);


//                if (param.IsParamAllow)
//                {
//                    rd.SetParameterValue("@Company", rptParams[0]);

//                }
//                return rd.ExportToStream(ExportFormatType.PortableDocFormat);
//            }
//            catch (Exception ex)
//            {


//            }
//            return null;
//        }

//        public static string TomatoRpt(string reportName, dynamic dataSource)
//        {
//            try
//            {
//                ReportDocument doc = new ReportDocument();
//                doc.SetDataSource(dataSource);

//                string path = System.Web.HttpContext.Current.Server.MapPath("~/Empress.Reports.Controllers/Reports/Rpt/rptProbationPeriodExtensionLetter.rpt");
//                doc.Load(path);

//                ExportOptions CrExportOptions;
//                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
//                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
//                CrDiskFileDestinationOptions.DiskFileName = "D:\\SampleReport.pdf";
//                CrExportOptions = doc.ExportOptions;
//                {
//                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
//                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
//                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
//                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
//                }
//                doc.Export();
//                return CrDiskFileDestinationOptions.DiskFileName;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }


//        public static string ExportToPdfForJobTermination(dynamic dataSource, string reportName, string exportPath)  
//        { 
//            bool isDevMode = false;
//            string reportRoot = "";
//            ReportDocument rd = new ReportDocument();
            
//            string rv = "";
//            try
//            {


//                try
//                {
//                    isDevMode = Convert.ToBoolean(ConfigurationManager.AppSettings["IsDevelopmentMode"]);

//                }
//                catch { }


//                string fullPath = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
//                if (isDevMode)
//                {
//                    string newPath = Path.GetFullPath(Path.Combine(fullPath, @"..\..\"));


//                    //get the folder that's in
//                    // string theDirectory = Path.GetDirectoryName(fullPath);
//                    reportRoot = Path.Combine(newPath, "Empress.Reports.Controllers\\Reports\\Rpt\\");
//                    //reportRoot = Path.Combine(newPath, "Empress.Reports.Controllers\\Reports\\Rpt\\Performance\\");
//                }
//                else
//                {
//                    string newPath = Path.GetFullPath(Path.Combine(fullPath, @"..\"));

//                    reportRoot = Path.Combine(newPath, "Reports//Rpt//");

//                }
//                var systemPath = AppDomain.CurrentDomain.BaseDirectory;

//                //exportPath = "D:\\Empress Application\\Empress Live\\Import\\HrDocument";
//                //exportPath = "D:\\Project\\Empress_v5_5_0 Intertek, Trust Bank, MBL & HRD\\Empress\\Import\\HrDocument";

//                string fileName = DateTime.Now.Ticks.ToString() + ".pdf";
//                string reportFullPath = Path.Combine(reportRoot, reportName);
//                string exportLocation = Path.Combine(exportPath, fileName);
//                rd.Load(reportFullPath);
//                rd.SetDataSource(dataSource);
//                rd.ExportToDisk(ExportFormatType.PortableDocFormat, exportLocation);
//                rv = fileName;
//            }
//            catch (Exception ex)
//            {

//                rv = ex.Message;
//                throw ex; 
//            }
//            return rv;
//        }

//        public static string ExportToPdfForJobConfirmationandExtension(dynamic dataSource, string reportName, string exportPath, dynamic subReportSource1, dynamic subReportSource2, dynamic subReportSource3, int recommendationType)
//        {
//            bool isDevMode = false; 
//            string reportRoot = "";
//            ReportDocument rd = new ReportDocument();

//            string rv = "";
//            try
//            {


//                try
//                {
//                    isDevMode = Convert.ToBoolean(ConfigurationManager.AppSettings["IsDevelopmentMode"]);

//                }
//                catch { }


//                string fullPath = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
//                if (isDevMode)
//                {
//                    string newPath = Path.GetFullPath(Path.Combine(fullPath, @"..\..\"));


//                    //get the folder that's in
//                    // string theDirectory = Path.GetDirectoryName(fullPath);
//                    reportRoot = Path.Combine(newPath, "Empress.Reports.Controllers\\Reports\\Rpt\\");
//                    //reportRoot = Path.Combine(newPath, "Empress.Reports.Controllers\\Reports\\Rpt\\Performance\\");
//                }
//                else
//                {
//                    string newPath = Path.GetFullPath(Path.Combine(fullPath, @"..\"));

//                    reportRoot = Path.Combine(newPath, "Reports//Rpt//");

//                }
//                var systemPath = AppDomain.CurrentDomain.BaseDirectory;

//                //exportPath = "D:\\Empress Application\\Empress Live\\Import\\HrDocument";
//                //exportPath = "D:\\Project\\Empress_v5_5_0 Intertek, Trust Bank, MBL & HRD\\Empress\\Import\\HrDocument";

//                string fileName = DateTime.Now.Ticks.ToString() + ".pdf";
//                string reportFullPath = Path.Combine(reportRoot, reportName);
//                string exportLocation = Path.Combine(exportPath, fileName);
//                rd.Load(reportFullPath);
//                rd.SetDataSource(dataSource);
//                if (recommendationType==1)
//                {
//                    rd.Subreports["extensionSubReport"].SetDataSource(subReportSource1);

//                }
//                else
//                {
//                    rd.Subreports["rptSubConfirmationService"].SetDataSource(subReportSource2);
//                    rd.Subreports["rptSubConfirmationServicePromoted"].SetDataSource(subReportSource3);

//                }
//                rd.ExportToDisk(ExportFormatType.PortableDocFormat, exportLocation);
//                rv = fileName;
//            }
//            catch (Exception ex)
//            {

//                rv = ex.Message;
//                throw ex;
//            }
//            return rv;
//        }
         
//        public static string ExportToPdfForMultiTransfer(dynamic mainDataSource, dynamic employeeinfo, dynamic emailGroupList, string reportName, string exportPath, string upperHeader, string lowerHeader)
//        {
//            bool isDevMode = false;
//            string reportRoot = "";
//            ReportDocument rd = new ReportDocument();
//            string rv = "";
//            try
//            {
//                try
//                {
//                    isDevMode = Convert.ToBoolean(ConfigurationManager.AppSettings["IsDevelopmentMode"]);

//                }
//                catch { }

//                string fullPath = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
//                if (isDevMode)
//                {
//                    string newPath = Path.GetFullPath(Path.Combine(fullPath, @"..\..\"));

//                    //get the folder that's in
//                    // string theDirectory = Path.GetDirectoryName(fullPath);
//                    reportRoot = Path.Combine(newPath, "Empress.Reports.Controllers\\Reports\\Rpt\\");
//                }
//                else
//                {
//                    string newPath = Path.GetFullPath(Path.Combine(fullPath, @"..\"));

//                    reportRoot = Path.Combine(newPath, "Reports//Rpt//");

//                }
//                var systemPath = AppDomain.CurrentDomain.BaseDirectory;

//                //exportPath = "D:\\Empress Application\\Empress Live\\Import\\HrDocument";
//                //exportPath = "D:\\Project\\Empress_v5_5_0 Intertek, Trust Bank, MBL & HRD\\Empress\\Import\\HrDocument";

//                string fileName = DateTime.Now.Ticks.ToString() + ".pdf";
//                string reportFullPath = Path.Combine(reportRoot, reportName);
//                string exportLocation = Path.Combine(exportPath, fileName);
//                rd.Load(reportFullPath);
//                rd.SetDataSource(mainDataSource);
//                rd.Subreports["emailConfigToCopy"].SetDataSource(emailGroupList);
//                rd.Subreports["TransferOrderEmployee"].SetDataSource(employeeinfo);
//                rd.SetParameterValue("@UpperHeader", upperHeader);
//                rd.SetParameterValue("@LowerHeader", lowerHeader);

//                rd.ExportToDisk(ExportFormatType.PortableDocFormat, exportLocation);
//                rv = fileName;
//            }
//            catch (Exception ex)
//            {

//                rv = ex.Message;
//                throw ex;
//            }
//            return rv;
//        }

//        public static string ExportToPdfForJobConfirmationandExtension(dynamic dataSource, string reportName, string exportPath, dynamic subDatasource_BeforePromotion, dynamic subDatasource_AfterPromotion)
//        {
//            bool isDevMode = false;
//            string reportRoot = "";
//            ReportDocument rd = new ReportDocument();

//            string rv = "";
//            try
//            {


//                try
//                {
//                    isDevMode = Convert.ToBoolean(ConfigurationManager.AppSettings["IsDevelopmentMode"]);

//                }
//                catch { }


//                string fullPath = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
//                if (isDevMode)
//                {
//                    string newPath = Path.GetFullPath(Path.Combine(fullPath, @"..\..\"));


//                    //get the folder that's in
//                    // string theDirectory = Path.GetDirectoryName(fullPath);
//                    reportRoot = Path.Combine(newPath, "Empress.Reports.Controllers\\Reports\\Rpt\\");
//                    //reportRoot = Path.Combine(newPath, "Empress.Reports.Controllers\\Reports\\Rpt\\Performance\\");
//                }
//                else
//                {
//                    string newPath = Path.GetFullPath(Path.Combine(fullPath, @"..\"));

//                    reportRoot = Path.Combine(newPath, "Reports//Rpt//");

//                }
//                var systemPath = AppDomain.CurrentDomain.BaseDirectory;

//                //exportPath = "D:\\Empress Application\\Empress Live\\Import\\HrDocument";
//                //exportPath = "D:\\Project\\Empress_v5_5_0 Intertek, Trust Bank, MBL & HRD\\Empress\\Import\\HrDocument";

//                string fileName = DateTime.Now.Ticks.ToString() + ".pdf";
//                string reportFullPath = Path.Combine(reportRoot, reportName);
//                string exportLocation = Path.Combine(exportPath, fileName);
//                rd.Load(reportFullPath);
//                rd.SetDataSource(dataSource);

//                    rd.Subreports["PromotionSubReportForExistingSalary"].SetDataSource(subDatasource_BeforePromotion);
//                    rd.Subreports["PromotionSubReportForPromotedSalary"].SetDataSource(subDatasource_AfterPromotion);

                
//                rd.ExportToDisk(ExportFormatType.PortableDocFormat, exportLocation);
//                rv = fileName;
//            }
//            catch (Exception ex)
//            {

//                rv = ex.Message;
//                throw ex;
//            }
//            return rv;
//        }

//    }
//}
