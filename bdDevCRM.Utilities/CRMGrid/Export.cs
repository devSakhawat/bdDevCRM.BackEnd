//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Diagnostics;
//using System.Drawing;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//using Microsoft.Office.Interop.Excel;
//using Bold = DocumentFormat.OpenXml.Spreadsheet.Bold;
//using CellFormat = Microsoft.Office.Interop.Excel.CellFormat;
//using Color = System.Drawing.Color;
//using DataTable = System.Data.DataTable;
//using Picture = Microsoft.Office.Interop.Excel.Picture;
//using Sheets = Microsoft.Office.Interop.Excel.Sheets;
//using Workbook = Microsoft.Office.Interop.Excel.Workbook;
//using Worksheet = Microsoft.Office.Interop.Excel.Worksheet;
//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Spreadsheet;
//using System.Web.Configuration;

//namespace Utilities
//{
//    public class Export
//    {


//        DataSet ds = new DataSet();
//        private Application application;
//        private Workbooks workbooks;
//        private Workbook workbook;
//        private Sheets sheets;
//        private Worksheet worksheet;
//        private Range range;
//        private Image myImage;

//        private string strFileName = "";

//        public void ExportToExcel(string filePath, int rowcout, string companyName, string companyAddress, int columnScape, bool istotal, string headerText, SqlDataReader reader)
//        {
//            //   ds = receiveDS;
//            strFileName = filePath;
//            try
//            {
//                application = new Application();
//                application.Visible = false;//true;
//                //application.Visible = true;//true;
//                application.DisplayAlerts = false;

//                workbooks = application.Workbooks;
//                workbook = (Workbook)application.Workbooks.Add(1);
//                workbook.SaveAs(strFileName, XlFileFormat.xlWorkbookNormal, null, null, false, false, XlSaveAsAccessMode.xlExclusive, false, false, null, null, false);

//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //range = worksheet.Cells;

//                Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);
//                #region Main Header


//                var headerRange1 = XLsheet.Range[XLsheet.Cells[1, 1], XLsheet.Cells[rowcout - 2, 6]];
//                headerRange1.Font.Bold = true;
//                headerRange1.Font.Size = 16;
//                headerRange1.Merge(Type.Missing);
//                headerRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                string picPath = System.Web.HttpContext.Current.Server.MapPath("../Images/report-logo.png");
//                object missing = System.Reflection.Missing.Value;
//                Range picPosition = headerRange1;
//                Pictures p = XLsheet.Pictures(missing) as Microsoft.Office.Interop.Excel.Pictures;
//                Picture pic = null;
//                try
//                {
//                    pic = p.Insert(picPath, missing);
//                    pic.Left = Convert.ToDouble(picPosition.Left);
//                    pic.Top = picPosition.Top;
//                }
//                catch (Exception)
//                {
//                    picPath = System.Web.HttpContext.Current.Server.MapPath("../../Images/report-logo.png");
//                    p = XLsheet.Pictures(missing);
//                    pic = p.Insert(picPath, missing);
//                    pic.Left = Convert.ToDouble(picPosition.Left);
//                    pic.Top = picPosition.Top;
//                }

//                if (companyName != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 2, 7], XLsheet.Cells[rowcout - 3, 14]];
//                    headerRange2[1, 1] = companyName;
//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 12;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                }

//                var headerRange3 = XLsheet.Range[XLsheet.Cells[rowcout - 1, 1], XLsheet.Cells[rowcout - 1, 6]];
//                headerRange3[1, 1] = headerText;// "Salary Control Register for the Month of " + salaryMonth.ToString("MMMM-yyyy");//companyAddress;
//                headerRange3.Font.Bold = true;
//                headerRange3.Font.Size = 12;
//                headerRange3.Merge(Type.Missing);
//                headerRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

//                #endregion


//                int excelSheetRow = this.WriteDataToTheSpecifiedRange(range, rowcout, reader);

//                if (istotal)
//                {

//                    var colCount = ds.Tables[0].Columns.Count;
//                    var dataRowCount = ds.Tables[0].Rows.Count;
//                    var headerRangeTotal = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount + 1, 1], XLsheet.Cells[rowcout + dataRowCount + 1, columnScape]];
//                    headerRangeTotal[1, 1] = "Total";
//                    headerRangeTotal.Font.Bold = true;
//                    headerRangeTotal.Font.Size = 12;
//                    headerRangeTotal.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                    headerRangeTotal.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    headerRangeTotal.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    headerRangeTotal.Merge(Type.Missing);
//                    headerRangeTotal.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                    //int i = columnScape - 1;

//                    for (int i = 0; i < (colCount - columnScape) + 1; i++)
//                    {
//                        //var col=
//                        var rangeTotal = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1], XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1]];
//                        string startColumnLetter = ExcelColumnLetter(i + columnScape);
//                        string endColumnLetter = ExcelColumnLetter(i + columnScape);

//                        if (listCols[i].DataType == typeof(Decimal) || listCols[i].DataType == typeof(double) || listCols[i].DataType == typeof(int))
//                        {
//                            rangeTotal[1, 1] = "=SUM(" + startColumnLetter + "" + (rowcout + 1) + ":" + endColumnLetter + "" + (rowcout + dataRowCount) + ")";
//                        }

//                        rangeTotal[1, 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                        rangeTotal[1, 1].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                        rangeTotal[1, 1].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);

//                    }
//                }


//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //XLsheet.PageSetup.PrintTitleRows = "$1:$5";
//                //XLsheet.PageSetup.LeftFooter = "-------------------------------------\n &BPrepared By&B  \n " + "Rapid 2.0| Payroll | Print Date, Time && Page No : " + DateTime.Now.ToString("MMMM dd, yyyy") + " | " + DateTime.Now.ToString("h:mm tt") + " | " + "Page &P of &N";
//                //XLsheet.PageSetup.BottomMargin = application.InchesToPoints(1.5);
//                //XLsheet.PageSetup.CenterFooter = "-------------------------------------\n &BChecked By&B \n";
//                //XLsheet.PageSetup.RightFooter = "------------------\n &BApproved By&B \n";

//                XLsheet.Columns.AutoFit();

//                XLsheet.Name = ds.Tables[0].TableName;
//                workbook.Save();

//            }
//            catch (Exception exp)
//            {
//                throw exp;
//            }
//            finally
//            {
//                DisposeExcelObject();
//            }
//        }



//        public void ExportToExcelForRecruitment(string filePath, int rowcout, string companyName, string companyAddress, int columnScape, bool istotal, string headerText, SqlDataReader reader)
//        {
//            //   ds = receiveDS;
//            strFileName = filePath;
//            try
//            {
//                application = new Application();
//                application.Visible = false;//true;
//                application.DisplayAlerts = false;

//                workbooks = application.Workbooks;
//                workbook = (Workbook)application.Workbooks.Add(1);
//                workbook.SaveAs(strFileName, XlFileFormat.xlWorkbookNormal, null, null, false, false, XlSaveAsAccessMode.xlExclusive, false, false, null, null, false);

//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //range = worksheet.Cells;

//                Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);
//                #region Main Header


//                var headerRange1 = XLsheet.Range[XLsheet.Cells[1, 1], XLsheet.Cells[rowcout - 2, 6]];
//                headerRange1.Font.Bold = true;
//                headerRange1.Font.Size = 16;
//                headerRange1.Merge(Type.Missing);
//                headerRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                string picPath = System.Web.HttpContext.Current.Server.MapPath("../Images/report-logo.png");
//                object missing = System.Reflection.Missing.Value;
//                Range picPosition = headerRange1;
//                Pictures p = XLsheet.Pictures(missing) as Microsoft.Office.Interop.Excel.Pictures;
//                Picture pic = null;
//                try
//                {
//                    pic = p.Insert(picPath, missing);
//                    pic.Left = Convert.ToDouble(picPosition.Left);
//                    pic.Top = picPosition.Top;
//                }
//                catch (Exception)
//                {
//                    picPath = System.Web.HttpContext.Current.Server.MapPath("../../Images/report-logo.png");
//                    p = XLsheet.Pictures(missing);
//                    pic = p.Insert(picPath, missing);
//                    pic.Left = Convert.ToDouble(picPosition.Left);
//                    pic.Top = picPosition.Top;
//                }

//                //if (companyName != "")
//                //{
//                //    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 2, 7], XLsheet.Cells[rowcout - 3, 14]];
//                //    headerRange2[1, 1] = companyName;
//                //    headerRange2.Font.Bold = true;
//                //    headerRange2.Font.Size = 12;
//                //    headerRange2.Merge(Type.Missing);
//                //    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //}

//                var headerRange3 = XLsheet.Range[XLsheet.Cells[rowcout - 1, 1], XLsheet.Cells[rowcout - 1, 6]];
//                //  headerRange3[1, 1] = headerText;// "Salary Control Register for the Month of " + salaryMonth.ToString("MMMM-yyyy");//companyAddress;
//                //headerRange3.Font.Bold = true;
//                //headerRange3.Font.Size = 12;
//                //headerRange3.Merge(Type.Missing);
//                //headerRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

//                #endregion


//                int excelSheetRow = this.WriteDataToTheSpecifiedRange(range, rowcout, reader);

//                //if (istotal)
//                //{

//                //    var colCount = ds.Tables[0].Columns.Count;
//                //    var dataRowCount = ds.Tables[0].Rows.Count;
//                //    var headerRangeTotal = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount + 1, 1], XLsheet.Cells[rowcout + dataRowCount + 1, columnScape]];
//                //  //  headerRangeTotal[1, 1] = "Total";
//                //    headerRangeTotal.Font.Bold = true;
//                //    headerRangeTotal.Font.Size = 12;
//                //    headerRangeTotal.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                //    headerRangeTotal.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                //    headerRangeTotal.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                //    headerRangeTotal.Merge(Type.Missing);
//                //    headerRangeTotal.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //    //int i = columnScape - 1;

//                //    for (int i = 0; i < (colCount - columnScape) + 1; i++)
//                //    {
//                //        //var col=
//                //        var rangeTotal = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1], XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1]];
//                //        string startColumnLetter = ExcelColumnLetter(i + columnScape);
//                //        string endColumnLetter = ExcelColumnLetter(i + columnScape);

//                //        //if (listCols[i].DataType == typeof(Decimal) || listCols[i].DataType == typeof(double) || listCols[i].DataType == typeof(int))
//                //        //{
//                //        //    rangeTotal[1, 1] = "=SUM(" + startColumnLetter + "" + (rowcout + 1) + ":" + endColumnLetter + "" + (rowcout + dataRowCount) + ")";
//                //        //}

//                //        //rangeTotal[1, 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                //        //rangeTotal[1, 1].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                //        //rangeTotal[1, 1].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);

//                //    }
//                //}


//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //XLsheet.PageSetup.PrintTitleRows = "$1:$5";
//                //XLsheet.PageSetup.LeftFooter = "-------------------------------------\n &BPrepared By&B  \n " + "Rapid 2.0| Payroll | Print Date, Time && Page No : " + DateTime.Now.ToString("MMMM dd, yyyy") + " | " + DateTime.Now.ToString("h:mm tt") + " | " + "Page &P of &N";
//                //XLsheet.PageSetup.BottomMargin = application.InchesToPoints(1.5);
//                //XLsheet.PageSetup.CenterFooter = "-------------------------------------\n &BChecked By&B \n";
//                //XLsheet.PageSetup.RightFooter = "------------------\n &BApproved By&B \n";

//                XLsheet.Columns.AutoFit();

//                XLsheet.Name = ds.Tables[0].TableName;
//                workbook.Save();

//            }
//            catch (Exception exp)
//            {
//                throw exp;
//            }
//            finally
//            {
//                DisposeExcelObject();
//            }
//        }


//        public void ExportToExcelForBgOt(string filePath, int rowcout, string companyName, string companyAddress, int columnScape, bool istotal, string headerText, SqlDataReader reader)
//        {
//            //   ds = receiveDS;
//            strFileName = filePath;
//            try
//            {
//                application = new Application();
//                application.Visible = false;//true;
//                application.DisplayAlerts = false;

//                workbooks = application.Workbooks;
//                workbook = (Workbook)application.Workbooks.Add(1);
//                workbook.SaveAs(strFileName, XlFileFormat.xlWorkbookNormal, null, null, false, false, XlSaveAsAccessMode.xlExclusive, false, false, null, null, false);

//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //range = worksheet.Cells;

//                Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);
//                #region Main Header


//                //var headerRange1 = XLsheet.Range[XLsheet.Cells[1, 1], XLsheet.Cells[rowcout - 2, 6]];
//                //headerRange1.Font.Bold = true;
//                //headerRange1.Font.Size = 16;
//                //headerRange1.Merge(Type.Missing);
//                //headerRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //string picPath = System.Web.HttpContext.Current.Server.MapPath("../Images/report-logo.png");
//                //object missing = System.Reflection.Missing.Value;
//                //Range picPosition = headerRange1;
//                //Pictures p = XLsheet.Pictures(missing) as Microsoft.Office.Interop.Excel.Pictures;
//                //Picture pic = null;
//                //try
//                //{
//                //    pic = p.Insert(picPath, missing);
//                //    pic.Left = Convert.ToDouble(picPosition.Left);
//                //    pic.Top = picPosition.Top;
//                //}
//                //catch (Exception)
//                //{
//                //    picPath = System.Web.HttpContext.Current.Server.MapPath("../../Images/report-logo.png");
//                //    p = XLsheet.Pictures(missing);
//                //    pic = p.Insert(picPath, missing);
//                //    pic.Left = Convert.ToDouble(picPosition.Left);
//                //    pic.Top = picPosition.Top;
//                //}
//                Object[] values = new Object[reader.FieldCount];
//                var fieldCount = values.Count();
//                if (companyName != "")
//                {
//                    var headerRange1 = XLsheet.Range[XLsheet.Cells[rowcout - 2, 1], XLsheet.Cells[rowcout - 2, fieldCount + 1]];
//                    //var rg = range[range.Cells[excelSheetRow, 1], range.Cells[excelSheetRow, dt.Columns.Count]];
//                    headerRange1[1, 1] = companyName;

//                    headerRange1.Font.Bold = true;
//                    headerRange1.Font.Size = 14;
//                    headerRange1.Merge(Type.Missing);
//                    headerRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 1, 1], XLsheet.Cells[rowcout - 1, fieldCount + 1]];
//                    //var rg = range[range.Cells[excelSheetRow, 1], range.Cells[excelSheetRow, dt.Columns.Count]];
//                    headerRange2[1, 1] = companyAddress;
//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 12;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


//                    var headerRange3 = XLsheet.Range[XLsheet.Cells[rowcout, 1], XLsheet.Cells[rowcout, fieldCount + 1]];
//                    //var rg = range[range.Cells[excelSheetRow, 1], range.Cells[excelSheetRow, dt.Columns.Count]];
//                    headerRange3[1, 1] = headerText;
//                    headerRange3.Font.Bold = true;
//                    headerRange3.Font.Size = 12;
//                    headerRange3.Merge(Type.Missing);
//                    headerRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                    rowcout++;


//                }



//                #endregion


//                int excelSheetRow = this.WriteDataToTheSpecifiedRange(range, rowcout, reader);

//                if (istotal)
//                {

//                    var colCount = ds.Tables[0].Columns.Count;
//                    var dataRowCount = ds.Tables[0].Rows.Count;
//                    var headerRangeTotal = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount + 1, 1], XLsheet.Cells[rowcout + dataRowCount + 1, columnScape]];
//                    headerRangeTotal[1, 1] = "Total";
//                    headerRangeTotal.Font.Bold = true;
//                    headerRangeTotal.Font.Size = 12;
//                    headerRangeTotal.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                    headerRangeTotal.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    headerRangeTotal.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    headerRangeTotal.Merge(Type.Missing);
//                    headerRangeTotal.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                    //int i = columnScape - 1;

//                    for (int i = 0; i < (colCount - columnScape) - 1; i++)
//                    {
//                        //var col=
//                        var rangeTotal = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1], XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1]];
//                        string startColumnLetter = ExcelColumnLetter(i + columnScape);
//                        string endColumnLetter = ExcelColumnLetter(i + columnScape);

//                        if (listCols[i].DataType == typeof(Decimal) || listCols[i].DataType == typeof(double) || listCols[i].DataType == typeof(int))
//                        {
//                            rangeTotal[1, 1] = "=SUM(" + startColumnLetter + "" + (rowcout + 1) + ":" + endColumnLetter + "" + (rowcout + dataRowCount) + ")";
//                        }

//                        rangeTotal[1, 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                        rangeTotal[1, 1].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                        rangeTotal[1, 1].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);

//                    }
//                }


//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //XLsheet.PageSetup.PrintTitleRows = "$1:$5";
//                //XLsheet.PageSetup.LeftFooter = "-------------------------------------\n &BPrepared By&B  \n " + "Rapid 2.0| Payroll | Print Date, Time && Page No : " + DateTime.Now.ToString("MMMM dd, yyyy") + " | " + DateTime.Now.ToString("h:mm tt") + " | " + "Page &P of &N";
//                //XLsheet.PageSetup.BottomMargin = application.InchesToPoints(1.5);
//                //XLsheet.PageSetup.CenterFooter = "-------------------------------------\n &BChecked By&B \n";
//                //XLsheet.PageSetup.RightFooter = "------------------\n &BApproved By&B \n";

//                XLsheet.Columns.AutoFit();

//                XLsheet.Name = ds.Tables[0].TableName;
//                workbook.Save();

//            }
//            catch (Exception exp)
//            {
//                throw exp;
//            }
//            finally
//            {
//                DisposeExcelObject();
//            }
//        }
//        public void ExportToExcelPerformance(string filePath, int rowcout, string companyName, string montherCompany, int columnScape, bool istotal, string headerText, SqlDataReader reader, string location, string appperiod, int reportType, string companyShortNameList, int payrollEffect)
//        {
//            //   ds = receiveDS;
//            strFileName = filePath;
//            try
//            {
//                application = new Application();
//                application.Visible = false;//true;
//                application.DisplayAlerts = false;

//                workbooks = application.Workbooks;
//                workbook = (Workbook)application.Workbooks.Add(1);
//                workbook.SaveAs(strFileName, XlFileFormat.xlWorkbookNormal, null, null, false, false, XlSaveAsAccessMode.xlExclusive, false, false, null, null, false);

//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //range = worksheet.Cells;

//                Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);
//                #region Main Header


//                var headerRange1 = XLsheet.Range[XLsheet.Cells[1, 1], XLsheet.Cells[rowcout - 2, 6]];
//                headerRange1.Font.Bold = true;
//                headerRange1.Font.Size = 16;
//                headerRange1.Merge(Type.Missing);
//                headerRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                string picPath = System.Web.HttpContext.Current.Server.MapPath("../Images/report-logo.png");
//                object missing = System.Reflection.Missing.Value;
//                Range picPosition = headerRange1;
//                Pictures p = XLsheet.Pictures(missing) as Microsoft.Office.Interop.Excel.Pictures;
//                Picture pic = null;
//                try
//                {
//                    pic = p.Insert(picPath, missing);
//                    pic.Left = Convert.ToDouble(picPosition.Left);
//                    pic.Top = picPosition.Top;
//                }
//                catch (Exception)
//                {
//                    picPath = System.Web.HttpContext.Current.Server.MapPath("../../Images/report-logo.png");
//                    p = XLsheet.Pictures(missing);
//                    pic = p.Insert(picPath, missing);
//                    pic.Left = Convert.ToDouble(picPosition.Left);
//                    pic.Top = picPosition.Top;
//                }

//                if (companyName != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 5, 8], XLsheet.Cells[rowcout - 5, 25]];
//                    var headerRange3 = XLsheet.Range[XLsheet.Cells[rowcout - 4, 8], XLsheet.Cells[rowcout - 4, 25]];

//                    headerRange2[1, 1] = companyName;

//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 12;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;



//                    headerRange3[1, 1] = "Sector-A";
//                    headerRange3.Font.Bold = true;
//                    headerRange3.Font.Size = 12;
//                    headerRange3.Merge(Type.Missing);
//                    headerRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

//                }


//                if (appperiod != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 3, 8], XLsheet.Cells[rowcout - 3, 25]];
//                    headerRange2[1, 1] = appperiod;
//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 12;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                }

//                if (headerText != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 2, 8], XLsheet.Cells[rowcout - 2, 25]];
//                    headerRange2[1, 1] = headerText + " ( " + companyShortNameList + " )";
//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 12;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                }



//                #endregion

//                int excelSheetRow = 0;
//                //int excelSheetRow = this.WriteDataToTheSpecifiedRange(range, rowcout, reader);
//                if (reportType == 3)
//                {
//                    excelSheetRow = this.WriteDataToTheSpecifiedRange(range, 5, reader);
//                }
//                else
//                {
//                    excelSheetRow = this.WriteDataToTheSpecifiedRangeforAppraisal(range, rowcout, reader, reportType, payrollEffect);

//                }



//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

//                XLsheet.Columns.AutoFit();

//                XLsheet.Name = ds.Tables[0].TableName;
//                workbook.Save();

//            }
//            catch (Exception exp)
//            {
//                throw exp;
//            }
//            finally
//            {
//                DisposeExcelObject();
//            }
//        }


//        public void ExportToExcelInterviewerAssesment(string filePath, int rowcout, string companyName, string montherCompany, int columnScape, bool istotal, string headerText, SqlDataReader reader, string location, string appperiod, int reportType, string companyShortNameList)
//        {
//            //   ds = receiveDS;
//            strFileName = filePath;
//            try
//            {
//                application = new Application();
//                application.Visible = false;//true;
//                application.DisplayAlerts = false;

//                workbooks = application.Workbooks;
//                workbook = (Workbook)application.Workbooks.Add(1);
//                workbook.SaveAs(strFileName, XlFileFormat.xlWorkbookNormal, null, null, false, false, XlSaveAsAccessMode.xlExclusive, false, false, null, null, false);

//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //range = worksheet.Cells;

//                Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);
//                #region Main Header


//                var headerRange1 = XLsheet.Range[XLsheet.Cells[1, 1], XLsheet.Cells[rowcout - 2, 6]];
//                headerRange1.Font.Bold = true;
//                headerRange1.Font.Size = 16;
//                headerRange1.Merge(Type.Missing);
//                headerRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                string picPath = System.Web.HttpContext.Current.Server.MapPath("../Images/report-logo.png");
//                object missing = System.Reflection.Missing.Value;
//                Range picPosition = headerRange1;
//                Pictures p = XLsheet.Pictures(missing) as Microsoft.Office.Interop.Excel.Pictures;
//                Picture pic = null;
//                try
//                {
//                    pic = p.Insert(picPath, missing);
//                    pic.Left = Convert.ToDouble(picPosition.Left);
//                    pic.Top = picPosition.Top;
//                }
//                catch (Exception)
//                {
//                    picPath = System.Web.HttpContext.Current.Server.MapPath("../../Images/report-logo.png");
//                    p = XLsheet.Pictures(missing);
//                    pic = p.Insert(picPath, missing);
//                    pic.Left = Convert.ToDouble(picPosition.Left);
//                    pic.Top = picPosition.Top;
//                }

//                if (companyName != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 5, 8], XLsheet.Cells[rowcout - 5, 25]];
//                    var headerRange3 = XLsheet.Range[XLsheet.Cells[rowcout - 4, 8], XLsheet.Cells[rowcout - 4, 25]];

//                    headerRange2[1, 1] = companyName;

//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 12;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;



//                    headerRange3[1, 1] = "Sector-A";
//                    headerRange3.Font.Bold = true;
//                    headerRange3.Font.Size = 12;
//                    headerRange3.Merge(Type.Missing);
//                    headerRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

//                }


//                if (appperiod != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 3, 8], XLsheet.Cells[rowcout - 3, 25]];
//                    headerRange2[1, 1] = appperiod;
//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 12;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                }

//                if (headerText != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 2, 8], XLsheet.Cells[rowcout - 2, 25]];
//                    headerRange2[1, 1] = headerText + " ( " + companyShortNameList + " )";
//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 12;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                }



//                #endregion

//                int excelSheetRow = 0;
//                //int excelSheetRow = this.WriteDataToTheSpecifiedRange(range, rowcout, reader);
//                if (reportType == 3|| reportType == 6)
//                {
//                    excelSheetRow = this.WriteDataToTheSpecifiedRange(range, 5, reader);
//                }
//                else
//                {
//                    excelSheetRow = this.WriteDataToTheSpecifiedRangeforAppraisal(range, rowcout, reader, reportType, 0);

//                }



//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

//                XLsheet.Columns.AutoFit();

//                XLsheet.Name = ds.Tables[0].TableName;
//                workbook.Save();

//            }
//            catch (Exception exp)
//            {
//                throw exp;
//            }
//            finally
//            {
//                DisposeExcelObject();
//            }
//        }

//        List<DataColumn> listCols = new List<DataColumn>();
//        private int WriteDataToTheSpecifiedRange(Range range, int rowount, SqlDataReader reader)
//        {
//            int iRow;
//            int iCol;
//            //Traverse through the DataTable columns to write the headers on the first row of the excel sheet.
//            DataTable dt = new DataTable();
//            DataTable dtOriginal = new DataTable();
//            DataTable dtSchema = reader.GetSchemaTable();
//            if (dtSchema != null)
//            {
//                foreach (DataRow drow in dtSchema.Rows)
//                {
//                    string columnName = Convert.ToString(drow["ColumnName"]);
//                    var column = new DataColumn(columnName, (Type)(drow["DataType"]));
//                    column.Unique = (bool)drow["IsUnique"];
//                    column.AllowDBNull = (bool)drow["AllowDBNull"];
//                    column.AutoIncrement = (bool)drow["IsAutoIncrement"];
//                    listCols.Add(column);
//                    dt.Columns.Add(column);
//                }

//                dtOriginal = dt.Clone();


//                #region Column Hiding
//                try
//                {
//                    dt.Columns.Remove("SortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("EmploymentDate");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("DSortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("IsClosed");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("EmployeeTypeName");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("EmpNoOfCashCtc");
//                }
//                catch (Exception)
//                {

//                }
//                try
//                {
//                    dt.Columns.Remove("EmpTypeSortOrder");
//                }
//                catch (Exception)
//                {

//                }
//                #endregion
//            }
//            for (iCol = 0; iCol < dt.Columns.Count; iCol++)
//            {

//                if (iCol == 1 || dt.Columns[iCol].ColumnName == "BankAccountNo" || dt.Columns[iCol].ColumnName == "Account No" || dt.Columns[iCol].ColumnName == "AccountNo" || dt.Columns[iCol].ColumnName == "Routing Number" || dt.Columns[iCol].ColumnName == "RoutingNumber")
//                {
//                    range[rowount, (iCol + 1)].NumberFormat = "@";
//                }
//                range[rowount, (iCol + 1)] = iCol < 4 ? Regex.Replace(dt.Columns[iCol].ColumnName, "([a-z])_?([A-Z])", "$1 $2") : dt.Columns[iCol].ColumnName;
//                range.WrapText = false;
//                range.Font.Bold = true;
//                range[rowount, (iCol + 1)].Font.Size = 10;
//                range[rowount, (iCol + 1)].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                range[rowount, (iCol + 1)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                range[rowount, (iCol + 1)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                if (iCol > 3)
//                {
//                    range[rowount, (iCol + 1)].Style.WrapText = true;
//                }
//                else
//                {
//                    range[rowount, (iCol + 1)].Style.WrapText = false;
//                }
//            }


//            // Call Read before accessing data. 
//            while (reader.Read())
//            {
//                DataRow dataRow = dtOriginal.NewRow();
//                for (int i = 0; i < listCols.Count; i++)
//                {
//                    dataRow[(listCols[i].ColumnName)] = reader[i];
//                }
//                //dt.Rows.Add(dataRow); //Previous
//                dtOriginal.Rows.Add(dataRow);
//            }


//            #region Column Hiding
//            if (dtOriginal.Rows.Count > 0)
//            {
//                try
//                {
//                    dtOriginal.Columns.Remove("SortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("EmploymentDate");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("DSortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("IsClosed");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("EmployeeTypeName");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("EmpNoOfCashCtc");
//                }
//                catch (Exception)
//                {

//                }
//                try
//                {
//                    dtOriginal.Columns.Remove("EmpTypeSortOrder");
//                }
//                catch (Exception)
//                {

//                }
//            }
//            #endregion

//            //ds.Tables.Add(dt); //Previous
//            ds.Tables.Add(dtOriginal);

//            //Traverse through the rows and columns of the datatable to write the data in the sheet.
//            int excelSheetRow = rowount + 1;
//            int sl = 1;

//            #region Report Sum or Grand Total Dynamic

//            //int num = 3;
//            //int rdata = -1; 
//            int num = 5;
//            int rdata = 0;

//            #endregion

//            if (dtOriginal.Rows.Count > 0)
//            {
//                #region Row Binding
//                foreach (DataRow row in dtOriginal.Rows)
//                {
//                    // var newRow = new Row();
//                    iCol = 0;
//                    foreach (DataColumn col in dtOriginal.Columns)
//                    {
//                        //if (iCol == 0)
//                        //{
//                        //    range[(excelSheetRow), (iCol + 1)] = sl;
//                        //}
//                        if (col.ToString() == "Cash" || col.ToString() == "Net Payout")
//                        {
//                            try
//                            {
//                                if (Convert.ToInt32(row[col]) < 0)
//                                {
//                                    range[(excelSheetRow), (iCol + 1)] = "(" + row[col].ToString() + ")";
//                                }
//                                else
//                                {
//                                    range[(excelSheetRow), (iCol + 1)] = row[col].ToString();
//                                }
//                            }
//                            catch (Exception)
//                            {

//                                range[(excelSheetRow), (iCol + 1)] = row[col].ToString();
//                            }

//                        }
//                        else if (col.ToString() == "BankAccountNo" || col.ToString() == "Account No" || col.ToString() == "AccountNo" || col.ToString() == "EmployeeId" || col.ToString() == "Routing Number" || col.ToString() == "RoutingNumber")
//                        {
//                            range[(excelSheetRow), (iCol + 1)].NumberFormat = "@";
//                            range[(excelSheetRow), (iCol + 1)] = row[col].ToString();
//                        }

//                        else if (col.ToString() == "Bank Account No" || col.ToString() == "Account No" || col.ToString() == "Name of Employees" || col.ToString() == "Routing Number" || col.ToString() == "RoutingNumber")
//                        {
//                            range[(excelSheetRow), (iCol + 1)].NumberFormat = "@";
//                            range[(excelSheetRow), (iCol + 1)] = row[col].ToString();
//                        }
//                        else if (col.ToString() == "Joining Date" || col.ToString() == "Date of Confirmation" || col.ToString() == "Last Increment Effective Date" || col.ToString() == "Last Enhancement Effective date" || col.ToString() == "Last Promotion Effective date" || col.ToString() == "End of Appraisal Period" || col.ToString() == "Effective from")
//                        {
//                            range[(excelSheetRow), (iCol + 1)].NumberFormat = "dd-MMM-yyyy";
//                            range[(excelSheetRow), (iCol + 1)] = row[col].ToString();
//                        }
//                        else
//                        {
//                            range[(excelSheetRow), (iCol + 1)] = row[col].ToString();
//                        }

//                        iCol++;
//                    }
//                    excelSheetRow++;
//                    sl++;
//                }

//                #endregion

//                #region Add SubTotaal Text

//                var rgsub = range[excelSheetRow, 1];
//                //var rgsub = range[excelSheetRow - 1, 3];
//                //var rgsub = XLsheet.Range[XLsheet.Cells[rowcout - 2, 8], XLsheet.Cells[rowcout - 2, 15]];



//                range[excelSheetRow, 1] = "Total Increase in Percentage = ";
//                range[excelSheetRow, 1].Interior.Color =
//                   System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CCFFCC"));
//                #endregion

//                #region Add Summation

//                for (int i = num; i <= dt.Columns.Count - rdata; i++)
//                {
//                    var colcount = dt.Columns;

//                    string startColumnLetter = ExcelColumnLetter(i - 1);
//                    string endColumnLetter = ExcelColumnLetter(i - 1);
//                    var headerRangeParentCostCenterSum = range[excelSheetRow, i];
//                    headerRangeParentCostCenterSum[(1), (1)] = "=SUM(" + startColumnLetter + "" +
//                                                               (excelSheetRow - dtOriginal.Rows.Count) + ":" +
//                                                               endColumnLetter + "" +
//                                                               (excelSheetRow - 1) + ")";

//                    headerRangeParentCostCenterSum[(1), (1)].Interior.Color =
//                        System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CCFFCC"));
//                }

//                rowount++;
//                excelSheetRow++;

//                #endregion
//            }



//            return excelSheetRow;
//        }


//        private int WriteDataToTheSpecifiedRangeRecruitment(Range range, int rowount, SqlDataReader reader)
//        {
//            int iRow;
//            int iCol;
//            //Traverse through the DataTable columns to write the headers on the first row of the excel sheet.
//            DataTable dt = new DataTable();
//            DataTable dtOriginal = new DataTable();
//            DataTable dtSchema = reader.GetSchemaTable();
//            if (dtSchema != null)
//            {
//                foreach (DataRow drow in dtSchema.Rows)
//                {
//                    string columnName = Convert.ToString(drow["ColumnName"]);
//                    var column = new DataColumn(columnName, (Type)(drow["DataType"]));
//                    column.Unique = (bool)drow["IsUnique"];
//                    column.AllowDBNull = (bool)drow["AllowDBNull"];
//                    column.AutoIncrement = (bool)drow["IsAutoIncrement"];
//                    listCols.Add(column);
//                    dt.Columns.Add(column);
//                }

//                dtOriginal = dt.Clone();


//                #region Column Hiding
//                try
//                {
//                    dt.Columns.Remove("SortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("EmploymentDate");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("DSortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("IsClosed");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("EmployeeTypeName");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("EmpNoOfCashCtc");
//                }
//                catch (Exception)
//                {

//                }
//                try
//                {
//                    dt.Columns.Remove("EmpTypeSortOrder");
//                }
//                catch (Exception)
//                {

//                }
//                #endregion
//            }
//            for (iCol = 0; iCol < dt.Columns.Count; iCol++)
//            {

//                if (iCol == 1 || dt.Columns[iCol].ColumnName == "BankAccountNo" || dt.Columns[iCol].ColumnName == "Account No" || dt.Columns[iCol].ColumnName == "AccountNo" || dt.Columns[iCol].ColumnName == "Routing Number" || dt.Columns[iCol].ColumnName == "RoutingNumber")
//                {
//                    range[rowount, (iCol + 1)].NumberFormat = "@";
//                }
//                range[rowount, (iCol + 1)] = iCol < 4 ? Regex.Replace(dt.Columns[iCol].ColumnName, "([a-z])_?([A-Z])", "$1 $2") : dt.Columns[iCol].ColumnName;
//                range.WrapText = false;
//                range.Font.Bold = true;
//                range[rowount, (iCol + 1)].Font.Size = 10;
//                range[rowount, (iCol + 1)].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                range[rowount, (iCol + 1)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                range[rowount, (iCol + 1)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                if (iCol > 3)
//                {
//                    range[rowount, (iCol + 1)].Style.WrapText = true;
//                }
//                else
//                {
//                    range[rowount, (iCol + 1)].Style.WrapText = false;
//                }
//            }


//            // Call Read before accessing data. 
//            while (reader.Read())
//            {
//                DataRow dataRow = dtOriginal.NewRow();
//                for (int i = 0; i < listCols.Count; i++)
//                {
//                    dataRow[(listCols[i].ColumnName)] = reader[i];
//                }
//                //dt.Rows.Add(dataRow); //Previous
//                dtOriginal.Rows.Add(dataRow);
//            }


//            #region Column Hiding
//            if (dtOriginal.Rows.Count > 0)
//            {
//                try
//                {
//                    dtOriginal.Columns.Remove("SortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("EmploymentDate");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("DSortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("IsClosed");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("EmployeeTypeName");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("EmpNoOfCashCtc");
//                }
//                catch (Exception)
//                {

//                }
//                try
//                {
//                    dtOriginal.Columns.Remove("EmpTypeSortOrder");
//                }
//                catch (Exception)
//                {

//                }
//            }
//            #endregion

//            //ds.Tables.Add(dt); //Previous
//            ds.Tables.Add(dtOriginal);

//            //Traverse through the rows and columns of the datatable to write the data in the sheet.
//            int excelSheetRow = rowount + 1;
//            int sl = 1;

//            #region Report Sum or Grand Total Dynamic

//            //int num = 3;
//            //int rdata = -1; 
//            int num = 5;
//            int rdata = 0;

//            #endregion

//            if (dtOriginal.Rows.Count > 0)
//            {
//                #region Row Binding
//                foreach (DataRow row in dtOriginal.Rows)
//                {
//                    // var newRow = new Row();
//                    iCol = 0;
//                    foreach (DataColumn col in dtOriginal.Columns)
//                    {
//                        //if (iCol == 0)
//                        //{
//                        //    range[(excelSheetRow), (iCol + 1)] = sl;
//                        //}
//                        if (col.ToString() == "Cash" || col.ToString() == "Net Payout")
//                        {
//                            try
//                            {
//                                if (Convert.ToInt32(row[col]) < 0)
//                                {
//                                    range[(excelSheetRow), (iCol + 1)] = "(" + row[col].ToString() + ")";
//                                }
//                                else
//                                {
//                                    range[(excelSheetRow), (iCol + 1)] = row[col].ToString();
//                                }
//                            }
//                            catch (Exception)
//                            {

//                                range[(excelSheetRow), (iCol + 1)] = row[col].ToString();
//                            }

//                        }
//                        else if (col.ToString() == "BankAccountNo" || col.ToString() == "Account No" || col.ToString() == "AccountNo" || col.ToString() == "EmployeeId" || col.ToString() == "Routing Number" || col.ToString() == "RoutingNumber")
//                        {
//                            range[(excelSheetRow), (iCol + 1)].NumberFormat = "@";
//                            range[(excelSheetRow), (iCol + 1)] = row[col].ToString();
//                        }

//                        else if (col.ToString() == "Bank Account No" || col.ToString() == "Account No" || col.ToString() == "Name of Employees" || col.ToString() == "Routing Number" || col.ToString() == "RoutingNumber")
//                        {
//                            range[(excelSheetRow), (iCol + 1)].NumberFormat = "@";
//                            range[(excelSheetRow), (iCol + 1)] = row[col].ToString();
//                        }
//                        else if (col.ToString() == "Joining Date" || col.ToString() == "Date of Confirmation" || col.ToString() == "Last Increment Effective Date" || col.ToString() == "Last Enhancement Effective date" || col.ToString() == "Last Promotion Effective date" || col.ToString() == "End of Appraisal Period" || col.ToString() == "Effective from")
//                        {
//                            range[(excelSheetRow), (iCol + 1)].NumberFormat = "dd-MMM-yyyy";
//                            range[(excelSheetRow), (iCol + 1)] = row[col].ToString();
//                        }
//                        else
//                        {
//                            range[(excelSheetRow), (iCol + 1)] = row[col].ToString();
//                        }

//                        iCol++;
//                    }
//                    excelSheetRow++;
//                    sl++;
//                }

//                #endregion

//                #region Add SubTotaal Text

//                var rgsub = range[excelSheetRow, 1];
//                //var rgsub = range[excelSheetRow - 1, 3];
//                //var rgsub = XLsheet.Range[XLsheet.Cells[rowcout - 2, 8], XLsheet.Cells[rowcout - 2, 15]];



//                //  range[excelSheetRow, 1] = "Total Increase in Percentage = ";
//                //range[excelSheetRow, 1].Interior.Color =
//                //   System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CCFFCC"));
//                #endregion

//                //#region Add Summation

//                //for (int i = num; i <= dt.Columns.Count - rdata; i++)
//                //{
//                //    var colcount = dt.Columns;

//                //    string startColumnLetter = ExcelColumnLetter(i - 1);
//                //    string endColumnLetter = ExcelColumnLetter(i - 1);
//                //    var headerRangeParentCostCenterSum = range[excelSheetRow, i];
//                //    headerRangeParentCostCenterSum[(1), (1)] = "=SUM(" + startColumnLetter + "" +
//                //                                               (excelSheetRow - dtOriginal.Rows.Count) + ":" +
//                //                                               endColumnLetter + "" +
//                //                                               (excelSheetRow - 1) + ")";

//                //    headerRangeParentCostCenterSum[(1), (1)].Interior.Color =
//                //        System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CCFFCC"));
//                //}

//                //rowount++;
//                //excelSheetRow++;

//                //#endregion
//            }



//            return excelSheetRow;
//        }

//        private int WriteDataToTheSpecifiedRangeforAppraisal(Range range, int rowount, SqlDataReader reader, int reportType, int payrollEffect)
//        {
//            int iRow;
//            int iCol;
//            //Traverse through the DataTable columns to write the headers on the first row of the excel sheet.
//            DataTable dt = new DataTable();
//            DataTable dtOriginal = new DataTable();
//            DataTable dtSchema = reader.GetSchemaTable();
//            if (dtSchema != null)
//            {
//                foreach (DataRow drow in dtSchema.Rows)
//                {
//                    string columnName = Convert.ToString(drow["ColumnName"]);
//                    var column = new DataColumn(columnName, (Type)(drow["DataType"]));
//                    column.Unique = (bool)drow["IsUnique"];
//                    column.AllowDBNull = (bool)drow["AllowDBNull"];
//                    column.AutoIncrement = (bool)drow["IsAutoIncrement"];
//                    listCols.Add(column);
//                    dt.Columns.Add(column);
//                }

//                dtOriginal = dt.Clone();

//                #region Column Hiding
//                try
//                {
//                    dt.Columns.Remove("SortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("EmploymentDate");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("DSortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("IsClosed");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("EmployeeTypeName");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("EmpNoOfCashCtc");
//                }
//                catch (Exception)
//                {

//                }
//                try
//                {
//                    dt.Columns.Remove("EmpTypeSortOrder");
//                }
//                catch (Exception)
//                {

//                }
//                #endregion
//            }
//            for (iCol = 0; iCol < dt.Columns.Count; iCol++)
//            {
//                if (iCol == 0)
//                {
//                    range[rowount, (iCol + 1)] = "SL";
//                    range.WrapText = true;
//                    range.Font.Bold = true;
//                    range[rowount, (iCol + 1)].Font.Size = 10;
//                    range[rowount, (iCol + 1)].ColumnWidth = 2;
//                    range[rowount, (iCol + 1)].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#fff2cc"));
//                    range[rowount, (iCol + 1)].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                    range[rowount, (iCol + 1)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    range[rowount, (iCol + 1)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                }
//                if (iCol == 1 || dt.Columns[iCol].ColumnName == "BankAccountNo" || dt.Columns[iCol].ColumnName == "Routing Number")
//                {
//                    range[rowount, (iCol + 2)].NumberFormat = "@";
//                }
//                range[rowount, (iCol + 2)] = iCol < 4 ? Regex.Replace(dt.Columns[iCol].ColumnName, "([a-z])_?([A-Z])", "$1 $2") : dt.Columns[iCol].ColumnName;
//                range.WrapText = false;
//                range.Font.Bold = true;
//                range[rowount, (iCol + 2)].Font.Size = 10;
//                range[rowount, (iCol + 2)].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#fff2cc"));
//                range[rowount, (iCol + 2)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                range[rowount, (iCol + 2)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                if (iCol > 3)
//                {
//                    range[rowount, (iCol + 2)].Style.WrapText = false;
//                    range[rowount, (iCol + 2)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    range[rowount, (iCol + 2)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                }
//                else
//                {
//                    range[rowount, (iCol + 2)].Style.WrapText = false;
//                    range[rowount, (iCol + 2)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    range[rowount, (iCol + 2)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                }
//            }


//            // Call Read before accessing data. 
//            while (reader.Read())
//            {
//                DataRow dataRow = dtOriginal.NewRow();
//                for (int i = 0; i < listCols.Count; i++)
//                {
//                    dataRow[(listCols[i].ColumnName)] = reader[i];
//                }
//                //dt.Rows.Add(dataRow); //Previous
//                dtOriginal.Rows.Add(dataRow);
//            }


//            #region Column Hiding
//            if (dtOriginal.Rows.Count > 0)
//            {
//                try
//                {
//                    dtOriginal.Columns.Remove("SortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("EmploymentDate");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("DSortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("IsClosed");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("EmployeeTypeName");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("EmpNoOfCashCtc");
//                }
//                catch (Exception)
//                {

//                }
//                try
//                {
//                    dtOriginal.Columns.Remove("EmpTypeSortOrder");
//                }
//                catch (Exception)
//                {

//                }
//            }
//            #endregion

//            //ds.Tables.Add(dt); //Previous
//            ds.Tables.Add(dtOriginal);

//            //Traverse through the rows and columns of the datatable to write the data in the sheet.
//            int excelSheetRow = rowount + 1;
//            int sl = 1;
//            var departmnentName = "";

//            #region Report Sum or Grand Total Dynamic

//            int num = 0;
//            int rdata = 0;

//            if (reportType == 0)
//            {
//                num = 12;
//                rdata = 4;
//            }
//            else if (reportType == 1)
//            {
//                num = 12;
//                rdata = 4;
//            }
//            else if (reportType == 2)
//            {
//                num = 12;
//                rdata = 4;

//            }
//            else if (reportType == 3)
//            {
//                num = 1;
//                rdata = 3;
//            }
//            else if (reportType == 4)
//            {
//                num = 12;
//                rdata = 4;
//            }
//            else if (reportType == 6)
//            {
//                num = 14;
//                rdata = 3;
//            }
//            else
//            {
//                num = 15;
//                rdata = 2;
//            }

//            #endregion

//            var dp = (from r in dtOriginal.AsEnumerable()
//                      select r["Department"]).Distinct().ToList();

//            var listGrandTotalCount = new List<GrandTotalCount>();

//            for (int k = 0; k < dp.Count; k++)
//            {
//                var gr = new GrandTotalCount();

//                #region Department Column

//                if (departmnentName == "" || departmnentName != dp[k].ToString())
//                {
//                    var rg = range[excelSheetRow, 1];
//                    range[excelSheetRow, 1] = dp[k].ToString();
//                    range[excelSheetRow, 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#eeeeee"));
//                    rowount++;
//                    excelSheetRow++;
//                }
//                #endregion

//                var dataTableByDep = from row1 in dtOriginal.AsEnumerable()
//                                     where
//                                         row1.Field<string>("Department") == dp[k].ToString()
//                                     select row1;

//                var datadep = dataTableByDep.AsDataView().ToTable();

//                if (datadep.Rows.Count > 0)
//                {
//                    #region Row Binding

//                    for (iRow = 0; iRow < datadep.Rows.Count; iRow++)
//                    {
//                        for (iCol = 0; iCol < datadep.Columns.Count; iCol++)
//                        {
//                            if (iCol == 0)
//                            {
//                                range[(excelSheetRow), (iCol + 1)] = sl;
//                            }
//                            if (datadep.Columns[iCol].ToString() == "Cash" || datadep.Columns.ToString() == "Net Payout")
//                            {
//                                try
//                                {
//                                    if (Convert.ToInt32(datadep.Rows[iRow][iCol]) < 0)
//                                    {
//                                        range[(excelSheetRow), (iCol + 2)] = "(" + datadep.Rows[iRow][iCol].ToString() + ")";
//                                    }
//                                    else
//                                    {
//                                        range[(excelSheetRow), (iCol + 2)] = datadep.Rows[iRow][iCol].ToString();
//                                    }
//                                }
//                                catch (Exception)
//                                {

//                                    range[(excelSheetRow), (iCol + 2)] = datadep.Rows[iRow][iCol].ToString();
//                                }

//                            }
//                            else if (datadep.Columns[iCol].ToString() == "BankAccountNo" || datadep.Columns[iCol].ToString() == "Employee ID" ||
//                                     datadep.Columns[iCol].ToString() == "Routing Number")
//                            {
//                                range[(excelSheetRow), (iCol + 2)].NumberFormat = "@";
//                                range[(excelSheetRow), (iCol + 2)] = datadep.Rows[iRow][iCol].ToString();
//                            }

//                            else if (datadep.Columns[iCol].ToString() == "Bank Account No" || datadep.Columns[iCol].ToString() == "Name of Employees" ||
//                                    datadep.Columns[iCol].ToString() == "Routing Number")
//                            {
//                                range[(excelSheetRow), (iCol + 2)].NumberFormat = "@";
//                                range[(excelSheetRow), (iCol + 2)] = datadep.Rows[iRow][iCol].ToString();
//                            }
//                            else if (datadep.Columns[iCol].ToString() == "Joining Date" || datadep.Columns[iCol].ToString() == "Date of Confirmation" ||
//                                     datadep.Columns[iCol].ToString() == "Last Increment Effective Date" ||
//                                    datadep.Columns[iCol].ToString() == "Last Enhancement Effective date" ||
//                                    datadep.Columns[iCol].ToString() == "Last Promotion Effective date" ||
//                                    datadep.Columns[iCol].ToString() == "End of Appraisal Period" ||
//                                    datadep.Columns[iCol].ToString() == "Effective from")
//                            {
//                                range[(excelSheetRow), (iCol + 2)].NumberFormat = "dd-MMM-yyyy";
//                                range[(excelSheetRow), (iCol + 2)] = datadep.Rows[iRow][iCol].ToString();
//                            }
//                            else
//                            {
//                                range[(excelSheetRow), (iCol + 2)] = datadep.Rows[iRow][iCol].ToString();
//                            }

//                            //iCol++;


//                        }
//                        excelSheetRow++;
//                        sl++;
//                    }

//                    #endregion

//                    #region Add SubTotaal Text

//                    var rgsub = range[excelSheetRow, 3];
//                    range[excelSheetRow, 1] = "Sub Total";
//                    range[excelSheetRow, 1].Interior.Color =
//                       System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#eeeeee"));
//                    #endregion

//                    #region Add Summation

//                    if (reportType == 6)
//                    {

//                        for (int i = num; i <= dt.Columns.Count - rdata; i++)
//                        {
//                            var colcount = dt.Columns;

//                            string startColumnLetter = ExcelColumnLetter(i - 1);
//                            string endColumnLetter = ExcelColumnLetter(i - 1);
//                            var headerRangeParentCostCenterSum = range[excelSheetRow, i];

//                            int[] antiSumColumns = { 15,17,18,19,20 };

//                            if (!antiSumColumns.Contains(i))
//                            {
//                                headerRangeParentCostCenterSum[(1), (1)] = "=SUM(" + startColumnLetter + "" +
//                                                           (excelSheetRow - dataTableByDep.Count()) + ":" +
//                                                           endColumnLetter + "" +
//                                                           (excelSheetRow - 1) + ")";
//                            }
                            

                           

//                            headerRangeParentCostCenterSum[(1), (1)].Interior.Color =
//                                System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CCFFCC"));
//                        }
//                        gr.ColumnNo = excelSheetRow;
//                        listGrandTotalCount.Add(gr);

//                        rowount++;
//                        excelSheetRow++;
//                    }
//                    else
//                    {

//                        for (int i = num; i <= dt.Columns.Count - rdata; i++)
//                        {
//                            var colcount = dt.Columns;

//                            string startColumnLetter = ExcelColumnLetter(i - 1);
//                            string endColumnLetter = ExcelColumnLetter(i - 1);
//                            var headerRangeParentCostCenterSum = range[excelSheetRow, i];

//                            if (i == 39 && reportType == 0)
//                            {
//                                string startColumnLetter1 = ExcelColumnLetter(i - 2);
//                                string endColumnLetter1 = ExcelColumnLetter(i - 2);
//                                string startColumnLetter2 = ExcelColumnLetter(26 - 1);
//                                string endColumnLetter2 = ExcelColumnLetter(26 - 1);

//                                headerRangeParentCostCenterSum[(1), (1)] = "=ROUND(SUM(" + startColumnLetter1 + "" +
//                                               (excelSheetRow - dataTableByDep.Count()) + ":" +
//                                               endColumnLetter1 + "" +
//                                               (excelSheetRow - 1) + ")" + "/" + "SUM(" + startColumnLetter2 + "" +
//                                               (excelSheetRow - dataTableByDep.Count()) + ":" +
//                                               endColumnLetter2 + "" +
//                                               (excelSheetRow - 1) + ")" + "*100,1)";
//                            }
//                            else if (i == 38 && reportType == 1)
//                            {
//                                string startColumnLetter1 = ExcelColumnLetter(i - 2);
//                                string endColumnLetter1 = ExcelColumnLetter(i - 2);
//                                string startColumnLetter2 = ExcelColumnLetter(30 - 5);
//                                string endColumnLetter2 = ExcelColumnLetter(30 - 5);

//                                headerRangeParentCostCenterSum[(1), (1)] = "=ROUND(SUM(" + startColumnLetter1 + "" +
//                                               (excelSheetRow - dataTableByDep.Count()) + ":" +
//                                               endColumnLetter1 + "" +
//                                               (excelSheetRow - 1) + ")" + "/" + "SUM(" + startColumnLetter2 + "" +
//                                               (excelSheetRow - dataTableByDep.Count()) + ":" +
//                                               endColumnLetter2 + "" +
//                                               (excelSheetRow - 1) + ")" + "*100,1)";
//                            }
//                            else if (i == 36 && reportType == 2)
//                            {
//                                string startColumnLetter1 = ExcelColumnLetter(i - 2);
//                                string endColumnLetter1 = ExcelColumnLetter(i - 2);
//                                string startColumnLetter2 = ExcelColumnLetter(29 - 5);
//                                string endColumnLetter2 = ExcelColumnLetter(29 - 5);

//                                headerRangeParentCostCenterSum[(1), (1)] = "=ROUND(SUM(" + startColumnLetter1 + "" +
//                                               (excelSheetRow - dataTableByDep.Count()) + ":" +
//                                               endColumnLetter1 + "" +
//                                               (excelSheetRow - 1) + ")" + "/" + "SUM(" + startColumnLetter2 + "" +
//                                               (excelSheetRow - dataTableByDep.Count()) + ":" +
//                                               endColumnLetter2 + "" +
//                                               (excelSheetRow - 1) + ")" + "*100,1)";
//                            }
//                            else if (i == 39 && reportType == 4)
//                            {
//                                string startColumnLetter1 = ExcelColumnLetter(i - 2);
//                                string endColumnLetter1 = ExcelColumnLetter(i - 2);
//                                string startColumnLetter2 = ExcelColumnLetter(31 - 6);
//                                string endColumnLetter2 = ExcelColumnLetter(31 - 6);

//                                headerRangeParentCostCenterSum[(1), (1)] = "=ROUND(SUM(" + startColumnLetter1 + "" +
//                                               (excelSheetRow - dataTableByDep.Count()) + ":" +
//                                               endColumnLetter1 + "" +
//                                               (excelSheetRow - 1) + ")" + "/" + "SUM(" + startColumnLetter2 + "" +
//                                               (excelSheetRow - dataTableByDep.Count()) + ":" +
//                                               endColumnLetter2 + "" +
//                                               (excelSheetRow - 1) + ")" + "*100,1)";
//                            }

//                            else
//                            {
//                                if (payrollEffect == 0)
//                                {
//                                    int[] antiSumColumns = { 13, 15, 16, 17, 18, 28, 31, 40 };
//                                    //int[] antiSumColumnsForPromotion = { 12, 14, 15, 16, 17, 29, 27 };
//                                    int[] antiSumColumnsForPromotion = { 13, 15, 16, 17, 18, 30, 28 };
//                                    //int[] antiSumColumnsForEnhancement = { 11, 13, 14, 15, 16, 24, };
//                                    int[] antiSumColumnsForEnhancement = { 13, 15, 16, 17, 18, 37, 26 };
//                                    //int[] antiSumColumnsForInitialSummary = { 11, 13, 14, 15, 16, 26, 29 };
//                                    int[] antiSumColumnsForInitialSummary = { 13, 15, 16, 17, 18, 28, 31 };

//                                    if ((!antiSumColumns.Contains(i) && reportType == 0) || (!antiSumColumnsForPromotion.Contains(i) && reportType == 1) ||
//                                        (!antiSumColumnsForEnhancement.Contains(i) && reportType == 2) ||
//                                        (!antiSumColumnsForInitialSummary.Contains(i) && reportType == 4))
//                                    {
//                                        headerRangeParentCostCenterSum[(1), (1)] = "=SUM(" + startColumnLetter + "" +
//                                                      (excelSheetRow - dataTableByDep.Count()) + ":" +
//                                                      endColumnLetter + "" +
//                                                      (excelSheetRow - 1) + ")";
//                                    }


//                                }
//                                else if (payrollEffect == 1)
//                                {
//                                    int[] antiSumColumns = { 13, 15, 16, 17, 18, 28, 31, 40 };
//                                    //int[] antiSumColumnsForEnhancement = { 11, 13, 14, 15, 16, 24, };
//                                    int[] antiSumColumnsForEnhancement = { 13, 15, 16, 17, 18, 37, 26 };
//                                    //int[] antiSumColumnsForPromotion = { 12, 14, 15, 16, 17, 29, 27 };
//                                    int[] antiSumColumnsForPromotion = { 13, 15, 16, 17, 18, 30, 28 };
//                                    //int[] antiSumColumnsForInitialSummary = { 11, 13, 14, 15, 16, 26, 29 };
//                                    int[] antiSumColumnsForInitialSummary = { 13, 15, 16, 17, 18, 28, 31 };
//                                    if ((!antiSumColumns.Contains(i) && reportType == 0) || (!antiSumColumnsForPromotion.Contains(i) && reportType == 1) || (!antiSumColumnsForEnhancement.Contains(i) && reportType == 2) ||
//                                        (!antiSumColumnsForInitialSummary.Contains(i) && reportType == 4))
//                                    {
//                                        headerRangeParentCostCenterSum[(1), (1)] = "=SUM(" + startColumnLetter + "" +
//                                                      (excelSheetRow - dataTableByDep.Count()) + ":" +
//                                                      endColumnLetter + "" +
//                                                      (excelSheetRow - 1) + ")";
//                                    }


//                                }



//                            }


//                            headerRangeParentCostCenterSum[(1), (1)].Interior.Color =
//                                System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#eeeeee"));
//                        }

//                        gr.ColumnNo = excelSheetRow;
//                        listGrandTotalCount.Add(gr);

//                        rowount++;
//                        excelSheetRow++;
//                    }
//                    #endregion
//                }
//            }

//            #region Add Grade Total Text

//            var rggrand = range[excelSheetRow, 3];
//            range[excelSheetRow, 1] = "Grand Total";
//            range.Font.Bold = true;
//            range[excelSheetRow, 1].Interior.Color =
//               System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#eeeeee"));

//            #endregion

//            #region Grade Total Summation

//            if(reportType == 6){

//                int[] antiSumColumns = { 15, 17, 18, 19, 20 };

               

//                 for (int i = 14; i <= dt.Columns.Count-3; i++)
//            {

//                if (!antiSumColumns.Contains(i))
//                {
//                    var colcount = dt.Columns;
//                    string startColumnLetter = ExcelColumnLetter(i - 1);
//                    string column = "";
//                    foreach (var grandTotalCount in listGrandTotalCount)
//                    {

//                        column += column == ""
//                          ? startColumnLetter + "" + grandTotalCount.ColumnNo
//                          : "+" + startColumnLetter + "" + grandTotalCount.ColumnNo;



//                    }
//                    if (column != "")
//                    {
//                        column = "=SUM(" + column + ")";
//                    }
//                    var headerRangeParentCostCenterSum = range[excelSheetRow, i];
//                    headerRangeParentCostCenterSum[(1), (1)] = column;
//                    headerRangeParentCostCenterSum[(1), (1)].Interior.Color =
//                       System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#eeeeee"));

//                }

              
               
//            }
//            }else{

//                for (int i = num; i <= dt.Columns.Count - rdata; i++)
//                {
//                    var colcount = dt.Columns;
//                    string startColumnLetter = ExcelColumnLetter(i - 1);
//                    string column = "";
//                    string column1 = "";
//                    string column2 = "";

//                    if (i == 39 && reportType == 0)
//                    {
//                        string startColumnLetter1 = ExcelColumnLetter(i - 2);
//                        string startColumnLetter2 = ExcelColumnLetter(26 - 1);

//                        foreach (var grandTotalCount in listGrandTotalCount)
//                        {
//                            column1 += column1 == "" ? startColumnLetter1 + "" + grandTotalCount.ColumnNo : "+" + startColumnLetter1 + "" + grandTotalCount.ColumnNo;
//                        }

//                        foreach (var grandTotalCount in listGrandTotalCount)
//                        {
//                            column2 += column2 == "" ? startColumnLetter2 + "" + grandTotalCount.ColumnNo : "+" + startColumnLetter2 + "" + grandTotalCount.ColumnNo;
//                        }

//                        if (column1 != "" && column2 != "")
//                        {
//                            column = "=ROUND(SUM(" + column1 + ")" + "/" + "SUM(" + column2 + ")*100,1)";
//                        }
//                    }
//                    else if (i == 38 && reportType == 1)
//                    {
//                        string startColumnLetter1 = ExcelColumnLetter(i - 2);
//                        string startColumnLetter2 = ExcelColumnLetter(30 - 5);

//                        foreach (var grandTotalCount in listGrandTotalCount)
//                        {
//                            column1 += column1 == "" ? startColumnLetter1 + "" + grandTotalCount.ColumnNo : "+" + startColumnLetter1 + "" + grandTotalCount.ColumnNo;
//                        }

//                        foreach (var grandTotalCount in listGrandTotalCount)
//                        {
//                            column2 += column2 == "" ? startColumnLetter2 + "" + grandTotalCount.ColumnNo : "+" + startColumnLetter2 + "" + grandTotalCount.ColumnNo;
//                        }

//                        if (column1 != "" && column2 != "")
//                        {
//                            column = "=ROUND(SUM(" + column1 + ")" + "/" + "SUM(" + column2 + ")*100,1)";
//                        }
//                    }
//                    else if (i == 36 && reportType == 2)
//                    {
//                        string startColumnLetter1 = ExcelColumnLetter(i - 2);
//                        string startColumnLetter2 = ExcelColumnLetter(29 - 5);

//                        foreach (var grandTotalCount in listGrandTotalCount)
//                        {
//                            column1 += column1 == "" ? startColumnLetter1 + "" + grandTotalCount.ColumnNo : "+" + startColumnLetter1 + "" + grandTotalCount.ColumnNo;
//                        }

//                        foreach (var grandTotalCount in listGrandTotalCount)
//                        {
//                            column2 += column2 == "" ? startColumnLetter2 + "" + grandTotalCount.ColumnNo : "+" + startColumnLetter2 + "" + grandTotalCount.ColumnNo;
//                        }

//                        if (column1 != "" && column2 != "")
//                        {
//                            column = "=ROUND(SUM(" + column1 + ")" + "/" + "SUM(" + column2 + ")*100,1)";
//                        }
//                    }
//                    else if (i == 39 && reportType == 4)
//                    {
//                        string startColumnLetter1 = ExcelColumnLetter(i - 2);
//                        string startColumnLetter2 = ExcelColumnLetter(31 - 6);

//                        foreach (var grandTotalCount in listGrandTotalCount)
//                        {
//                            column1 += column1 == "" ? startColumnLetter1 + "" + grandTotalCount.ColumnNo : "+" + startColumnLetter1 + "" + grandTotalCount.ColumnNo;
//                        }

//                        foreach (var grandTotalCount in listGrandTotalCount)
//                        {
//                            column2 += column2 == "" ? startColumnLetter2 + "" + grandTotalCount.ColumnNo : "+" + startColumnLetter2 + "" + grandTotalCount.ColumnNo;
//                        }

//                        if (column1 != "" && column2 != "")
//                        {
//                            column = "=ROUND(SUM(" + column1 + ")" + "/" + "SUM(" + column2 + ")*100,1)";
//                        }
//                    }
//                    else
//                    {
//                        int[] antiSumColumns = { 13, 15, 16, 17, 18, 28, 31, 40 };

//                        //int[] antiSumColumnsForPromotion = { 12, 14, 15, 16, 17, 29, 27 };
//                        int[] antiSumColumnsForPromotion = { 13, 15, 16, 17, 18, 30, 28 };
//                        //int[] antiSumColumnsForEnhancement = { 11, 13, 14, 15, 16, 24 };
//                        int[] antiSumColumnsForEnhancement = { 13, 15, 16, 17, 18, 37, 26 };

//                        //int[] antiSumColumnsForInitialSummary = { 11, 13, 14, 15, 16, 26, 29 };
//                        int[] antiSumColumnsForInitialSummary = { 13, 15, 16, 17, 18, 28, 31 };

//                        if ((!antiSumColumns.Contains(i) && reportType == 0 && payrollEffect == 0) || (!antiSumColumnsForPromotion.Contains(i) && reportType == 1 && payrollEffect == 0) ||
//                            (!antiSumColumnsForEnhancement.Contains(i) && reportType == 2 && payrollEffect == 0) ||
//                            (!antiSumColumnsForInitialSummary.Contains(i) && reportType == 4 && payrollEffect == 0))
//                        {
//                            foreach (var grandTotalCount in listGrandTotalCount)
//                            {
//                                column += column == "" ? startColumnLetter + "" + grandTotalCount.ColumnNo : "+" + startColumnLetter + "" + grandTotalCount.ColumnNo;
//                            }
//                            if (column != "")
//                            {
//                                column = "=SUM(" + column + ")";
//                            }
//                        }

//                        else if ((!antiSumColumns.Contains(i) && reportType == 0 && payrollEffect == 1) || (!antiSumColumnsForPromotion.Contains(i) && reportType == 1 && payrollEffect == 1) || (!antiSumColumnsForEnhancement.Contains(i) && reportType == 2 && payrollEffect == 1) || (!antiSumColumnsForInitialSummary.Contains(i) && reportType == 4 && payrollEffect == 1))
//                        {
//                            foreach (var grandTotalCount in listGrandTotalCount)
//                            {
//                                column += column == "" ? startColumnLetter + "" + grandTotalCount.ColumnNo : "+" + startColumnLetter + "" + grandTotalCount.ColumnNo;
//                            }
//                            if (column != "")
//                            {
//                                column = "=SUM(" + column + ")";
//                            }
//                        }



//                    }

//                    var headerRangeParentCostCenterSum = range[excelSheetRow, i];
//                    headerRangeParentCostCenterSum[(1), (1)] = column;
//                    headerRangeParentCostCenterSum[(1), (1)].Interior.Color =
//                       System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#eeeeee"));
//                }
//            }

//            #endregion


//            return excelSheetRow;
//        }

//        private static void ExportToExcel(DataTable resultsData)
//        {
//            const string fileName = @"D:\MyExcel.xls";

//            File.Delete(fileName);

//            uint sheetId = 1; //Start at the first sheet in the Excel workbook.

//            //This is the first time of creating the excel file and the first sheet.
//            // Create a spreadsheet document by supplying the filepath.
//            // By default, AutoSave = true, Editable = true, and Type = xlsx.
//            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook);

//            // Add a WorkbookPart to the document.
//            WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
//            workbookpart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();


//            // Add a WorksheetPart to the WorkbookPart.
//            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
//            var sheetData = new SheetData();
//            worksheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet(sheetData);



//            var bold1 = new Bold();
//            //  CellFormat cf = new CellFormat();

//            // Add Sheets to the Workbook.
//            DocumentFormat.OpenXml.Spreadsheet.Sheets sheets; sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>(new DocumentFormat.OpenXml.Spreadsheet.Sheets());

//            // Append a new worksheet and associate it with the workbook.
//            var sheet = new Sheet()
//            {
//                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
//                SheetId = sheetId,
//                Name = "Sheet" + sheetId
//            };
//            sheets.Append(sheet);



//            //Add Header Row.
//            var headerRow = new Row();
//            //headerRow.
//            foreach (DataColumn column in resultsData.Columns)
//            {
//                var cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(column.ColumnName) };
//                headerRow.AppendChild(cell);

//            }
//            sheetData.AppendChild(headerRow);

//            foreach (DataRow row in resultsData.Rows)
//            {
//                var newRow = new Row();
//                foreach (DataColumn col in resultsData.Columns)
//                {
//                    var cell = new Cell
//                    {
//                        DataType = CellValues.String,
//                        CellValue = new CellValue(row[col].ToString())
//                    };
//                    newRow.AppendChild(cell);
//                }

//                sheetData.AppendChild(newRow);
//            }
//            workbookpart.Workbook.Save();

//            spreadsheetDocument.Close();

//        }


//        public void DisposeExcelObject()
//        {
//            try
//            {



//                if (workbook != null)
//                    workbook.Close(true);
//                if (application != null)
//                {
//                    if (application.Workbooks != null)
//                        application.Workbooks.Close();
//                    application.Quit();
//                }
//                if (range != null)
//                    System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
//                if (workbook != null)
//                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
//                if (workbooks != null)
//                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbooks);
//                if (sheets != null)
//                    System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
//                if (sheets != null)
//                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
//                if (application != null)
//                    System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
//                range = null;
//                workbook = null;
//                workbooks = null;
//                sheets = null;
//                worksheet = null;
//                application = null;
//                Process[] pProcess;
//                pProcess = Process.GetProcessesByName("Excel");
//                foreach (var process in pProcess)
//                {
//                    process.Kill();
//                }
//            }
//            catch (Exception ex)
//            {

//            }
//        }

//        public string ExcelColumnLetter(int intCol)
//        {
//            string strColumn;
//            char letter1, letter2;
//            int intFirstLetter = ((intCol) / 26);
//            int intSecondLetter = (intCol % 26);
//            intFirstLetter = intFirstLetter + 64;
//            intSecondLetter = intSecondLetter + 65;

//            if (intFirstLetter > 64)
//            {
//                letter1 = (char)intFirstLetter;
//            }
//            else
//                letter1 = ' ';
//            letter2 = (char)intSecondLetter;
//            strColumn = string.Concat(letter1, letter2);
//            return strColumn.Trim();
//        }

//        public void ExportToExcelWithoutHeader(string filePath, int rowcout, string companyName, string companyAddress, int columnScape, bool istotal, string headerText, SqlDataReader reader, int assemblyId)
//        {
//            //   ds = receiveDS;
//            strFileName = filePath;
//            try
//            {
//                DataTable dtOriginalFinal = new DataTable();
//                application = new Application();
//                application.Visible = false;//true;
//                application.DisplayAlerts = false;

//                workbooks = application.Workbooks;
//                workbook = (Workbook)application.Workbooks.Add(1);
//                workbook.SaveAs(strFileName, XlFileFormat.xlWorkbookNormal, null, null, false, false, XlSaveAsAccessMode.xlExclusive, false, false, null, null, false);

//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //range = worksheet.Cells;

//                Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);
//                #region Main Header


//                //var headerRange1 = XLsheet.Range[XLsheet.Cells[1, 1], XLsheet.Cells[rowcout - 2, 6]];
//                //headerRange1.Font.Bold = true;
//                //headerRange1.Font.Size = 16;
//                //headerRange1.Merge(Type.Missing);
//                //headerRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //string picPath = System.Web.HttpContext.Current.Server.MapPath("../Images/report-logo.png");
//                //object missing = System.Reflection.Missing.Value;
//                //Range picPosition = headerRange1;
//                //Pictures p = XLsheet.Pictures(missing) as Microsoft.Office.Interop.Excel.Pictures;
//                //Picture pic = null;
//                //try
//                //{
//                //    pic = p.Insert(picPath, missing);
//                //    pic.Left = Convert.ToDouble(picPosition.Left);
//                //    pic.Top = picPosition.Top;
//                //}
//                //catch (Exception)
//                //{
//                //    picPath = System.Web.HttpContext.Current.Server.MapPath("../../Images/report-logo.png");
//                //    p = XLsheet.Pictures(missing);
//                //    pic = p.Insert(picPath, missing);
//                //    pic.Left = Convert.ToDouble(picPosition.Left);
//                //    pic.Top = picPosition.Top;
//                //}

//                if (assemblyId == 12)
//                {
//                    if (companyName != "")
//                    {
//                        var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 4, 1], XLsheet.Cells[rowcout - 4, 5]];
//                        headerRange2[1, 1] = companyName;
//                        headerRange2.Font.Bold = true;
//                        headerRange2.Font.Size = 16;
//                        headerRange2.Merge(Type.Missing);
//                        headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                    }
//                    if (companyAddress != "")
//                    {
//                        var headerRange3 = XLsheet.Range[XLsheet.Cells[rowcout - 3, 1], XLsheet.Cells[rowcout - 3, 5]];
//                        headerRange3[1, 1] = companyAddress;// 
//                        headerRange3.Font.Bold = true;
//                        headerRange3.Font.Size = 10;
//                        headerRange3.Merge(Type.Missing);
//                        headerRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                    }
//                    if (headerText != "")
//                    {
//                        var headerRange4 = XLsheet.Range[XLsheet.Cells[rowcout - 2, 1], XLsheet.Cells[rowcout - 2, 5]];
//                        headerRange4[1, 1] = headerText;// "Salary Control Register for the Month of " + salaryMonth.ToString("MMMM-yyyy");//companyAddress;
//                        headerRange4.Font.Bold = true;
//                        headerRange4.Font.Size = 10;
//                        headerRange4.Merge(Type.Missing);
//                        headerRange4.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                    }


//                }
//                //if (companyName != "")
//                //{
//                //    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 2, 7], XLsheet.Cells[rowcout - 3, 14]];
//                //    headerRange2[1, 1] = companyName;
//                //    headerRange2.Font.Bold = true;
//                //    headerRange2.Font.Size = 12;
//                //    headerRange2.Merge(Type.Missing);
//                //    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //}

//                //var headerRange3 = XLsheet.Range[XLsheet.Cells[rowcout - 1, 1], XLsheet.Cells[rowcout - 1, 6]];
//                //headerRange3[1, 1] = headerText;// "Salary Control Register for the Month of " + salaryMonth.ToString("MMMM-yyyy");//companyAddress;
//                //headerRange3.Font.Bold = true;
//                //headerRange3.Font.Size = 12;
//                //headerRange3.Merge(Type.Missing);
//                //headerRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

//                #endregion
//                int excelSheetRow1 = this.WriteDataToTheSpecifiedRangeForBankInstructionExcel(range, rowcout, reader, ref dtOriginalFinal);
//                // int excelSheetRow = this.WriteDataToTheSpecifiedRange(range, rowcout, reader);

//                if (istotal)
//                {

//                    var colCount = ds.Tables[0].Columns.Count;
//                    var dataRowCount = ds.Tables[0].Rows.Count;
//                    var headerRangeTotal = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount + 1, 1], XLsheet.Cells[rowcout + dataRowCount + 1, columnScape]];
//                    headerRangeTotal[1, 1] = "Total";
//                    headerRangeTotal.Font.Bold = true;
//                    headerRangeTotal.Font.Size = 12;
//                    headerRangeTotal.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                    headerRangeTotal.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    headerRangeTotal.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    headerRangeTotal.Merge(Type.Missing);
//                    headerRangeTotal.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                    //int i = columnScape - 1;

//                    for (int i = 0; i < (colCount - columnScape) + 1; i++)
//                    {
//                        //var col=
//                        var rangeTotal = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1], XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1]];
//                        string startColumnLetter = ExcelColumnLetter(i + columnScape);
//                        string endColumnLetter = ExcelColumnLetter(i + columnScape);


//                        if (listCols[i].DataType == typeof(Decimal) || listCols[i].DataType == typeof(double) || listCols[i].DataType == typeof(int))
//                        {
//                            rangeTotal[1, 1] = "=SUM(" + startColumnLetter + "" + (rowcout + 1) + ":" + endColumnLetter + "" + (rowcout + dataRowCount) + ")";

//                        }

//                        rangeTotal[1, 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                        rangeTotal[1, 1].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                        rangeTotal[1, 1].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                        //totalAmount = Convert.ToInt32(rangeTotal);
//                        //var colCount2 = ds.Tables[0].Columns.Count;
//                        //var dataRowCount2 = ds.Tables[0].Rows.Count;
//                        //var headerRangeTotal2 = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount2 + 2, 1], XLsheet.Cells[rowcout + dataRowCount2 + 2, 5]];
//                        //headerRangeTotal2[1, 1] = "In Word: " + GetInWordFromTotalAmount(totalAmount);
//                        //headerRangeTotal2.Font.Bold = false;
//                        //headerRangeTotal2.Font.Size = 10;
//                        //headerRangeTotal2.Merge(Type.Missing);
//                        //headerRangeTotal2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

//                    }
//                }






//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //XLsheet.PageSetup.PrintTitleRows = "$1:$5";
//                //XLsheet.PageSetup.LeftFooter = "-------------------------------------\n &BPrepared By&B  \n " + "Rapid 2.0| Payroll | Print Date, Time && Page No : " + DateTime.Now.ToString("MMMM dd, yyyy") + " | " + DateTime.Now.ToString("h:mm tt") + " | " + "Page &P of &N";
//                //XLsheet.PageSetup.BottomMargin = application.InchesToPoints(1.5);
//                //XLsheet.PageSetup.CenterFooter = "-------------------------------------\n &BChecked By&B \n";
//                //XLsheet.PageSetup.RightFooter = "------------------\n &BApproved By&B \n";

//                if (assemblyId == 12)
//                {
//                    var totalAmount = 0;
//                    #region Footer Inword & Sign

//                    #region Sum of Total Amount

//                    foreach (DataColumn column in dtOriginalFinal.Columns)
//                    {
//                        if (column.ColumnName == "Amount(BDT)")
//                        {

//                            foreach (DataRow row in dtOriginalFinal.Rows)
//                            {
//                                try
//                                {

//                                    totalAmount += Convert.ToInt32(row["Amount(BDT)"]);


//                                }
//                                catch
//                                {

//                                }
//                            }

//                        }

//                        else
//                        {
//                            foreach (DataRow row in dtOriginalFinal.Rows)
//                            {
//                                try
//                                {

//                                    totalAmount += Convert.ToInt32(row["Net Amount(BDT)"]);


//                                }
//                                catch
//                                {

//                                }
//                            }
//                        }


//                        break;
//                    }




//                    #endregion

//                    if (headerText != "")
//                    {
//                        var colCount2 = ds.Tables[0].Columns.Count;
//                        var dataRowCount2 = ds.Tables[0].Rows.Count;
//                        var headerRangeTotal2 = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount2 + 2, 1], XLsheet.Cells[rowcout + dataRowCount2 + 2, 5]];
//                        headerRangeTotal2[1, 1] = "In Words: " + GetInWordFromTotalAmount(totalAmount) + " Only";
//                        headerRangeTotal2.Font.Bold = false;
//                        headerRangeTotal2.Font.Size = 10;
//                        headerRangeTotal2.Merge(Type.Missing);
//                        headerRangeTotal2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
//                    }
                    

//                    #endregion
//                }



//                XLsheet.Columns.AutoFit();

//                XLsheet.Name = ds.Tables[0].TableName;
//                workbook.Save();

//            }
//            catch (Exception exp)
//            {
//                throw exp;
//            }
//            finally
//            {
//                DisposeExcelObject();
//            }
//        }

//        public void ExportToExcelWithHeaderForBankInstructionExcel(string filePath, int rowcout, string companyName, string companyAddress, int columnScape, bool istotal, string headerText, SqlDataReader reader, string bankBranchTxt)
//        {
//            //   ds = receiveDS;
//            strFileName = filePath;
//            try
//            {
//                application = new Application();
//                application.Visible = false;//true;
//                application.DisplayAlerts = false;

//                workbooks = application.Workbooks;
//                workbook = (Workbook)application.Workbooks.Add(1);
//                workbook.SaveAs(strFileName, XlFileFormat.xlWorkbookNormal, null, null, false, false, XlSaveAsAccessMode.xlExclusive, false, false, null, null, false);

//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
//                //range = worksheet.Cells;

//                Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);
//                #region Main Header

//                #region Logo
//                //var headerRange1 = XLsheet.Range[XLsheet.Cells[1, 1], XLsheet.Cells[rowcout - 2, 6]];
//                //headerRange1.Font.Bold = true;
//                //headerRange1.Font.Size = 16;
//                //headerRange1.Merge(Type.Missing);
//                //headerRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //string picPath = System.Web.HttpContext.Current.Server.MapPath("../Images/report-logo.png");
//                //object missing = System.Reflection.Missing.Value;
//                //Range picPosition = headerRange1;
//                //Pictures p = XLsheet.Pictures(missing) as Microsoft.Office.Interop.Excel.Pictures;
//                //Picture pic = null;
//                //try
//                //{
//                //    pic = p.Insert(picPath, missing);
//                //    pic.Left = Convert.ToDouble(picPosition.Left);
//                //    pic.Top = picPosition.Top;
//                //}
//                //catch (Exception)
//                //{
//                //    picPath = System.Web.HttpContext.Current.Server.MapPath("../../Images/report-logo.png");
//                //    p = XLsheet.Pictures(missing);
//                //    pic = p.Insert(picPath, missing);
//                //    pic.Left = Convert.ToDouble(picPosition.Left);
//                //    pic.Top = picPosition.Top;
//                //}
//                #endregion

//                if (companyName != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 3, 1], XLsheet.Cells[rowcout - 3, 5]];
//                    headerRange2[1, 1] = companyName;
//                    headerRange2.Font.Bold = false;
//                    headerRange2.Font.Size = 10;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
//                }

//                var headerRange3 = XLsheet.Range[XLsheet.Cells[rowcout - 2, 1], XLsheet.Cells[rowcout - 2, 5]];
//                headerRange3[1, 1] = headerText;// "Salary Control Register for the Month of " + salaryMonth.ToString("MMMM-yyyy");//companyAddress;
//                headerRange3.Font.Bold = false;
//                headerRange3.Font.Size = 10;
//                headerRange3.Merge(Type.Missing);
//                headerRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

//                var headerRange4 = XLsheet.Range[XLsheet.Cells[rowcout - 1, 1], XLsheet.Cells[rowcout - 1, 5]];
//                headerRange4[1, 1] = bankBranchTxt;// "Salary Control Register for the Month of " + salaryMonth.ToString("MMMM-yyyy");//companyAddress;
//                headerRange4.Font.Bold = false;
//                headerRange4.Font.Size = 10;
//                headerRange4.Merge(Type.Missing);
//                headerRange4.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

//                #endregion

//                DataTable dtOriginalFinal = new DataTable();
//                int excelSheetRow = this.WriteDataToTheSpecifiedRangeForBankInstructionExcel(range, rowcout, reader, ref dtOriginalFinal);

//                if (istotal)
//                {
//                    var colCount = ds.Tables[0].Columns.Count;
//                    var dataRowCount = ds.Tables[0].Rows.Count;
//                    var headerRangeTotal = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount + 1, 1], XLsheet.Cells[rowcout + dataRowCount + 1, columnScape]];
//                    headerRangeTotal[1, 1] = "Total";
//                    headerRangeTotal.Font.Bold = false;
//                    headerRangeTotal.Font.Size = 10;
//                    headerRangeTotal.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                    headerRangeTotal.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    headerRangeTotal.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    headerRangeTotal.Merge(Type.Missing);
//                    headerRangeTotal.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
//                    //int i = columnScape - 1;

//                    for (int i = 0; i < (colCount - columnScape) + 1; i++)
//                    {
//                        //var col=
//                        var rangeTotal = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1], XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1]];
//                        string startColumnLetter = ExcelColumnLetter(i + columnScape);
//                        string endColumnLetter = ExcelColumnLetter(i + columnScape);

//                        if (listCols[i].DataType == typeof(Decimal) || listCols[i].DataType == typeof(double) || listCols[i].DataType == typeof(int))
//                        {
//                            rangeTotal[1, 1] = "=SUM(" + startColumnLetter + "" + (rowcout + 1) + ":" + endColumnLetter + "" + (rowcout + dataRowCount) + ")";
//                        }

//                        rangeTotal[1, 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                        rangeTotal[1, 1].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                        rangeTotal[1, 1].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);

//                    }
//                }


//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
//                //XLsheet.PageSetup.PrintTitleRows = "$1:$5";
//                //XLsheet.PageSetup.LeftFooter = "-------------------------------------\n &BPrepared By&B  \n " + "Rapid 2.0| Payroll | Print Date, Time && Page No : " + DateTime.Now.ToString("MMMM dd, yyyy") + " | " + DateTime.Now.ToString("h:mm tt") + " | " + "Page &P of &N";
//                //XLsheet.PageSetup.BottomMargin = application.InchesToPoints(1.5);
//                //XLsheet.PageSetup.CenterFooter = "-------------------------------------\n &BChecked By&B \n";
//                //XLsheet.PageSetup.RightFooter = "------------------\n &BApproved By&B \n";

//                #region Footer Inword & Sign

//                #region Sum of Total Amount
//                var totalAmount = 0;

//                foreach (DataRow row in dtOriginalFinal.Rows)
//                {
//                    try
//                    {
//                        totalAmount += Convert.ToInt32(row["Amount"]);
//                    }
//                    catch
//                    {

//                    }
//                }
//                #endregion

//                var colCount2 = ds.Tables[0].Columns.Count;
//                var dataRowCount2 = ds.Tables[0].Rows.Count;
//                var headerRangeTotal2 = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount2 + 2, 1], XLsheet.Cells[rowcout + dataRowCount2 + 2, 5]];
//                headerRangeTotal2[1, 1] = "In Word: " + GetInWordFromTotalAmount(totalAmount);
//                headerRangeTotal2.Font.Bold = false;
//                headerRangeTotal2.Font.Size = 10;
//                headerRangeTotal2.Merge(Type.Missing);
//                headerRangeTotal2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

//                var headerRangeTotal3 = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount2 + 3, 1], XLsheet.Cells[rowcout + dataRowCount2 + 3, 5]];
//                headerRangeTotal3[1, 1] = "";
//                headerRangeTotal3.Font.Bold = false;
//                headerRangeTotal3.Font.Size = 10;
//                headerRangeTotal3.Merge(Type.Missing);
//                headerRangeTotal3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

//                var headerRangeTotal4 = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount2 + 4, 1], XLsheet.Cells[rowcout + dataRowCount2 + 4, 5]];
//                headerRangeTotal4[1, 1] = "";
//                headerRangeTotal4.Font.Bold = false;
//                headerRangeTotal4.Font.Size = 10;
//                headerRangeTotal4.Merge(Type.Missing);
//                headerRangeTotal4.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

//                var headerRangeTotal5 = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount2 + 5, 1], XLsheet.Cells[rowcout + dataRowCount2 + 5, 5]];
//                headerRangeTotal5[1, 1] = "";
//                headerRangeTotal5.Font.Bold = false;
//                headerRangeTotal5.Font.Size = 10;
//                headerRangeTotal5.Merge(Type.Missing);
//                headerRangeTotal5.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

//                var headerRangeTotal6 = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount2 + 6, 1], XLsheet.Cells[rowcout + dataRowCount2 + 6, 5]];
//                headerRangeTotal6[1, 1] = "";
//                headerRangeTotal6.Font.Bold = false;
//                headerRangeTotal6.Font.Size = 10;
//                headerRangeTotal6.Merge(Type.Missing);
//                headerRangeTotal6.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

//                var headerRangeTotal7 = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount2 + 7, 1], XLsheet.Cells[rowcout + dataRowCount2 + 7, 5]];
//                headerRangeTotal7[1, 1] = "";
//                headerRangeTotal7.Font.Bold = false;
//                headerRangeTotal7.Font.Size = 10;
//                headerRangeTotal7.Merge(Type.Missing);
//                headerRangeTotal7.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;


//                var headerRangeTotal8 = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount2 + 8, 1], XLsheet.Cells[rowcout + dataRowCount2 + 8, 5]];
//                headerRangeTotal8[1, 1] = "Prepared by                        Checked by                        Approved by                       Approved by";
//                headerRangeTotal8.Font.Bold = false;
//                headerRangeTotal8.Font.Size = 10;
//                headerRangeTotal8.Merge(Type.Missing);
//                headerRangeTotal8.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

//                #endregion

//                XLsheet.Columns.AutoFit();

//                XLsheet.Name = ds.Tables[0].TableName;
//                workbook.Save();

//            }
//            catch (Exception exp)
//            {
//                throw exp;
//            }
//            finally
//            {
//                DisposeExcelObject();
//            }
//        }

//        private string GetInWordFromTotalAmount(int amount)
//        {
//            decimal numAmount;
//            string strCrore;
//            string strLac;
//            string strThousand;

//            decimal numCrore;
//            decimal numLac;
//            decimal numThousand;
//            decimal numPaisa;
//            string strPaisa;
//            numAmount = amount;

//            numPaisa = (numAmount - Decimal.Truncate(numAmount)) * 100;
//            numAmount = numAmount - (numPaisa / 100);
//            numCrore = Decimal.Truncate((numAmount / 10000000));
//            numAmount = numAmount - numCrore * 10000000;
//            numLac = Decimal.Truncate((numAmount / 100000));
//            numThousand = numAmount - numLac * 100000;

//            if (numCrore > 0)
//                strCrore = ConvertNumberToText(numCrore) + " Crore ";
//            else strCrore = "";
//            if (numLac > 0)
//                strLac = ConvertNumberToText(numLac) + " Lac ";
//            else strLac = "";
//            if (numThousand > 0)
//                strThousand = ConvertNumberToText(numThousand);
//            else strThousand = "";
//            if (numPaisa > 0)
//                strPaisa = " and Paisa " + ConvertNumberToText(numPaisa);
//            else strPaisa = "";

//            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
//            var finalWord = (strCrore + strLac + strThousand + strPaisa) + " Taka";
//            return myTI.ToTitleCase(finalWord);
//        }

//        static string ConvertNumberToText(decimal number)
//        {
//            var num = Convert.ToInt32(number);
//            string tempString = "";

//            int thousands;

//            int temp;

//            var result = "";

//            if (num == 0)
//            {

//                return "";

//            }


//            if (num < 1000)
//            {

//                HelperConvertNumberToText(num, out tempString);

//                result += tempString;

//            }

//            else
//            {

//                thousands = num / 1000;

//                temp = num - thousands * 1000;

//                HelperConvertNumberToText(thousands, out tempString);

//                result += tempString;

//                result += "Thousand ";

//                HelperConvertNumberToText(temp, out tempString);

//                result += tempString;

//            }

//            return result;

//        }

//        static bool HelperConvertNumberToText(int num, out string buf)
//        {

//            string[] strones = {

//            "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight",

//            "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen",

//            "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen",

//          };



//            string[] strtens = {

//              "Ten", "Twenty", "Thirty", "Fourty", "Fifty", "Sixty",

//              "Seventy", "Eighty", "Ninety", "Hundred"

//          };



//            string result = "";

//            buf = "";

//            int single, tens, hundreds;



//            if (num > 1000)

//                return false;



//            hundreds = num / 100;

//            num = num - hundreds * 100;

//            if (num < 20)
//            {

//                tens = 0; // special case

//                single = num;

//            }

//            else
//            {

//                tens = num / 10;

//                num = num - tens * 10;

//                single = num;

//            }



//            result = "";



//            if (hundreds > 0)
//            {

//                result += strones[hundreds - 1];

//                result += " Hundred ";

//            }

//            if (tens > 0)
//            {

//                result += strtens[tens - 1];

//                result += " ";

//            }

//            if (single > 0)
//            {

//                result += strones[single - 1];

//                result += " ";

//            }



//            buf = result;

//            return true;

//        }

//        private int WriteDataToTheSpecifiedRangeForBankInstructionExcel(Range range, int rowount, SqlDataReader reader, ref DataTable dtOriginalFinal)
//        {
//            int iRow;
//            int iCol;
//            //Traverse through the DataTable columns to write the headers on the first row of the excel sheet.
//            DataTable dt = new DataTable();
//            DataTable dtOriginal = new DataTable();
//            DataTable dtSchema = reader.GetSchemaTable();
//            listCols = new List<DataColumn>();
//            if (dtSchema != null)
//            {
//                foreach (DataRow drow in dtSchema.Rows)
//                {
//                    string columnName = Convert.ToString(drow["ColumnName"]);
//                    var column = new DataColumn(columnName, (Type)(drow["DataType"]));
//                    column.Unique = (bool)drow["IsUnique"];
//                    column.AllowDBNull = (bool)drow["AllowDBNull"];
//                    column.AutoIncrement = (bool)drow["IsAutoIncrement"];
//                    listCols.Add(column);
//                    dt.Columns.Add(column);
//                }

//                dtOriginal = dt.Clone();

//                #region Column Hiding
//                try
//                {
//                    dt.Columns.Remove("SortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("EmploymentDate");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("DSortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("IsClosed");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("EmployeeTypeName");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("EmpNoOfCashCtc");
//                }
//                catch (Exception)
//                {

//                }
//                try
//                {
//                    dt.Columns.Remove("EmpTypeSortOrder");
//                }
//                catch (Exception)
//                {

//                }
//                #endregion
//            }
//            for (iCol = 0; iCol < dt.Columns.Count; iCol++)
//            {
//                if (iCol == 0)
//                {
//                    range[rowount, (iCol + 1)] = "SL";
//                    range.WrapText = false;
//                    range.Font.Bold = true;
//                    range[rowount, (iCol + 1)].Font.Size = 10;
//                    range[rowount, (iCol + 1)].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                    range[rowount, (iCol + 1)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    range[rowount, (iCol + 1)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                }
//                range[rowount, (iCol + 2)] = iCol < 4 ? Regex.Replace(dt.Columns[iCol].ColumnName, "([a-z])_?([A-Z])", "$1 $2") : dt.Columns[iCol].ColumnName;
//                range.WrapText = false;
//                range.Font.Bold = true;
//                range[rowount, (iCol + 2)].Font.Size = 10;
//                range[rowount, (iCol + 2)].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                range[rowount, (iCol + 2)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                range[rowount, (iCol + 2)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                if (iCol > 3)
//                {
//                    range[rowount, (iCol + 2)].Style.WrapText = true;
//                }
//                else
//                {
//                    range[rowount, (iCol + 2)].Style.WrapText = false;
//                }
//            }


//            // Call Read before accessing data. 
//            while (reader.Read())
//            {
//                DataRow dataRow = dtOriginal.NewRow();
//                for (int i = 0; i < listCols.Count; i++)
//                {
//                    dataRow[(listCols[i].ColumnName)] = reader[i];
//                }
//                //dt.Rows.Add(dataRow); //Previous
//                dtOriginal.Rows.Add(dataRow);
//            }


//            #region Column Hiding
//            if (dtOriginal.Rows.Count > 0)
//            {
//                try
//                {
//                    dtOriginal.Columns.Remove("SortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("EmploymentDate");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("DSortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("IsClosed");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("EmployeeTypeName");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("EmpNoOfCashCtc");
//                }
//                catch (Exception)
//                {

//                }
//                try
//                {
//                    dtOriginal.Columns.Remove("EmpTypeSortOrder");
//                }
//                catch (Exception)
//                {

//                }
//            }
//            #endregion

//            //ds.Tables.Add(dt); //Previous
//            ds.Tables.Add(dtOriginal);
//            dtOriginalFinal = dtOriginal;

//            //Traverse through the rows and columns of the datatable to write the data in the sheet.
//            int excelSheetRow = rowount + 1;
//            int sl = 1;


//            foreach (DataRow row in dtOriginal.Rows)
//            {
//                // var newRow = new Row();
//                iCol = 0;
//                foreach (DataColumn col in dtOriginal.Columns)
//                {
//                    if (iCol == 0)
//                    {
//                        range[(excelSheetRow), (iCol + 1)] = sl;
//                    }
//                    if (col.ToString() == "Cash" || col.ToString() == "Net Payout")
//                    {
//                        try
//                        {
//                            if (Convert.ToInt32(row[col]) < 0)
//                            {
//                                range[(excelSheetRow), (iCol + 2)] = "(" + row[col].ToString() + ")";
//                            }
//                            else
//                            {
//                                range[(excelSheetRow), (iCol + 2)] = row[col].ToString();
//                            }
//                        }
//                        catch (Exception)
//                        {

//                            range[(excelSheetRow), (iCol + 2)] = row[col].ToString();
//                        }

//                    }
//                    else if (col.ToString() == "Bank Account No" || col.ToString() == "Employee ID"|| col.ToString() == "Wallet Number")
//                    {
//                        range[(excelSheetRow), (iCol + 2)].NumberFormat = "@";
//                        range[(excelSheetRow), (iCol + 2)] = row[col].ToString();
//                    }
//                    else
//                    {
//                        range[(excelSheetRow), (iCol + 2)] = row[col].ToString();
//                    }

//                    iCol++;
//                }
//                excelSheetRow++;
//                sl++;
//            }

//            return excelSheetRow;
//        }
//        private int WriteDataToTheSpecifiedRangeItk(Range range, int rowount, SqlDataReader reader, int assemblyId, string colourCode)
//        {
//            int iRow;
//            int iCol;
//            //Traverse through the DataTable columns to write the headers on the first row of the excel sheet.
//            DataTable dt = new DataTable();
//            DataTable dtOriginal = new DataTable();
//            DataTable dtSchema = reader.GetSchemaTable();
//            if (dtSchema != null)
//            {
//                foreach (DataRow drow in dtSchema.Rows)
//                {
//                    string columnName = Convert.ToString(drow["ColumnName"]);
//                    var column = new DataColumn(columnName, (Type)(drow["DataType"]));
//                    column.Unique = (bool)drow["IsUnique"];
//                    column.AllowDBNull = (bool)drow["AllowDBNull"];
//                    column.AutoIncrement = (bool)drow["IsAutoIncrement"];
//                    listCols.Add(column);
//                    dt.Columns.Add(column);
//                }

//                dtOriginal = dt.Clone();

//                #region Column Hiding
//                try
//                {
//                    dt.Columns.Remove("SortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("EmploymentDate");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("DSortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("IsClosed");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("EmployeeTypeName");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dt.Columns.Remove("EmpNoOfCashCtc");
//                }
//                catch (Exception)
//                {

//                }
//                try
//                {
//                    dt.Columns.Remove("EmpTypeSortOrder");
//                }
//                catch (Exception)
//                {

//                }
//                try
//                {
//                    dt.Columns.Remove("DueAmount");
//                }
//                catch (Exception)
//                {

//                }
//                try
//                {
//                    dt.Columns.Remove("LoanAmount");
//                }
//                catch (Exception)
//                {

//                }
//                try
//                {
//                    dt.Columns.Remove("BankCode");
//                }
//                catch (Exception)
//                {

//                }

//                //try
//                //{
//                //    dt.Columns.Remove("Bank Name");
//                //}
//                //catch (Exception)
//                //{

//                //}
//                if (assemblyId != 19)
//                {

//                    try
//                    {
//                        dt.Columns.Remove("Bank");
//                    }
//                    catch (Exception)
//                    {

//                    }
//                    try
//                    {
//                        dt.Columns.Remove("Cash");
//                    }
//                    catch (Exception)
//                    {

//                    }
//                }

//                #endregion
//            }
//            for (iCol = 0; iCol < dt.Columns.Count; iCol++)
//            {
//                if (iCol == 0)
//                {
//                    range[rowount, (iCol + 1)] = "SL";
//                    range.WrapText = false;
//                    range.Font.Bold = true;
//                    range[rowount, (iCol + 1)].Font.Size = 10;
//                    range[rowount, (iCol + 1)].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colourCode));
//                    range[rowount, (iCol + 1)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    range[rowount, (iCol + 1)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                }
//                if (iCol == 1)
//                {
//                    range[rowount, (iCol + 2)].NumberFormat = "@";
//                }

//                range[rowount, (iCol + 2)] = iCol < 4 ? Regex.Replace(dt.Columns[iCol].ColumnName, "([a-z])_?([A-Z])", "$1 $2") : dt.Columns[iCol].ColumnName;
//                range.WrapText = false;
//                range.Font.Bold = true;
//                range[rowount, (iCol + 2)].Font.Size = 10;
//                range[rowount, (iCol + 2)].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colourCode));
//                range[rowount, (iCol + 2)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                range[rowount, (iCol + 2)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                range[rowount, (iCol + 2)].Style.WrapText = false;
//            }


//            // Call Read before accessing data. 
//            while (reader.Read())
//            {
//                DataRow dataRow = dtOriginal.NewRow();
//                for (int i = 0; i < listCols.Count; i++)
//                {
//                    dataRow[(listCols[i].ColumnName)] = reader[i];
//                }
//                //dt.Rows.Add(dataRow); //Previous
//                dtOriginal.Rows.Add(dataRow);
//            }


//            #region Column Hiding
//            if (dtOriginal.Rows.Count > 0)
//            {
//                try
//                {
//                    dtOriginal.Columns.Remove("SortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("EmploymentDate");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("DSortOrder");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("IsClosed");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("EmployeeTypeName");
//                }
//                catch (Exception)
//                {

//                }

//                try
//                {
//                    dtOriginal.Columns.Remove("EmpNoOfCashCtc");
//                }
//                catch (Exception)
//                {

//                }
//                try
//                {
//                    dtOriginal.Columns.Remove("EmpTypeSortOrder");
//                }
//                catch (Exception)
//                {

//                }
//                try
//                {
//                    dtOriginal.Columns.Remove("DueAmount");
//                }
//                catch (Exception)
//                {

//                }
//                try
//                {
//                    dtOriginal.Columns.Remove("LoanAmount");
//                }
//                catch (Exception)
//                {

//                }
//                try
//                {
//                    dtOriginal.Columns.Remove("BankCode");
//                }
//                catch (Exception)
//                {

//                }


//                //try
//                //{
//                //    dtOriginal.Columns.Remove("Bank Account No");
//                //}
//                //catch (Exception)
//                //{

//                //}
//                //try
//                //{
//                //    dtOriginal.Columns.Remove("Bank Name");
//                //}
//                //catch (Exception)
//                //{

//                //}
//                if (assemblyId != 19)
//                {
//                    try
//                    {
//                        dtOriginal.Columns.Remove("Bank");
//                    }
//                    catch (Exception)
//                    {

//                    }
//                    try
//                    {
//                        dtOriginal.Columns.Remove("Cash");
//                    }
//                    catch (Exception)
//                    {

//                    }
//                }
//            }
//            #endregion

//            //ds.Tables.Add(dt); //Previous
//            ds.Tables.Add(dtOriginal);


//            //Traverse through the rows and columns of the datatable to write the data in the sheet.
//            int excelSheetRow = rowount + 1;
//            int sl = 1;


//            foreach (DataRow row in dtOriginal.Rows)
//            {
//                // var newRow = new Row();
//                iCol = 0;
//                foreach (DataColumn col in dtOriginal.Columns)
//                {
//                    if (iCol == 0)
//                    {
//                        range[(excelSheetRow), (iCol + 1)] = sl;
//                    }
//                    if (col.ToString() == "Cash" || col.ToString() == "Net Payout")
//                    {
//                        try
//                        {
//                            if (Convert.ToInt32(row[col]) < 0)
//                            {
//                                range[(excelSheetRow), (iCol + 2)] = "(" + row[col].ToString() + ")";
//                            }
//                            else
//                            {
//                                range[(excelSheetRow), (iCol + 2)] = row[col].ToString();
//                            }
//                        }
//                        catch (Exception)
//                        {

//                            range[(excelSheetRow), (iCol + 2)] = row[col].ToString();
//                        }

//                    }
//                    else if (col.ToString() == "Bank Account No")
//                    {
//                        range[(excelSheetRow), (iCol + 2)].NumberFormat = "@";
//                        range[(excelSheetRow), (iCol + 2)] = row[col].ToString();
//                    }
//                    else if (col.ToString() == "Branch Code")
//                    {
//                        range[(excelSheetRow), (iCol + 2)].NumberFormat = "@";
//                        range[(excelSheetRow), (iCol + 2)] = row[col].ToString();
//                    }
//                    else if (col.ToString() == "Account Code")
//                    {
//                        range[(excelSheetRow), (iCol + 2)].NumberFormat = "@";
//                        range[(excelSheetRow), (iCol + 2)] = row[col].ToString();
//                    }
//                    else
//                    {
//                        if (col.ToString() == "EmployeeId" || col.ToString() == "Routing No")
//                        {
//                            range[(excelSheetRow), (iCol + 2)].NumberFormat = "@";
//                        }
//                        range[(excelSheetRow), (iCol + 2)] = row[col].ToString();
//                    }

//                    iCol++;
//                }
//                excelSheetRow++;
//                sl++;
//            }

//            return excelSheetRow;
//        }

//        public void ExportToExcelItk(string filePath, int rowcout, string companyName, string companyAddress, int columnScape, bool istotal, string headerText,
//               SqlDataReader reader, int assemblyId, string colourCode)
//        {
//            //   ds = receiveDS;
//            strFileName = filePath;
//            try
//            {
//                application = new Application();
//                application.Visible = false;//true;
//                application.DisplayAlerts = false;

//                workbooks = application.Workbooks;
//                workbook = (Workbook)application.Workbooks.Add(1);
//                workbook.SaveAs(strFileName, XlFileFormat.xlWorkbookNormal, null, null, false, false, XlSaveAsAccessMode.xlExclusive, false, false, null, null, false);

//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //range = worksheet.Cells;

//                Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);
//                #region Main Header
//                if (headerText != "")
//                {
//                    var headerRange3 = XLsheet.Range[XLsheet.Cells[rowcout - 1, 1], XLsheet.Cells[rowcout - 1, 10]];
//                    headerRange3[1, 1] = headerText;// "Salary Control Register for the Month of " + salaryMonth.ToString("MMMM-yyyy");//companyAddress;
//                    headerRange3.Font.Bold = true;
//                    headerRange3.Font.Size = 12;
//                    headerRange3.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("Black"));
//                    headerRange3.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("white"));
//                    headerRange3.Merge(Type.Missing);
//                    headerRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
//                }

//                #endregion


//                int excelSheetRow = this.WriteDataToTheSpecifiedRangeItk(range, rowcout, reader, assemblyId, colourCode);

//                if (istotal)
//                {

//                    var colCount = ds.Tables[0].Columns.Count;
//                    var dataRowCount = ds.Tables[0].Rows.Count;
//                    var headerRangeTotal = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount + 1, 1], XLsheet.Cells[rowcout + dataRowCount + 1, columnScape]];
//                    headerRangeTotal[1, 1] = "Total";
//                    headerRangeTotal.Font.Bold = true;
//                    headerRangeTotal.Font.Size = 12;
//                    headerRangeTotal.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colourCode));
//                    headerRangeTotal.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    headerRangeTotal.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    headerRangeTotal.Merge(Type.Missing);
//                    headerRangeTotal.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                    //int i = columnScape - 1;

//                    for (int i = 0; i < (colCount - columnScape) + 1; i++)
//                    {
//                        //var col=
//                        var rangeTotal = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1], XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1]];
//                        string startColumnLetter = ExcelColumnLetter(i + columnScape);
//                        string endColumnLetter = ExcelColumnLetter(i + columnScape);

//                        if (listCols[i].DataType == typeof(Decimal) || listCols[i].DataType == typeof(double) || listCols[i].DataType == typeof(int))
//                        {
//                            rangeTotal[1, 1] = "=SUM(" + startColumnLetter + "" + (rowcout + 1) + ":" + endColumnLetter + "" + (rowcout + dataRowCount) + ")";
//                        }

//                        rangeTotal[1, 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colourCode));
//                        rangeTotal[1, 1].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                        rangeTotal[1, 1].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);

//                    }
//                }


//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                // XLsheet.PageSetup.PrintTitleRows = "$1:$5";
//                //XLsheet.PageSetup.LeftFooter = "-------------------------------------\n &BPrepared By&B  \n " + "Rapid 2.0| Payroll | Print Date, Time && Page No : " + DateTime.Now.ToString("MMMM dd, yyyy") + " | " + DateTime.Now.ToString("h:mm tt") + " | " + "Page &P of &N";
//                //XLsheet.PageSetup.BottomMargin = application.InchesToPoints(1.5);
//                //XLsheet.PageSetup.CenterFooter = "-------------------------------------\n &BChecked By&B \n";
//                //XLsheet.PageSetup.RightFooter = "------------------\n &BApproved By&B \n";

//                XLsheet.Columns.AutoFit();

//                XLsheet.Name = ds.Tables[0].TableName;
//                workbook.Save();

//            }
//            catch (Exception exp)
//            {
//                throw exp;
//            }
//            finally
//            {
//                DisposeExcelObject();
//            }
//        }


//        private class GrandTotalCount
//        {
//            public int ColumnNo { get; set; }
//        }

//        public void GenerateExcelWithHeaderAndSqlDataReaderForPrintableDeptWiseSalarySheet(DataTable reader, string filePath, int rowcout, string companyName,
//        string companyAddress, int columnScape, bool istotal, int assemblyInfoId, SqlDataReader readerData)
//        {
//            var title = "";
//            strFileName = filePath;
//            try
//            {
//                application = new Application();
//                application.Visible = false; //true;
//                application.DisplayAlerts = false;

//                workbooks = application.Workbooks;
//                workbook = (Workbook)application.Workbooks.Add(1);
//                workbook.SaveAs(strFileName, XlFileFormat.xlWorkbookNormal, null, null, false, false,
//                    XlSaveAsAccessMode.xlExclusive, false, false, null, null, false);

//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //range = worksheet.Cells;

//                Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);


//                #region Print DateTime

//                #endregion

//                int excelSheetRow = this.WriteDataToTheSpecifiedRangeWithReaderForPrintableDeptWiseSalarySheet(reader, range, rowcout, assemblyInfoId, readerData);


//                range.Name = ds.Tables[0].TableName;
//                var assemblyInfo = Data<AssemblyInfo>.DataSource(string.Format(@"select top 1 * from AssemblyInfo")).FirstOrDefault();
//                XLsheet.Columns.AutoFit();

//                workbook.Save();
//            }
//            catch (Exception exp)
//            {
//                //string path = System.Web.HttpContext.Current.Server.MapPath(@"");
//                //path = path.Substring(0, path.Length - 3);
//                //System.IO.File.AppendAllText("LeaveYearTemp" + "/errorLog.txt", exp.Message);
//                throw exp;
//            }
//            finally
//            {
//                DisposeExcelObject();
//            }
//        }

//        private int WriteDataToTheSpecifiedRangeWithReaderForPrintableDeptWiseSalarySheet(DataTable reader, Range range, int rowount, int assemblyInfoId, SqlDataReader readerData)
//        {
//            int iRow;
//            int iCol;
//            var objAzExport = new AzExportToExcel();
//            //var cpc = new CommonParamConditon(); 

//            #region Data Table Conversion from Reader
//            DataTable dt = new DataTable();
//            DataTable dtOriginal = new DataTable();
//            DataTable dtSchema = readerData.GetSchemaTable();
//            if (dtSchema != null)
//            {
//                foreach (DataRow drow in dtSchema.Rows)
//                {
//                    string columnName = Convert.ToString(drow["ColumnName"]);
//                    var column = new DataColumn(columnName, (Type)(drow["DataType"]));
//                    column.Unique = (bool)drow["IsUnique"];
//                    column.AllowDBNull = (bool)drow["AllowDBNull"];
//                    column.AutoIncrement = (bool)drow["IsAutoIncrement"];
//                    listCols.Add(column);
//                    dt.Columns.Add(column);
//                }
//                dtOriginal = dt.Clone();
//            }


//            //ds.Tables.Add(dt);
//            ds.Tables.Add(dtOriginal);
//            //dtOriginalFinal = dtOriginal;
//            #endregion

//            int excelSheetRow = rowount + 1;
//            int sl = 1;

//            var companyName = "";
//            var locationName = "";
//            var branchName = "";
//            var companyAddress = "";
//            var departmnentName = "";
//            var hdCount = false;
//            var listGrandTotalCount = new List<GrandTotalCount>();

//            //foreach (var salary in dtSchema.Rows)
//            //{
//            //    var data = "";
//            //}


//            #region Table

//            iCol = 0;
//            foreach (var salary in dt.Rows)
//            {
//                #region Company Name
//                //string companyName = "";
//                if (dt.Columns[iCol].ColumnName == "CompanyName")
//                {
//                    companyName = Convert.ToString(salary.Equals("CompanyName"));
//                }
//                if (dt.Columns[iCol].ColumnName == "CompanyAddress")
//                {
//                    companyAddress = Convert.ToString(salary.Equals("CompanyAddress"));
//                }
//                if (dt.Columns[iCol].ColumnName == "BranchName")
//                {
//                    branchName = Convert.ToString(salary.Equals("BranchName"));
//                }
//                if (dt.Columns[iCol].ColumnName == "DepartmetName")
//                {
//                    departmnentName = Convert.ToString(salary.Equals("DepartmetName"));
//                }

//                var gr = new GrandTotalCount();
//                if (companyName == "")
//                {
//                    if (companyName == "")
//                    {
//                        Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);
//                        var headerRange1 = XLsheet.Range[XLsheet.Cells[rowount, 1], XLsheet.Cells[rowount, dt.Columns.Count + 1]];
//                        //var rg = range[range.Cells[excelSheetRow, 1], range.Cells[excelSheetRow, dt.Columns.Count]];
//                        headerRange1[1, 1] = companyName;
//                        headerRange1.Font.Bold = true;
//                        headerRange1.Font.Size = 14;
//                        headerRange1.Merge(Type.Missing);
//                        headerRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                        //headerRange1.Interior.Color =
//                        //    System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#0066ff"));


//                        if (assemblyInfoId == 12)
//                        {

//                            var headerRange2 = XLsheet.Range[XLsheet.Cells[rowount + 1, 1], XLsheet.Cells[rowount + 1, dt.Columns.Count + 1]];
//                            //var rg = range[range.Cells[excelSheetRow, 1], range.Cells[excelSheetRow, dt.Columns.Count]];
//                            headerRange2[1, 1] = companyAddress;
//                            headerRange2.Font.Bold = true;
//                            headerRange2.Font.Size = 12;
//                            headerRange2.Merge(Type.Missing);
//                            headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                            rowount++;

//                            var headerRange3 = XLsheet.Range[XLsheet.Cells[rowount + 1, 1], XLsheet.Cells[rowount + 1, dt.Columns.Count + 1]];
//                            //var rg = range[range.Cells[excelSheetRow, 1], range.Cells[excelSheetRow, dt.Columns.Count]];
//                            //headerRange3[1, 1] = "Salary Sheet for the Month of " + salaryMonth.ToString("MMMM-yyyy");
//                            headerRange3.Font.Bold = true;
//                            headerRange3.Font.Size = 12;
//                            headerRange3.Merge(Type.Missing);
//                            headerRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                            rowount++;
//                            excelSheetRow++;
//                        }

//                        //var rg = range[rowount, 3];
//                        //range[rowount, 3] = salary.CompanyName + Environment.NewLine + salary.CompanyAddress;
//                        //range[rowount, 3].Font.Bold = true;
//                        //range[rowount, 3].Font.Size = 12;
//                        //range[rowount, 3].Merge(Type.Missing);
//                        //range[rowount, 3].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

//                        //range[rowount, 3].Interior.Color =
//                        //    System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#0066ff"));


//                        rowount++;
//                        excelSheetRow++;

//                        //var headerRange2 = XLsheet.Range[XLsheet.Cells[rowount, 1], XLsheet.Cells[rowount, dt.Columns.Count+1]];
//                        ////var rg = range[range.Cells[excelSheetRow, 1], range.Cells[excelSheetRow, dt.Columns.Count]];
//                        //headerRange2[1, 1] = salary.CompanyAddress;
//                        //headerRange2.Font.Bold = true;
//                        //headerRange2.Font.Size = 14;
//                        //headerRange2.Merge(Type.Missing);
//                        //headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                        ////headerRange2.Interior.Color =
//                        ////    System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#0066ff"));

//                        //rowount++;
//                        //excelSheetRow++;
//                    }
//                    else
//                    {
//                        //var rg = range[range.Cells[excelSheetRow, 1], range.Cells[excelSheetRow, dt.Columns.Count]];

//                        //var headerRange2 = range[XLsheet.Cells[rowcout - 2, 7], XLsheet.Cells[rowcout - 3, 14]];

//                        //range[excelSheetRow, 3] = salary.CompanyName + Environment.NewLine + salary.CompanyAddress;
//                        //range[excelSheetRow, 3].Interior.Color =
//                        //   System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#0066ff"));
//                        Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);
//                        var headerRange1 = XLsheet.Range[XLsheet.Cells[excelSheetRow, 1], XLsheet.Cells[excelSheetRow, dt.Columns.Count + 1]];
//                        //var rg = range[range.Cells[excelSheetRow, 1], range.Cells[excelSheetRow, dt.Columns.Count]];
//                        headerRange1[1, 1] = companyName;
//                        headerRange1.Font.Bold = true;
//                        headerRange1.Font.Size = 20;
//                        headerRange1.Merge(Type.Missing);
//                        headerRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                        //headerRange1.Interior.Color =
//                        //    System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#0066ff"));


//                        rowount++;
//                        excelSheetRow++;


//                        //var headerRange2 = XLsheet.Range[XLsheet.Cells[rowount, 1], XLsheet.Cells[rowount, dt.Columns.Count + 1]];
//                        ////var rg = range[range.Cells[excelSheetRow, 1], range.Cells[excelSheetRow, dt.Columns.Count]];
//                        //headerRange2[1, 1] = salary.CompanyAddress;
//                        //headerRange2.Font.Bold = true;
//                        //headerRange2.Font.Size = 14;
//                        //headerRange2.Merge(Type.Missing);
//                        //headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                        ////headerRange2.Interior.Color =
//                        ////    System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#0066ff"));

//                        //rowount++;
//                        //excelSheetRow++;
//                    }

//                }

//                #endregion

//                #region Column Name
//                //Traverse through the DataTable columns to write the headers on the first row of the excel sheet.
//                if (companyName == "")
//                {
//                    for (iCol = 0; iCol < dt.Columns.Count; iCol++)
//                    {
//                        if (iCol == 0)
//                        {
//                            range[rowount, (iCol + 1)] = "SL";
//                            range[rowount, (iCol + 1)].Interior.Color =
//                                System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                        }
//                        range[rowount, (iCol + 2)] = iCol < 12
//                            ? Regex.Replace(dt.Columns[iCol].ColumnName, "([a-z])_?([A-Z])", "$1 $2")
//                            : dt.Columns[iCol].ColumnName;

//                        range[rowount, (iCol + 2)].Interior.Color =
//                         System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                    }
//                    rowount++;
//                    excelSheetRow++;
//                }

//                #endregion

//                if (assemblyInfoId != 5 && assemblyInfoId != 12)
//                {
//                    #region Loction Column
//                    if (locationName == "" || locationName != "" || companyName != "")
//                    {
//                        var rg = range[rowount, 3];
//                        range[rowount, 3] = branchName;
//                        rowount++;
//                        excelSheetRow++;
//                    }


//                    #endregion

//                    #region Department Column

//                    if (departmnentName == "" || departmnentName != "" ||
//                        locationName != "")
//                    {
//                        var rg = range[excelSheetRow, 3];
//                        range[excelSheetRow, 3] = departmnentName;
//                        range[excelSheetRow, 3].Interior.Color =
//                           System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#cc6600"));
//                        rowount++;
//                        excelSheetRow++;
//                    }

//                    #endregion
//                }
//                else
//                {
//                    #region Department Column

//                    if (departmnentName != "")
//                    {
//                        //var dataTableByDep = from row in dt.AsEnumerable()
//                        //                     where
//                        //                         row.Field<string>("Department") == salary.DepartmentName && row.Field<string>("CompanyName") == salary.CompanyName
//                        //                     select row;

//                        var dataTableByDep = from row in dt.AsEnumerable()
//                                             where
//                                                 row.Field<string>("Department") == departmnentName && row.Field<string>("CompanyName") == companyName
//                                             select row;

//                        if (dataTableByDep.Count() != 0)
//                        {
//                            var rg = range[excelSheetRow, 3];
//                            range[excelSheetRow, 3] = departmnentName;
//                            range[excelSheetRow, 3].Interior.Color =
//                                System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#cc6600"));
//                            rowount++;
//                            excelSheetRow++;
//                        }
//                    }

//                    #endregion
//                }

//                if (assemblyInfoId == 5 || assemblyInfoId == 12)
//                {

//                    var dataTableByDep = from row in dt.AsEnumerable()
//                                         where
//                                             row.Field<string>("Department") == departmnentName && row.Field<string>("CompanyName") == companyName
//                                         select row;

//                    var datadep = dataTableByDep.AsDataView().ToTable();
//                    if (datadep.Rows.Count > 0)
//                    {

//                        #region Row Binding
//                        for (iRow = 0; iRow < datadep.Rows.Count; iRow++)
//                        {
//                            for (iCol = 0; iCol < datadep.Columns.Count; iCol++)
//                            {
//                                if (iCol == 0)
//                                {
//                                    range[(excelSheetRow), (iCol + 1)] = sl;
//                                }
//                                if (datadep.Columns[iCol].ColumnName.Equals("TIN"))
//                                {
//                                    range[(excelSheetRow), (iCol + 2)] = "'" + datadep.Rows[iRow][iCol].ToString();
//                                }
//                                else
//                                {
//                                    range[(excelSheetRow), (iCol + 2)] = datadep.Rows[iRow][iCol].ToString();
//                                }
//                                if (iCol > 11)
//                                {
//                                    try
//                                    {
//                                        decimal ctcAmt = Convert.ToDecimal(datadep.Rows[iRow][iCol].ToString());
//                                        if (ctcAmt < 0)
//                                        {
//                                            var nt = Convert.ToDecimal(dt.Rows[iRow]["Net Payout"].ToString());
//                                            range[(excelSheetRow), (iCol + 2)] = "(" + datadep.Rows[iRow][iCol].ToString() + ")";
//                                            if (nt < 0)
//                                            {
//                                                datadep.Rows[iRow]["Remarks"] = "Adjusted With Bank";
//                                            }

//                                        }
//                                    }
//                                    catch
//                                    {

//                                    }

//                                    range[(excelSheetRow), (iCol + 2)].NumberFormat = "#,###,###";

//                                }
//                                range[(excelSheetRow), (iCol + 2)].Font.Name = "Verdana";


//                            }
//                            excelSheetRow++;
//                            sl++;
//                        }

//                        #endregion

//                        #region Add SubTotaal Text

//                        var rgsub = range[excelSheetRow, 3];
//                        range[excelSheetRow, 3] = "Sub Total";
//                        range[excelSheetRow, 3].Interior.Color =
//                           System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CCFFCC"));
//                        #endregion

//                        #region Add Summation

//                        for (int i = 11; i <= dt.Columns.Count; i++)
//                        {
//                            var colcount = dt.Columns;

//                            string startColumnLetter = objAzExport.ExcelColumnLetter(i - 1);
//                            string endColumnLetter = objAzExport.ExcelColumnLetter(i - 1);
//                            var headerRangeParentCostCenterSum = range[excelSheetRow, i];
//                            headerRangeParentCostCenterSum[(1), (1)] = "=SUM(" + startColumnLetter + "" +
//                                                                       (excelSheetRow - dataTableByDep.Count()) + ":" +
//                                                                       endColumnLetter + "" +
//                                                                       (excelSheetRow - 1) + ")";

//                            headerRangeParentCostCenterSum[(1), (1)].Interior.Color =
//                                System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CCFFCC"));
//                        }

//                        gr.ColumnNo = excelSheetRow;
//                        listGrandTotalCount.Add(gr);

//                        rowount++;
//                        excelSheetRow++;

//                        #endregion
//                    }

//                }
//                else
//                {

//                    #region Row Binding
//                    var dataTableByDep = from row in dt.AsEnumerable()
//                                         where
//                                             row.Field<string>("Department") == departmnentName &&
//                                             row.Field<string>("BranchName") == branchName &&
//                                              row.Field<string>("CompanyName") == companyName
//                                         select row;

//                    var datadep = dataTableByDep.AsDataView().ToTable();

//                    for (iRow = 0; iRow < datadep.Rows.Count; iRow++)
//                    {
//                        for (iCol = 0; iCol < datadep.Columns.Count; iCol++)
//                        {
//                            if (iCol == 0)
//                            {
//                                range[(excelSheetRow), (iCol + 1)] = sl;
//                            }

//                            if (datadep.Columns[iCol].ColumnName.Equals("TIN"))
//                            {
//                                range[(excelSheetRow), (iCol + 2)] = "'" + datadep.Rows[iRow][iCol].ToString();
//                            }
//                            else
//                            {
//                                range[(excelSheetRow), (iCol + 2)] = datadep.Rows[iRow][iCol].ToString();
//                            }
//                            if (iCol > 11)
//                            {
//                                range[(excelSheetRow), (iCol + 2)].NumberFormat = "#,###,###";

//                            }
//                            range[(excelSheetRow), (iCol + 2)].Font.Name = "Verdana";
//                        }
//                        excelSheetRow++;
//                        sl++;
//                    }

//                    #endregion

//                    #region Add SubTotaal Text

//                    var rgsub = range[excelSheetRow, 3];
//                    range[excelSheetRow, 3] = "Sub Total";
//                    range.Font.Bold = true;
//                    range[excelSheetRow, 3].Interior.Color =
//                      System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CCFFCC"));

//                    #endregion

//                    #region Add Summation

//                    for (int i = 14; i <= dt.Columns.Count; i++)
//                    {
//                        var colcount = dt.Columns;

//                        string startColumnLetter = objAzExport.ExcelColumnLetter(i - 1);
//                        string endColumnLetter = objAzExport.ExcelColumnLetter(i - 1);
//                        var headerRangeParentCostCenterSum = range[excelSheetRow, i];
//                        headerRangeParentCostCenterSum[(1), (1)] = "=SUM(" + startColumnLetter + "" +
//                                                                   (excelSheetRow - dataTableByDep.Count()) + ":" +
//                                                                   endColumnLetter + "" +
//                                                                   (excelSheetRow - 1) + ")";

//                        headerRangeParentCostCenterSum[(1), (1)].Font.Size = 14;
//                        headerRangeParentCostCenterSum[(1), (1)].Interior.Color =
//                          System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CCFFCC"));
//                    }

//                    gr.ColumnNo = excelSheetRow;
//                    listGrandTotalCount.Add(gr);

//                    rowount++;
//                    excelSheetRow++;

//                    #endregion

//                }

//                locationName = branchName;

//                iCol++;
//            }

//            #endregion

//            #region Add Grade Total Text

//            var rggrand = range[excelSheetRow, 3];
//            range[excelSheetRow, 3] = "Grand Total";
//            range.Font.Bold = true;
//            range[excelSheetRow, 3].Interior.Color =
//               System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#ff3399"));

//            #endregion

//            #region Grade Total Summation
//            for (int i = 11; i <= dt.Columns.Count; i++)
//            {
//                var colcount = dt.Columns;
//                string startColumnLetter = objAzExport.ExcelColumnLetter(i - 1);
//                string column = "";
//                foreach (var grandTotalCount in listGrandTotalCount)
//                {
//                    column += column == ""
//                        ? startColumnLetter + "" + grandTotalCount.ColumnNo
//                        : "+" + startColumnLetter + "" + grandTotalCount.ColumnNo;
//                }
//                if (column != "")
//                {
//                    column = "=SUM(" + column + ")";
//                }
//                var headerRangeParentCostCenterSum = range[excelSheetRow, i];
//                headerRangeParentCostCenterSum[(1), (1)] = column;
//                headerRangeParentCostCenterSum[(1), (1)].Interior.Color =
//                   System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#ff3399"));
//            }
//            #endregion

//            return excelSheetRow;
//        }



//        public void ExportToExcelAttendanceSheetRecruitment(string filePath, int rowcout, string companyName, string montherCompany, int columnScape, bool istotal, string headerText, SqlDataReader reader, string location, string appperiod, int reportType, string companyShortNameList)
//        {
//            //   ds = receiveDS;
//            strFileName = filePath;
//            try
//            {
//                application = new Application();
//                application.Visible = false;//true;
//                application.DisplayAlerts = false;

//                workbooks = application.Workbooks;
//                workbook = (Workbook)application.Workbooks.Add(1);
//                workbook.SaveAs(strFileName, XlFileFormat.xlWorkbookNormal, null, null, false, false, XlSaveAsAccessMode.xlExclusive, false, false, null, null, false);

//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //range = worksheet.Cells;

//                Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);
//                #region Main Header


//                var headerRange1 = XLsheet.Range[XLsheet.Cells[1, 1], XLsheet.Cells[rowcout - 2, 6]];
//                headerRange1.Font.Bold = true;
//                headerRange1.Font.Size = 16;
//                headerRange1.Merge(Type.Missing);
//                headerRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                string picPath = System.Web.HttpContext.Current.Server.MapPath("../Images/report-logo.png");
//                object missing = System.Reflection.Missing.Value;
//                Range picPosition = headerRange1;
//                Pictures p = XLsheet.Pictures(missing) as Microsoft.Office.Interop.Excel.Pictures;
//                Picture pic = null;
//                try
//                {
//                    pic = p.Insert(picPath, missing);
//                    pic.Left = Convert.ToDouble(picPosition.Left);
//                    pic.Top = picPosition.Top;
//                }
//                catch (Exception)
//                {
//                    picPath = System.Web.HttpContext.Current.Server.MapPath("../../Images/report-logo.png");
//                    p = XLsheet.Pictures(missing);
//                    pic = p.Insert(picPath, missing);
//                    pic.Left = Convert.ToDouble(picPosition.Left);
//                    pic.Top = picPosition.Top;
//                }

//                if (companyName != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 5, 8], XLsheet.Cells[rowcout - 5, 8]];
//                    var headerRange3 = XLsheet.Range[XLsheet.Cells[rowcout - 4, 8], XLsheet.Cells[rowcout - 4, 8]];

//                    headerRange2[1, 1] = companyName;

//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 12;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;



//                    headerRange3[1, 1] = "Sector-A";
//                    headerRange3.Font.Bold = true;
//                    headerRange3.Font.Size = 12;
//                    headerRange3.Merge(Type.Missing);
//                    headerRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

//                }


//                //if (appperiod != "")
//                //{
//                //    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 3, 8], XLsheet.Cells[rowcout - 3, 25]];
//                //    headerRange2[1, 1] = appperiod;
//                //    headerRange2.Font.Bold = true;
//                //    headerRange2.Font.Size = 12;
//                //    headerRange2.Merge(Type.Missing);
//                //    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //}

//                if (headerText != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 2, 8], XLsheet.Cells[rowcout - 2, 8]];
//                    headerRange2[1, 1] = headerText + " ( " + companyShortNameList + " )";
//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 12;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                }



//                #endregion

//                int excelSheetRow = 0;
//                //int excelSheetRow = this.WriteDataToTheSpecifiedRange(range, rowcout, reader);

//                excelSheetRow = this.WriteDataToTheSpecifiedRangeRecruitment(range, 5, reader);

//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

//                XLsheet.Columns.AutoFit();

//                XLsheet.Name = ds.Tables[0].TableName;
//                workbook.Save();

//            }
//            catch (Exception exp)
//            {
//                throw exp;
//            }
//            finally
//            {
//                DisposeExcelObject();
//            }
//        }


//        public void ExportToExcelRecAssement(string filePath, int rowcout, string companyName, string designationName, int columnScape, bool istotal, string headerText,
//    SqlDataReader reader, int assemblyId, string colourCode,string vanue,DateTime interviewDate)
//        {
//            //   ds = receiveDS;
//            strFileName = filePath;
//            try
//            {
//                application = new Application();
//                application.Visible = false;//true;
//                application.DisplayAlerts = false;

//                workbooks = application.Workbooks;
//                workbook = (Workbook)application.Workbooks.Add(1);
//                workbook.SaveAs(strFileName, XlFileFormat.xlWorkbookNormal, null, null, false, false, XlSaveAsAccessMode.xlExclusive, false, false, null, null, false);

//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //range = worksheet.Cells;

//                Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);
//                #region Main Header
//                if (companyName!=null && companyName != "")
//                {
//                    var headerRangeCompany = XLsheet.Range[XLsheet.Cells[rowcout - 4, 1], XLsheet.Cells[rowcout - 4, 10]];
//                    headerRangeCompany[1, 1] = companyName;// "Salary Control Register for the Month of " + salaryMonth.ToString("MMMM-yyyy");//companyAddress;
//                    headerRangeCompany.Font.Bold = true;
//                    headerRangeCompany.Font.Size = 12;
//                    headerRangeCompany.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("Gray"));
//                    headerRangeCompany.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("white"));
//                    headerRangeCompany.Merge(Type.Missing);
//                    headerRangeCompany.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
//                }

//                if (designationName != null && designationName != "")
//                {
//                    var headerRangeDes = XLsheet.Range[XLsheet.Cells[rowcout - 3, 1], XLsheet.Cells[rowcout - 3, 10]];
//                    headerRangeDes[1, 1] = designationName;// "Salary Control Register for the Month of " + salaryMonth.ToString("MMMM-yyyy");//companyAddress;
//                    headerRangeDes.Font.Bold = true;
//                    headerRangeDes.Font.Size = 12;
//                    headerRangeDes.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("Gray"));
//                    headerRangeDes.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("white"));
//                    headerRangeDes.Merge(Type.Missing);
//                    headerRangeDes.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                }
//                if (vanue != null && vanue != "")
//                {
//                    var headerRangevanue = XLsheet.Range[XLsheet.Cells[rowcout - 2, 1], XLsheet.Cells[rowcout - 2, 10]];
//                    headerRangevanue[1, 1] = vanue;// "Salary Control Register for the Month of " + salaryMonth.ToString("MMMM-yyyy");//companyAddress;
//                    headerRangevanue.Font.Bold = true;
//                    headerRangevanue.Font.Size = 12;
//                    headerRangevanue.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("Gray"));
//                    headerRangevanue.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("white"));
//                    headerRangevanue.Merge(Type.Missing);
//                    headerRangevanue.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
//                }

//                if (interviewDate != null && interviewDate != DateTime.MinValue)
//                {
//                    var headerRangeinterviewDate = XLsheet.Range[XLsheet.Cells[rowcout - 1, 1], XLsheet.Cells[rowcout - 1, 10]];
//                    headerRangeinterviewDate[1, 1] ="Interview Date: "+ interviewDate.ToString("MMM dd, yyyy");// "Salary Control Register for the Month of " + salaryMonth.ToString("MMMM-yyyy");//companyAddress;
//                    headerRangeinterviewDate.Font.Bold = true;
//                    headerRangeinterviewDate.Font.Size = 12;
//                    headerRangeinterviewDate.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("Gray"));
//                    headerRangeinterviewDate.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("white"));
//                    headerRangeinterviewDate.Merge(Type.Missing);
//                    headerRangeinterviewDate.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
//                }

//                #endregion


//                int excelSheetRow = this.WriteDataToTheSpecifiedRangeItk(range, rowcout, reader, assemblyId, colourCode);

//                //if (istotal)
//                //{

//                //    var colCount = ds.Tables[0].Columns.Count;
//                //    var dataRowCount = ds.Tables[0].Rows.Count;
//                //    var headerRangeTotal = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount + 1, 1], XLsheet.Cells[rowcout + dataRowCount + 1, columnScape]];
//                //    headerRangeTotal[1, 1] = "Total";
//                //    headerRangeTotal.Font.Bold = true;
//                //    headerRangeTotal.Font.Size = 12;
//                //    headerRangeTotal.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colourCode));
//                //    headerRangeTotal.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                //    headerRangeTotal.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                //    headerRangeTotal.Merge(Type.Missing);
//                //    headerRangeTotal.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //    //int i = columnScape - 1;

//                //    for (int i = 0; i < (colCount - columnScape) + 1; i++)
//                //    {
//                //        //var col=
//                //        var rangeTotal = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1], XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1]];
//                //        string startColumnLetter = ExcelColumnLetter(i + columnScape);
//                //        string endColumnLetter = ExcelColumnLetter(i + columnScape);

//                //        if (listCols[i].DataType == typeof(Decimal) || listCols[i].DataType == typeof(double) || listCols[i].DataType == typeof(int))
//                //        {
//                //            rangeTotal[1, 1] = "=SUM(" + startColumnLetter + "" + (rowcout + 1) + ":" + endColumnLetter + "" + (rowcout + dataRowCount) + ")";
//                //        }

//                //        rangeTotal[1, 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colourCode));
//                //        rangeTotal[1, 1].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                //        rangeTotal[1, 1].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);

//                //    }
//                //}


//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                // XLsheet.PageSetup.PrintTitleRows = "$1:$5";
//                //XLsheet.PageSetup.LeftFooter = "-------------------------------------\n &BPrepared By&B  \n " + "Rapid 2.0| Payroll | Print Date, Time && Page No : " + DateTime.Now.ToString("MMMM dd, yyyy") + " | " + DateTime.Now.ToString("h:mm tt") + " | " + "Page &P of &N";
//                //XLsheet.PageSetup.BottomMargin = application.InchesToPoints(1.5);
//                //XLsheet.PageSetup.CenterFooter = "-------------------------------------\n &BChecked By&B \n";
//                //XLsheet.PageSetup.RightFooter = "------------------\n &BApproved By&B \n";

//                XLsheet.Columns.AutoFit();

//                XLsheet.Name = ds.Tables[0].TableName;
//                workbook.Save();

//            }
//            catch (Exception exp)
//            {
//                throw exp;
//            }
//            finally
//            {
//                DisposeExcelObject();
//            }
//        }


//    }

//}
