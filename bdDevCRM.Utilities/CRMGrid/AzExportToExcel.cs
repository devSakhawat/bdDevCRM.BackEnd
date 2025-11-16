//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Diagnostics;
//using System.Drawing;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using Microsoft.Office.Interop.Excel;


//namespace Utilities
//{
//    public class AzExportToExcel
//    {
//        private DataSet ds = new DataSet();
//        private Application application;
//        private Workbooks workbooks;
//        private Workbook workbook;
//        private Sheets sheets;
//        private Worksheet worksheet;
//        private Range range;
//        private Image myImage;

//        private string strFileName = "";


//        public void GenerateExcelForBooking(DataSet receiveDS, string filePath, int rowcout, string headerText)
//        {
//            ds = receiveDS;
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
//                int excelSheetRow = this.WriteDataToTheSpecifiedRange(ds.Tables[0], range, rowcout);
//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                var headerRange3 = range.Range[range.Cells[rowcout - 1, 1], range.Cells[rowcout - 1, 20]];
//                headerRange3[1, 1] = headerText;
//                // "Salary Control Register for the Month of " + salaryMonth.ToString("MMMM-yyyy");//companyAddress;
//                headerRange3.Font.Bold = true;
//                headerRange3.Font.Size = 15;
//                headerRange3.Merge(Type.Missing);
//                headerRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
//                Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);

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
//        public void GenerateExcel(DataSet receiveDS, string filePath, int rowcout)
//        {
//            ds = receiveDS;
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
//                int excelSheetRow = this.WriteDataToTheSpecifiedRange(ds.Tables[0], range, rowcout);


//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

//                //range = worksheet.Cells;
//                //this.OverWriteDataToTheSpecifiedRange(ds.Tables[skuDataTableIndex], range);

//                //worksheet.get_Range("A1", "AZ1").EntireColumn.AutoFit();
//                //worksheet.SaveAs(strFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
//                //                 Type.Missing, Type.Missing, Type.Missing, Type.Missing);
//                Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);

//                XLsheet.Columns.AutoFit();

//                XLsheet.Name = ds.Tables[0].TableName;
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

//        public void GenerateExcelWithHeader(DataSet receiveDS, string filePath, int rowcout, string companyName,
//            string companyAddress, int columnScape, bool istotal, DateTime salaryMonth, string locationName, string costCenterName, string disbursmentType)
//        {
//            var title = "Salary Sheet for the Month of " + salaryMonth.ToString("MMMM-yyyy");
//            GenerateExcelWithHeader(receiveDS, filePath, rowcout, companyName, companyAddress, columnScape,
//                istotal, title, locationName, costCenterName, disbursmentType);
//        }

//        public void GenerateExcelWithHeader(DataSet receiveDS, string filePath, int rowcout, string companyName,
//            string companyAddress, int columnScape, bool istotal, DateTime salaryMonth)
//        {
//            var title = "Salary Sheet for the Month of " + salaryMonth.ToString("MMMM-yyyy");
//            GenerateExcelWithHeader(receiveDS, filePath, rowcout, companyName, companyAddress, columnScape,
//                istotal, title, "", "", "");
//        }

//        public void GenerateExcelWithHeader(DataSet receiveDS, string filePath, int rowcout, string companyName,
//            string companyAddress, int columnScape, bool istotal, string headerText, string locationName, string costCenterName, string disbursmentType)
//        {
//            ds = receiveDS;
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

//                #region Main Header

//                //if (companyName != "")
//                //{
//                //    var headerRange1 = XLsheet.Range[XLsheet.Cells[1, 1], XLsheet.Cells[rowcout - 2, 6]];
//                //    //headerRange1[1, 1] = companyName;
//                //    headerRange1.Font.Bold = true;
//                //    headerRange1.Font.Size = 16;
//                //    //headerRange1.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                //    //headerRange1.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                //    //headerRange1.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                //    headerRange1.Merge(Type.Missing);
//                //    headerRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //    //var picPath = "G:\\Project\\Asp.Net\\Azolution\\Empress_4_0_0\\Empress_3_0_0\\Images\\" + "report-logo.png";
//                //    string picPath = System.Web.HttpContext.Current.Server.MapPath("/Images/report-logo.png");
//                //    object missing = System.Reflection.Missing.Value;
//                //    Microsoft.Office.Interop.Excel.Range picPosition = headerRange1;
//                //    //GetPicturePosition(); // retrieve the range for picture insert
//                //    Microsoft.Office.Interop.Excel.Pictures p =
//                //        XLsheet.Pictures(missing) as Microsoft.Office.Interop.Excel.Pictures;
//                //    Microsoft.Office.Interop.Excel.Picture pic = null;
//                //    pic = p.Insert(picPath, missing);
//                //    pic.Left = Convert.ToDouble(picPosition.Left);
//                //    pic.Top = picPosition.Top;


//                //}
//                //if (companyName != "")
//                //{
//                //    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 3, 7], XLsheet.Cells[rowcout - 3, 14]];
//                //    headerRange2[1, 1] = companyName; //companyAddress;
//                //    headerRange2.Font.Bold = true;
//                //    headerRange2.Font.Size = 14;
//                //    //headerRange2.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                //    //headerRange2.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                //    //headerRange2.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                //    headerRange2.Merge(Type.Missing);
//                //    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //}

//                //if (costCenterName != "")
//                //{
//                //    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 3, ds.Tables[0].Columns.Count - 5], XLsheet.Cells[rowcout - 3, ds.Tables[0].Columns.Count]];
//                //    headerRange2[1, 1] = costCenterName; //companyAddress;
//                //    headerRange2.Font.Bold = true;
//                //    headerRange2.Font.Size = 14;
//                //    headerRange2.Merge(Type.Missing);
//                //    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
//                //}


//                //if (locationName != "")
//                //{
//                //    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 2, 7], XLsheet.Cells[rowcout - 2, 14]];
//                //    headerRange2[1, 1] = locationName; //companyAddress;
//                //    headerRange2.Font.Bold = true;
//                //    headerRange2.Font.Size = 14;
//                //    headerRange2.Merge(Type.Missing);
//                //    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //}

//                //if (disbursmentType != "")
//                //{
//                //    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 2, ds.Tables[0].Columns.Count-5], XLsheet.Cells[rowcout - 2, ds.Tables[0].Columns.Count]];
//                //    headerRange2[1, 1] = disbursmentType; //companyAddress;
//                //    headerRange2.Font.Bold = true;
//                //    headerRange2.Font.Size = 14;
//                //    headerRange2.Merge(Type.Missing);
//                //    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
//                //}

//                //// if (companyName != "")
//                //// {
//                //var headerRange3 = XLsheet.Range[XLsheet.Cells[rowcout - 1, 1], XLsheet.Cells[rowcout - 1, 6]];
//                //headerRange3[1, 1] = headerText;
//                //    // "Salary Control Register for the Month of " + salaryMonth.ToString("MMMM-yyyy");//companyAddress;
//                //headerRange3.Font.Bold = true;
//                //headerRange3.Font.Size = 14;
//                //headerRange3.Merge(Type.Missing);
//                //headerRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
//                ////}
//                ////var headerRang = XLsheet.get_Range(XLsheet.Cells[1, 1], XLsheet.Cells[1, 43]);

//                #endregion

//                #region Print DateTime

//                //var headerRangeSystem = XLsheet.Range[XLsheet.Cells[rowcout - 1, ds.Tables[0].Columns.Count - 4], XLsheet.Cells[rowcout - 1, ds.Tables[0].Columns.Count]];
//                //headerRangeSystem[1, 1] = "Print From Rapid 2.0";
//                //headerRangeSystem.Font.Bold = true;
//                //headerRangeSystem.Font.Size = 10;
//                //headerRangeSystem.Merge(Type.Missing);
//                //headerRangeSystem.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

//                //var headerRangeDateTimeVal = XLsheet.Range[XLsheet.Cells[rowcout - 2, ds.Tables[0].Columns.Count - 4], XLsheet.Cells[rowcout - 2, ds.Tables[0].Columns.Count]];
//                //headerRangeDateTimeVal[1, 1] = "Print Date & Time : " + DateTime.Now.ToString("MMMM dd, yyyy") + DateTime.Now.ToString("h:mm tt");//+ DateTime.Now.ToString("d-MMM-yyyy HH.mm.ss tt", CultureInfo.InvariantCulture);//DateTime.Now.ToString("dd MMM yyyy hh:mm:ss");//companyAddress;
//                //headerRangeDateTimeVal.Font.Bold = true;
//                //headerRangeDateTimeVal.Font.Size = 10;
//                //headerRangeDateTimeVal.Merge(Type.Missing);
//                //headerRangeDateTimeVal.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

//                #endregion

//                int excelSheetRow = this.WriteDataToTheSpecifiedRange(ds.Tables[0], range, rowcout);

//                if (istotal)
//                {
//                    var colCount = ds.Tables[0].Columns.Count;
//                    var dataRowCount = ds.Tables[0].Rows.Count;
//                    var headerRangeTotal =
//                        XLsheet.Range[
//                            XLsheet.Cells[rowcout + dataRowCount + 1, 1],
//                            XLsheet.Cells[rowcout + dataRowCount + 1, columnScape]];
//                    headerRangeTotal[1, 1] = "Total";
//                    headerRangeTotal.Font.Bold = true;
//                    headerRangeTotal.Font.Size = 14;
//                    headerRangeTotal.Interior.Color =
//                        System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                    headerRangeTotal.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    headerRangeTotal.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    headerRangeTotal.Merge(Type.Missing);
//                    headerRangeTotal.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            
//                    //int i = columnScape - 1;

//                    for (int i = 0; i < (colCount - columnScape) + 1; i++)
//                    {
//                        var rangeTotal =
//                            XLsheet.Range[
//                                XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1],
//                                XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1]];
//                        string startColumnLetter = ExcelColumnLetter(i + columnScape);
//                        string endColumnLetter = ExcelColumnLetter(i + columnScape);
//                        if (ds.Tables[0].Columns[i].DataType == typeof(Decimal) || ds.Tables[0].Columns[i].DataType == typeof(double) || ds.Tables[0].Columns[i].DataType == typeof(int))
//                        {
//                            rangeTotal[1, 1] = "=SUM(" + startColumnLetter + "" + (rowcout + 1) + ":" + endColumnLetter + "" +
//                                               (rowcout + dataRowCount) + ")";
//                        }
//                        rangeTotal[1, 1].Interior.Color =
//                            System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                        rangeTotal[1, 1].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                        rangeTotal[1, 1].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    }
//                }


//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");



//                //XLsheet.PageSetup.PrintTitleRows = "$1:$5";
//                //XLsheet.PageSetup.RightHeader = "Page &P of &N";
//                //int pd = 35;
//                //string pre = "Prepared By";
//                //pre = pre.PadRight(pd);

//                //string check = "Checked By";
//                //check = check.PadRight(pd);

//                //string adt = "Audited By";
//                //adt = adt.PadRight(pd);

//                //string auth = " Authorized by";
//                //auth = auth.PadRight(pd);

//                // XLsheet.PageSetup.BottomMargin = application.InchesToPoints(1.5);

//                XLsheet.Columns.AutoFit();

//                XLsheet.Name = ds.Tables[0].TableName;
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

//        public void GenerateExcelWithHeaderForSalarySheetPDF(DataSet receiveDS, string filePath, int rowcout, string companyName,
//           string companyAddress, int columnScape, bool istotal, DateTime salaryMonth, string locationName, string costCenterName,
//            string disbursmentType, string reportLogo)
//        {
//            var title = "Salary Sheet for the Month of " + salaryMonth.ToString("MMMM-yyyy");
//            GenerateExcelWithHeaderForSalarySheetPDF(receiveDS, filePath, rowcout, companyName, companyAddress, columnScape,
//                istotal, title, locationName, costCenterName, disbursmentType, reportLogo);
//        }
//        public void GenerateExcelWithHeaderForSalarySheetPDF(DataSet receiveDS, string filePath, int rowcout, string companyName,
//           string companyAddress, int columnScape, bool istotal, string headerText, string locationName, string costCenterName, string disbursmentType, string reportLogo)
//        {
//            ds = receiveDS;
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

//                #region Main Header

//                // if (companyName != "")
//                //{
//                var headerRange1 = XLsheet.Range[XLsheet.Cells[1, 1], XLsheet.Cells[rowcout - 2, 6]];
//                //headerRange1[1, 1] = companyName;
//                headerRange1.Font.Bold = true;
//                headerRange1.Font.Size = 30;
//                //headerRange1.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                //headerRange1.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                //headerRange1.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                headerRange1.Merge(Type.Missing);
//                headerRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

//                string picPath = System.Web.HttpContext.Current.Server.MapPath("/Images/report-logo.png");
//                if (reportLogo != "")
//                {
//                    picPath = System.Web.HttpContext.Current.Server.MapPath(reportLogo);
//                }

//                object missing = System.Reflection.Missing.Value;
//                Microsoft.Office.Interop.Excel.Range picPosition = headerRange1;
//                //GetPicturePosition(); // retrieve the range for picture insert
//                Microsoft.Office.Interop.Excel.Pictures p =
//                    XLsheet.Pictures(missing) as Microsoft.Office.Interop.Excel.Pictures;
//                Microsoft.Office.Interop.Excel.Picture pic = null;
//                pic = p.Insert(picPath, missing);
//                pic.Left = Convert.ToDouble(picPosition.Left);
//                pic.Top = picPosition.Top;


//                //  }
//                if (companyName != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 3, 7], XLsheet.Cells[rowcout - 3, 14]];
//                    headerRange2[1, 1] = companyName; //companyAddress;
//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 70;
//                    headerRange2.Rows.RowHeight = 100;
//                    //headerRange2.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                    //headerRange2.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    //headerRange2.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                }

//                if (costCenterName != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 3, ds.Tables[0].Columns.Count - 6], XLsheet.Cells[rowcout - 3, ds.Tables[0].Columns.Count]];
//                    headerRange2[1, 1] = costCenterName; //companyAddress;
//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 65;
//                    headerRange2.Rows.RowHeight = 100;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
//                }


//                if (locationName != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 2, 7], XLsheet.Cells[rowcout - 2, 14]];
//                    headerRange2[1, 1] = locationName; //companyAddress;
//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 70;
//                    headerRange2.Rows.RowHeight = 100;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                }

//                if (disbursmentType != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 2, ds.Tables[0].Columns.Count - 6], XLsheet.Cells[rowcout - 2, ds.Tables[0].Columns.Count]];
//                    headerRange2[1, 1] = disbursmentType; //companyAddress;
//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 65;
//                    headerRange2.Rows.RowHeight = 100;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
//                }

//                // if (companyName != "")
//                // {
//                var headerRange3 = XLsheet.Range[XLsheet.Cells[rowcout - 1, 1], XLsheet.Cells[rowcout - 1, 6]];
//                headerRange3[1, 1] = headerText;
//                // "Salary Control Register for the Month of " + salaryMonth.ToString("MMMM-yyyy");//companyAddress;
//                headerRange3.Font.Bold = true;
//                headerRange3.Font.Size = 70;
//                headerRange3.Rows.RowHeight = 100;
//                headerRange3.Merge(Type.Missing);
//                headerRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
//                //}
//                //var headerRang = XLsheet.get_Range(XLsheet.Cells[1, 1], XLsheet.Cells[1, 43]);

//                #endregion



//                int excelSheetRow = this.WriteDataToTheSpecifiedRangeForSalarySheetPDF(ds.Tables[0], range, rowcout);

//                if (istotal)
//                {
//                    var colCount = ds.Tables[0].Columns.Count;
//                    var dataRowCount = ds.Tables[0].Rows.Count;
//                    var headerRangeTotal =
//                        XLsheet.Range[
//                            XLsheet.Cells[rowcout + dataRowCount + 1, 1],
//                            XLsheet.Cells[rowcout + dataRowCount + 1, columnScape]];
//                    headerRangeTotal[1, 1] = "Total";
//                    headerRangeTotal.Font.Bold = true;
//                    headerRangeTotal.Font.Size = 50;
//                    headerRangeTotal.Interior.Color =
//                        System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                    headerRangeTotal.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    headerRangeTotal.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    headerRangeTotal.Merge(Type.Missing);
//                    headerRangeTotal.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                    //int i = columnScape - 1;

//                    for (int i = 0; i < (colCount - columnScape) + 1; i++)
//                    {
//                        var rangeTotal =
//                            XLsheet.Range[
//                                XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1],
//                                XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1]];
//                        string startColumnLetter = ExcelColumnLetter(i + columnScape);
//                        string endColumnLetter = ExcelColumnLetter(i + columnScape);
//                        rangeTotal[1, 1] = "=SUM(" + startColumnLetter + "" + (rowcout + 1) + ":" + endColumnLetter + "" +
//                                           (rowcout + dataRowCount) + ")";
//                        rangeTotal[1, 1].Interior.Color =
//                            System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                        rangeTotal[1, 1].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                        rangeTotal[1, 1].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                        rangeTotal[1, 1].Font.Size = 50;

//                    }

//                    headerRangeTotal.Rows.RowHeight = 60;
//                    headerRangeTotal.Rows.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                    headerRangeTotal.Rows.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                }


//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");


//                XLsheet.PageSetup.PrintTitleRows = "$1:$5";

//                XLsheet.PageSetup.LeftFooter = "&50 &BPrepared By                      Checked By&B  \n " + "Empress| Payroll | Print Date, Time && Page No : " + DateTime.Now.ToString("MMMM dd, yyyy") + " | " + DateTime.Now.ToString("h:mm tt") + " | " + "Page &P of &N";
//                XLsheet.PageSetup.CenterFooter = "&50 &BAudited By                 Authorized by&B \n";
//                XLsheet.PageSetup.RightFooter = "&50 &BAuthorized by               Authorized by&B \n";

//                XLsheet.Columns.AutoFit();

//                XLsheet.Name = ds.Tables[0].TableName;
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

//        private int WriteDataToTheSpecifiedRange(System.Data.DataTable dt, Range range, int rowount)
//        {
//            int iRow;
//            int iCol;
//            //Traverse through the DataTable columns to write the headers on the first row of the excel sheet.
//            for (iCol = 0; iCol < dt.Columns.Count; iCol++)
//            {
//                //range = (Range)worksheet.Cells[1, (iCol + 1)];
//                //range.Value2 = dt.Columns[iCol].ColumnName;
//                if (iCol == 0)
//                {
//                    range[rowount, (iCol + 1)] = "SL";
//                    range.WrapText = false;
//                    range.Font.Bold = true;
//                    range[rowount, (iCol + 1)].Font.Size = 14;



//                    range[rowount, (iCol + 1)].Interior.Color =
//                        System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                    range[rowount, (iCol + 1)].Borders.LineStyle =
//                        Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    range[rowount, (iCol + 1)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                }
//                range[rowount, (iCol + 2)] = iCol < 4
//                    ? Regex.Replace(dt.Columns[iCol].ColumnName, "([a-z])_?([A-Z])", "$1 $2")
//                    : dt.Columns[iCol].ColumnName;
//                range.WrapText = false;
//                range.Font.Bold = true;
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                range[rowount, (iCol + 2)].Font.Size = 14;

//                range[rowount, (iCol + 2)].Interior.Color =
//                    System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                range[rowount, (iCol + 2)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;


//                range[rowount, (iCol + 2)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);

//                if (iCol > 5)
//                {
//                    range[rowount, (iCol + 2)].Style.WrapText = true;
//                    //range[rowount, (iCol + 1)].EntireRow.AutoFit();
//                    //range.EntireColumn.AutoFit();
//                }
//                else
//                {
//                    range[rowount, (iCol + 2)].Style.WrapText = false;
//                }


//            }



//            //Traverse through the rows and columns of the datatable to write the data in the sheet.
//            int excelSheetRow = rowount + 1;
//            int sl = 1;
//            for (iRow = 0; iRow < dt.Rows.Count; iRow++)
//            {
//                for (iCol = 0; iCol < dt.Columns.Count; iCol++)
//                {
//                    if (iCol == 0)
//                    {
//                        range[(excelSheetRow), (iCol + 1)] = sl;
//                        range[(excelSheetRow), (iCol + 1)].Borders.LineStyle =
//                            Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                        range[(excelSheetRow), (iCol + 1)].Borders.Color =
//                            System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    }

//                    range[(excelSheetRow), (iCol + 2)] = dt.Rows[iRow][iCol].ToString();
//                    range[(excelSheetRow), (iCol + 2)].Borders.LineStyle =
//                        Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    range[(excelSheetRow), (iCol + 2)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    if (iCol > 5)
//                    {
//                        try
//                        {



//                            decimal ctcAmt = Convert.ToDecimal(dt.Rows[iRow][iCol].ToString());
//                            if (ctcAmt < 0)
//                            {
//                                var nt = Convert.ToDecimal(dt.Rows[iRow]["Net Payout"].ToString());
//                                range[(excelSheetRow), (iCol + 2)] = "(" + dt.Rows[iRow][iCol].ToString() + ")";
//                                if (nt < 0)
//                                {
//                                    dt.Rows[iRow]["Remarks"] = "Adjusted With Bank";
//                                }

//                            }
//                        }
//                        catch
//                        {

//                        }

//                        range[(excelSheetRow), (iCol + 2)].NumberFormat = "#,###,###";
//                        range[(excelSheetRow), (iCol + 2)].WrapText = true;
//                    }
//                    else
//                    {

//                        range[(excelSheetRow), (iCol + 2)].WrapText = false;
//                    }
//                    range[(excelSheetRow), (iCol + 2)].Font.Name = "Verdana";

//                    range[(excelSheetRow), (iCol + 2)].Font.Size = 14;

//                    //if (dt.Columns[iCol].ColumnName == "Material")
//                    //{
//                    //    range[(excelSheetRow), (iCol + 1)] = "'" + dt.Rows[iRow][iCol].ToString();
//                    //}
//                    //else
//                    //{
//                    //    range[(excelSheetRow), (iCol + 1)] = dt.Rows[iRow][iCol].ToString();
//                    //}


//                }
//                excelSheetRow++;
//                sl++;
//            }


//            return excelSheetRow;
//        }

//        private int WriteDataToTheSpecifiedRangeForSalarySheetPDF(System.Data.DataTable dt, Range range, int rowount)
//        {
//            int iRow;
//            int iCol;
//            //Traverse through the DataTable columns to write the headers on the first row of the excel sheet.
//            for (iCol = 0; iCol < dt.Columns.Count; iCol++)
//            {
//                //range = (Range)worksheet.Cells[1, (iCol + 1)];
//                //range.Value2 = dt.Columns[iCol].ColumnName;
//                if (iCol == 0)
//                {
//                    range[rowount, (iCol + 1)] = "SL";
//                    range.WrapText = false;
//                    range.Font.Bold = true;
//                    range[rowount, (iCol + 1)].Font.Size = 50;



//                    range[rowount, (iCol + 1)].Interior.Color =
//                        System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                    range[rowount, (iCol + 1)].Borders.LineStyle =
//                        Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    range[rowount, (iCol + 1)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    range[rowount, (iCol + 1)].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                }
//                range[rowount, (iCol + 2)] = iCol < 4
//                    ? Regex.Replace(dt.Columns[iCol].ColumnName, "([a-z])_?([A-Z])", "$1 $2")
//                    : dt.Columns[iCol].ColumnName;
//                range.WrapText = false;
//                range.Font.Bold = true;
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                range[rowount, (iCol + 2)].Font.Size = 50;
//                range[rowount, (iCol + 2)].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                range[rowount, (iCol + 2)].Interior.Color =
//                    System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                range[rowount, (iCol + 2)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;


//                range[rowount, (iCol + 2)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);

//                if (iCol > 10)
//                {
//                    range[rowount, (iCol + 2)].Style.WrapText = true;
//                    //range[rowount, (iCol + 1)].EntireRow.AutoFit();
//                    //range.EntireColumn.AutoFit();
//                }
//                else
//                {
//                    range[rowount, (iCol + 2)].Style.WrapText = false;
//                }


//            }



//            //Traverse through the rows and columns of the datatable to write the data in the sheet.
//            int excelSheetRow = rowount + 1;
//            int sl = 1;
//            for (iRow = 0; iRow < dt.Rows.Count; iRow++)
//            {
//                for (iCol = 0; iCol < dt.Columns.Count; iCol++)
//                {
//                    if (iCol == 0)
//                    {
//                        range[(excelSheetRow), (iCol + 1)] = sl;
//                        range[(excelSheetRow), (iCol + 1)].Borders.LineStyle =
//                            Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                        range[(excelSheetRow), (iCol + 1)].Borders.Color =
//                            System.Drawing.ColorTranslator.ToOle(Color.Black);

//                        range[(excelSheetRow), (iCol + 1)].Font.Size = 50;
//                        range[(excelSheetRow), (iCol + 1)].Font.Bold = true;
//                        // range[(excelSheetRow), (iCol + 1)].Font.Size = 12;
//                    }

//                    range[(excelSheetRow), (iCol + 2)] = dt.Rows[iRow][iCol].ToString();
//                    range[(excelSheetRow), (iCol + 2)].Borders.LineStyle =
//                        Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    range[(excelSheetRow), (iCol + 2)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    if (iCol > 5)
//                    {
//                        range[(excelSheetRow), (iCol + 2)].NumberFormat = "#,###,###";
//                        range[(excelSheetRow), (iCol + 2)].WrapText = true;
//                    }
//                    else
//                    {

//                        range[(excelSheetRow), (iCol + 2)].WrapText = false;
//                    }

//                    range[(excelSheetRow), (iCol + 2)].Font.Size = 50;
//                    range[(excelSheetRow), (iCol + 2)].Font.Bold = true;
//                    // range[(excelSheetRow), (iCol + 2)].Font.Size = 12;


//                }
//                range.Rows[excelSheetRow].RowHeight = 200;
//                range.Rows[excelSheetRow].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                range.Rows[excelSheetRow].VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

//                excelSheetRow++;
//                sl++;
//            }


//            return excelSheetRow;
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

//        public void GenerateExcelWithHeaderForPdf(DataSet receiveDS, string filePath, int rowcout, string companyName,
//            string companyAddress, int columnScape, bool istotal, int itemPerPage)
//        {
//            ds = receiveDS;
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

//                Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);


//                int excelSheetRow = this.WriteDataToTheSpecifiedRangeForPdf(ds.Tables[0], range, rowcout, itemPerPage,
//                    XLsheet, rowcout, companyName, columnScape);

//                //if (istotal)
//                //{

//                //    var colCount = ds.Tables[0].Columns.Count;
//                //    var dataRowCount = ds.Tables[0].Rows.Count;
//                //    var headerRangeTotal = XLsheet.Range[XLsheet.Cells[excelSheetRow + 1, 1], XLsheet.Cells[excelSheetRow + 1, columnScape]];
//                //    headerRangeTotal[1, 1] = "Total";
//                //    headerRangeTotal.Font.Bold = true;
//                //    headerRangeTotal.Font.Size = 12;
//                //    headerRangeTotal.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                //    headerRangeTotal.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                //    headerRangeTotal.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                //    headerRangeTotal.Merge(Type.Missing);
//                //    headerRangeTotal.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                //    //int i = columnScape - 1;

//                //    for (int i = 0; i < colCount - columnScape; i++)
//                //    {

//                //        //var rangeTotal = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1], XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1]];
//                //        var rangeTotal = XLsheet.Range[XLsheet.Cells[excelSheetRow + 1, i + columnScape + 1], XLsheet.Cells[excelSheetRow + 1, i + columnScape + 1]];
//                //        string startColumnLetter = ExcelColumnLetter(i + columnScape);
//                //        string endColumnLetter = ExcelColumnLetter(i + columnScape);
//                //        rangeTotal[1, 1] = "=SUM(" + startColumnLetter + "" + (rowcout + 1) + ":" + endColumnLetter + "" + (rowcout + dataRowCount) + ")";
//                //        rangeTotal[1, 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                //        rangeTotal[1, 1].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                //        rangeTotal[1, 1].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);

//                //    }
//                //}


//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                XLsheet.PageSetup.PrintTitleRows = "$1:$6";
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

//        private int WriteDataToTheSpecifiedRangeForPdf(System.Data.DataTable dt, Range range, int rowount,
//            int itemPerPage, Worksheet XLsheet, int rowcout, string companyName, int columnScape)
//        {
//            int iRow;
//            int iCol;

//            rowount = 1;

//            int excelSheetRow = rowount;
//            int pageSize = itemPerPage;
//            var totalPages = 0;
//            if (dt.Rows.Count % pageSize == 0)
//            {
//                totalPages = dt.Rows.Count / pageSize;
//            }
//            else
//            {
//                totalPages = (dt.Rows.Count / pageSize) + 1;
//            }
//            for (int i = 0; i < totalPages; i++)
//            {
//                #region Main Header

//                var headerRange1 = XLsheet.Range[XLsheet.Cells[excelSheetRow, 1], XLsheet.Cells[(excelSheetRow + 3), 6]];
//                //rowount + pageSize * i
//                headerRange1.Font.Bold = true;
//                headerRange1.Font.Size = 16;
//                //headerRange1.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                // headerRange1.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                headerRange1.Merge(Type.Missing);
//                headerRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                string picPath = System.Web.HttpContext.Current.Server.MapPath("../Images/report-logo.png");
//                object missing = System.Reflection.Missing.Value;
//                Microsoft.Office.Interop.Excel.Range picPosition = headerRange1;
//                //GetPicturePosition(); // retrieve the range for picture insert
//                Microsoft.Office.Interop.Excel.Pictures p =
//                    XLsheet.Pictures(missing) as Microsoft.Office.Interop.Excel.Pictures;
//                Microsoft.Office.Interop.Excel.Picture pic = null;
//                pic = p.Insert(picPath, missing);
//                pic.Left = Convert.ToDouble(picPosition.Left);
//                pic.Top = picPosition.Top;
//                headerRange1.Style.Height = 84;

//                #endregion

//                #region Print DateTime

//                var headerRangeDateTimeVal =
//                    XLsheet.Range[
//                        XLsheet.Cells[excelSheetRow + 2, ds.Tables[0].Columns.Count - 4],
//                        XLsheet.Cells[excelSheetRow + 2, ds.Tables[0].Columns.Count]];
//                headerRangeDateTimeVal[1, 1] = "Print Date & Time : " +
//                                               DateTime.Now.ToString("d-MMM-yyyy HH.mm.ss tt",
//                                                   CultureInfo.InvariantCulture);
//                headerRangeDateTimeVal[2, 4] = "Page " + (i + 1) + " of " + totalPages;
//                headerRangeDateTimeVal.Font.Bold = true;
//                headerRangeDateTimeVal.Font.Size = 14;
//                headerRangeDateTimeVal.Merge(Type.Missing);
//                headerRangeDateTimeVal.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

//                #endregion

//                excelSheetRow = excelSheetRow + 5;
//                for (iCol = 0; iCol < dt.Columns.Count; iCol++)
//                {
//                    range[excelSheetRow, (iCol + 1)] = iCol < 4
//                        ? Regex.Replace(dt.Columns[iCol].ColumnName, "([a-z])_?([A-Z])", "$1 $2")
//                        : dt.Columns[iCol].ColumnName;
//                    range.WrapText = false;
//                    range.Font.Bold = true;
//                    range[excelSheetRow, (iCol + 1)].Font.Size = 14;
//                    range[excelSheetRow, (iCol + 1)].Interior.Color =
//                        System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                    range[excelSheetRow, (iCol + 1)].Borders.LineStyle =
//                        Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    range[excelSheetRow, (iCol + 1)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    if (iCol > 3)
//                    {
//                        range[excelSheetRow, (iCol + 1)].Style.WrapText = true;
//                    }
//                    else
//                    {
//                        range[excelSheetRow, (iCol + 1)].Style.WrapText = false;
//                    }
//                }

//                bool endLoop = false;
//                excelSheetRow = excelSheetRow + 1;
//                for (iRow = i * pageSize; iRow < (i + 1) * pageSize; iRow++)
//                {
//                    for (iCol = 0; iCol < dt.Columns.Count; iCol++)
//                    {
//                        if (iRow > dt.Rows.Count - 1)
//                        {
//                            endLoop = true;
//                            break;
//                        }
//                        range[(excelSheetRow), (iCol + 1)] = dt.Rows[iRow][iCol].ToString();
//                        range[(excelSheetRow), (iCol + 1)].Borders.LineStyle =
//                            Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                        range[(excelSheetRow), (iCol + 1)].Borders.Color =
//                            System.Drawing.ColorTranslator.ToOle(Color.Black);

//                        if (iCol > 3)
//                        {
//                            range[(excelSheetRow), (iCol + 1)].WrapText = true;
//                        }
//                        else
//                        {
//                            range[(excelSheetRow), (iCol + 1)].WrapText = false;
//                        }
//                    }
//                    if (endLoop) break;
//                    excelSheetRow++;
//                }
//                //  excelSheetRow++;
//                //  rowount = rowcout+4;
//            }

//            WriteReportFooter(XLsheet, rowcout, columnScape, excelSheetRow);
//            return excelSheetRow;
//        }

//        private void WriteReportFooter(Worksheet XLsheet, int rowcout, int columnScape, int excelSheetRow)
//        {
//            var colCount = ds.Tables[0].Columns.Count;
//            var dataRowCount = ds.Tables[0].Rows.Count;
//            var headerRangeTotal =
//                XLsheet.Range[XLsheet.Cells[excelSheetRow + 1, 1], XLsheet.Cells[excelSheetRow + 1, columnScape]];
//            headerRangeTotal[1, 1] = "Total";
//            headerRangeTotal.Font.Bold = true;
//            headerRangeTotal.Font.Size = 14;
//            headerRangeTotal.Interior.Color =
//                System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//            headerRangeTotal.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//            headerRangeTotal.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//            headerRangeTotal.Merge(Type.Missing);
//            headerRangeTotal.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//            //int i = columnScape - 1;

//            for (int i = 0; i < colCount - columnScape; i++)
//            {
//                //var rangeTotal = XLsheet.Range[XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1], XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1]];
//                var rangeTotal =
//                    XLsheet.Range[
//                        XLsheet.Cells[excelSheetRow + 1, i + columnScape + 1],
//                        XLsheet.Cells[excelSheetRow + 1, i + columnScape + 1]];
//                string startColumnLetter = ExcelColumnLetter(i + columnScape);
//                string endColumnLetter = ExcelColumnLetter(i + columnScape);
//                rangeTotal[1, 1] = "=SUM(" + startColumnLetter + "" + (rowcout + 1) + ":" + endColumnLetter + "" +
//                                   (rowcout + dataRowCount) + ")";
//                rangeTotal[1, 1].Interior.Color =
//                    System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                rangeTotal[1, 1].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                rangeTotal[1, 1].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//            }
//        }

//        #region ExportToExcel_Sajida
//        public void ExportToExcelSajida(System.Data.DataTable dataTable, string filePath)
//        {
//            Application application = new Application();
//            application.Visible = false;
//            application.DisplayAlerts = false;

//            Workbook workbook = application.Workbooks.Add();
//            Worksheet worksheet = (Worksheet)workbook.Worksheets[1];

//            try
//            {
//                // Write headers with styling
//                for (int i = 0; i < dataTable.Columns.Count; i++)
//                {
//                    string columnName = i < 4 ? Regex.Replace(dataTable.Columns[i].ColumnName, "([a-z])_?([A-Z])", "$1 $2") : dataTable.Columns[i].ColumnName;
//                    worksheet.Cells[1, i + 1].Value = columnName;
//                    ApplyHeaderStyle(worksheet.Cells[1, i + 1]);
//                }

//                // Prepare data array
//                object[,] data = new object[dataTable.Rows.Count, dataTable.Columns.Count];
//                for (int iRow = 0; iRow < dataTable.Rows.Count; iRow++)
//                {
//                    for (int iCol = 0; iCol < dataTable.Columns.Count; iCol++)
//                    {
//                        string columnName = dataTable.Columns[iCol].ColumnName.ToLower();
//                        if (columnName.Contains("bankaccountno") || columnName.Contains("nationalid") || columnName.Contains("routingnumber"))
//                        {
//                            // Prefix the value with an apostrophe to treat it as text
//                            data[iRow, iCol] = "'" + dataTable.Rows[iRow][iCol].ToString();
//                        }
//                        //if (iCol.ToString() == "BankAccountNo" || iCol.ToString() == "Account No" || iCol.ToString() == "AccountNo" || iCol.ToString() == "EmployeeId" || iCol.ToString() == "Routing Number" || iCol.ToString() == "RoutingNumber")
//                        //{
//                        //    data[iRow, iCol] = "'" + dataTable.Rows[iRow][iCol].ToString();

//                        //    //data[iRow, iCol] = dataTable.Rows[iRow][iCol].ToString();
//                        //}
//                        else
//                        {
//                            data[iRow, iCol] = dataTable.Rows[iRow][iCol].ToString();
//                        }
                        
//                    }
//                }

//                // Write data to the worksheet
//                Range dataRange = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[dataTable.Rows.Count + 1, dataTable.Columns.Count]];
//                dataRange.Value = data;

//                // Apply cell styles
//                ApplyCellStyles(dataRange);

//                // Autofit columns
//                Range headerRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, dataTable.Columns.Count]];
//                headerRange.EntireColumn.AutoFit();

//                // Save file
//                workbook.SaveAs(filePath);
//            }
//            catch (Exception ex)
//            {
//                // Handle exception
//                Console.WriteLine("Error: " + ex.Message);
//            }
//            finally
//            {
//                // Cleanup
//                DisposeExcelObject();
//                DisposeExcelObjectSajida(worksheet);
//                DisposeExcelObjectSajida(workbook);
//                DisposeExcelObjectSajida(application);
//                GC.Collect();
//                GC.WaitForPendingFinalizers();
//            }
//        }

//        private void ApplyHeaderStyle(Range cell)
//        {
//            cell.WrapText = false;
//            cell.Font.Bold = true;
//            cell.Font.Size = 14;
//            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//            cell.Borders.LineStyle = XlLineStyle.xlContinuous;
//            cell.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//        }

//        private void ApplyCellStyles(Range range)
//        {
//            range.WrapText = true;
//            range.Font.Size = 14;
//            range.Borders.LineStyle = XlLineStyle.xlContinuous;
//            range.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//        }

//        private void DisposeExcelObjectSajida(object obj)
//        {
//            try
//            {
//                if (obj != null)
//                {
//                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
//                    obj = null;
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Error releasing Excel object: " + ex.Message);
//            }
//        }
//        #endregion ExportToExcel_Sajida

//        public void ExportToExcel(System.Data.DataTable dataTable, string filePath)
//        {
//            strFileName = filePath;
//            try
//            {
//                int rowcout = dataTable.Rows.Count;

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
//                int excelSheetRow = this.WriteDataToTheSpecifiedRange(dataTable, range, 1);

//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


//                Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);

//                XLsheet.Columns.AutoFit();

//                XLsheet.Name = "Sheet1";
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

//        public void GenerateExcelWithHeaderForReportEnginePdf(DataSet receiveDS, string filePath, int rowcout,
//            string companyName, string companyAddress, int columnScape, bool istotal)
//        {
//            ds = receiveDS;
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

//                #region Main Header

//                var headerRange1 = XLsheet.Range[XLsheet.Cells[1, 1], XLsheet.Cells[rowcout - 2, 6]];

//                headerRange1.Font.Bold = true;
//                headerRange1.Font.Size = 16;
//                headerRange1.Merge(Type.Missing);
//                headerRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                string picPath = "";
//                try
//                {
//                    picPath = System.Web.HttpContext.Current.Server.MapPath("../../Images/report-logo.png");

//                }
//                catch (Exception ex)
//                {
//                    if (ex.Message.Contains(""))
//                    {
//                        picPath = System.Web.HttpContext.Current.Server.MapPath("../Images/report-logo.png");
//                    }

//                }
//                object missing = System.Reflection.Missing.Value;
//                Microsoft.Office.Interop.Excel.Range picPosition = headerRange1;
//                //GetPicturePosition(); // retrieve the range for picture insert
//                Microsoft.Office.Interop.Excel.Pictures p =
//                    XLsheet.Pictures(missing) as Microsoft.Office.Interop.Excel.Pictures;
//                Microsoft.Office.Interop.Excel.Picture pic = null;
//                pic = p.Insert(picPath, missing);
//                pic.Left = Convert.ToDouble(picPosition.Left);
//                pic.Top = picPosition.Top;


//                //  }
//                if (companyName != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 3, 7], XLsheet.Cells[rowcout - 3, 14]];
//                    headerRange2[1, 1] = companyName;
//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 14;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                }

//                #endregion

//                int excelSheetRow = this.WriteDataToTheSpecifiedRangeForReportEngine(ds.Tables[0], range, rowcout);

//                #region Total Column

//                if (istotal)
//                {
//                    var colCount = ds.Tables[0].Columns.Count;
//                    var dataRowCount = ds.Tables[0].Rows.Count;
//                    var headerRangeTotal =
//                        XLsheet.Range[
//                            XLsheet.Cells[rowcout + dataRowCount + 1, 1],
//                            XLsheet.Cells[rowcout + dataRowCount + 1, columnScape]];
//                    headerRangeTotal[1, 1] = "Total";
//                    headerRangeTotal.Font.Bold = true;
//                    headerRangeTotal.Font.Size = 14;
//                    headerRangeTotal.Interior.Color =
//                        System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                    headerRangeTotal.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    headerRangeTotal.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    headerRangeTotal.Merge(Type.Missing);
//                    headerRangeTotal.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                    //int i = columnScape - 1;

//                    for (int i = 0; i < (colCount - columnScape) + 1; i++)
//                    {
//                        var rangeTotal =
//                            XLsheet.Range[
//                                XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1],
//                                XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1]];
//                        string startColumnLetter = ExcelColumnLetter(i + columnScape);
//                        string endColumnLetter = ExcelColumnLetter(i + columnScape);
//                        rangeTotal[1, 1] = "=SUM(" + startColumnLetter + "" + (rowcout + 1) + ":" + endColumnLetter + "" +
//                                           (rowcout + dataRowCount) + ")";
//                        rangeTotal[1, 1].Interior.Color =
//                            System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                        rangeTotal[1, 1].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                        rangeTotal[1, 1].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    }
//                }

//                #endregion

//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


//                XLsheet.PageSetup.PrintTitleRows = "$1:$5";
//                XLsheet.PageSetup.LeftFooter = "---------------------------\n &BPrepared By&B  \n " +
//                                               "Empress | Report | Print Date, Time && Page No : " +
//                                               DateTime.Now.ToString("MMMM dd, yyyy") + " | " +
//                                               DateTime.Now.ToString("h:mm tt") + " | " + "Page &P of &N";
//                XLsheet.PageSetup.BottomMargin = application.InchesToPoints(1.5);
//                XLsheet.PageSetup.CenterFooter = "---------------------\n &BChecked By&B \n";
//                XLsheet.PageSetup.RightFooter = "------------------\n &BApproved By&B \n";

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

//        public void ExportToCSV(System.Data.DataTable dataTable, string filePath)
//        {
//            strFileName = filePath;
//            try
//            {
//                string cols = dataTable.Columns.Cast<DataColumn>()
//                    .Aggregate("", (current, col) => current + (col.ColumnName + ";"));
//                cols += Environment.NewLine;
//                System.IO.File.AppendAllText(filePath, cols);

//                string rowStr = "";
//                foreach (DataRow row in dataTable.Rows)
//                {
//                    rowStr += dataTable.Columns.Cast<DataColumn>()
//                        .Aggregate("", (current, col) => current + (row[col.ColumnName] + ";"));
//                    rowStr += Environment.NewLine;
//                }

//                System.IO.File.AppendAllText(filePath, rowStr);
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

//        private int WriteDataToTheSpecifiedRangeForReportEngine(System.Data.DataTable dt, Range range, int rowount)
//        {
//            int iRow;
//            int iCol;
//            //Traverse through the DataTable columns to write the headers on the first row of the excel sheet.
//            for (iCol = 0; iCol < dt.Columns.Count; iCol++)
//            {
//                //range = (Range)worksheet.Cells[1, (iCol + 1)];
//                //range.Value2 = dt.Columns[iCol].ColumnName;
//                if (iCol == 0)
//                {
//                    range[rowount, (iCol + 1)] = "SL";
//                    range.WrapText = false;
//                    range.Font.Bold = true;
//                    range[rowount, (iCol + 1)].Font.Size = 14;
//                    range[rowount, (iCol + 1)].Interior.Color =
//                        System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                    range[rowount, (iCol + 1)].Borders.LineStyle =
//                        Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    range[rowount, (iCol + 1)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                }
//                range[rowount, (iCol + 2)] = Regex.Replace(dt.Columns[iCol].ColumnName, "([a-z])_?([A-Z])", "$1 $2");
//                //iCol < 4 ? Regex.Replace(dt.Columns[iCol].ColumnName, "([a-z])_?([A-Z])", "$1 $2") : dt.Columns[iCol].ColumnName;
//                range.WrapText = false;
//                range.Font.Bold = true;
//                range[rowount, (iCol + 2)].Font.Size = 14;
//                range[rowount, (iCol + 2)].Interior.Color =
//                    System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                range[rowount, (iCol + 2)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                range[rowount, (iCol + 2)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                range[rowount, (iCol + 2)].Style.WrapText = false;
//            }


//            //range.AutoFit();

//            //Traverse through the rows and columns of the datatable to write the data in the sheet.
//            int excelSheetRow = rowount + 1;
//            int sl = 1;
//            for (iRow = 0; iRow < dt.Rows.Count; iRow++)
//            {
//                for (iCol = 0; iCol < dt.Columns.Count; iCol++)
//                {
//                    if (iCol == 0)
//                    {
//                        range[(excelSheetRow), (iCol + 1)] = sl;
//                        range[(excelSheetRow), (iCol + 1)].Borders.LineStyle =
//                            Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                        range[(excelSheetRow), (iCol + 1)].Borders.Color =
//                            System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    }

//                    range[(excelSheetRow), (iCol + 2)] = dt.Rows[iRow][iCol].ToString();
//                    range[(excelSheetRow), (iCol + 2)].Borders.LineStyle =
//                        Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    range[(excelSheetRow), (iCol + 2)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    range[(excelSheetRow), (iCol + 2)].WrapText = false;
//                }
//                excelSheetRow++;
//                sl++;
//            }


//            return excelSheetRow;
//        }

//        public void GenerateTextFile(DataSet ds, string physicalPath, DateTime salaryMonth)
//        {
//            var txt = new StringBuilder();

//            string salaryNaraion1 = "SALARY FOR THE MONTH ";
//            string salaryNaraion2 =
//                CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(salaryMonth.Month).ToUpper() + " " +
//                salaryMonth.Year.ToString();
//            salaryNaraion2 = GenerateStringWithBlankSpaceRightSide(salaryNaraion2, 30);

//            var dt = ds.Tables["NetPayoutCredit"];

//            var textHeader =
//                string.Format("10001{0} SALARY & DEDUCTIONS                                              SAL ",
//                    DateTime.Today.ToString("ddMMyyyy"));
//            txt.AppendLine(textHeader);
//            decimal footeramount = 0;
//            int trCount = 0;

//            #region  Net Payoput

//            for (int i = 0; i < dt.Rows.Count; i++)
//            {
//                trCount++;
//                //string accountNo = GenerateStringWithZero(dt.Rows[i]["BankAccountNo"].ToString().Trim(), 10);
//                string accountNo = dt.Rows[i]["BankAccountNo"].ToString().Trim();
//                if (accountNo.Length != 13)
//                {
//                    var ertxt = new StringBuilder();
//                    var invalidMes = string.Format("Account No Length Mismatch-----{0}", accountNo);
//                    ertxt.AppendLine(invalidMes);
//                    File.WriteAllText(physicalPath, ertxt.ToString());
//                    return;
//                }

//                decimal netPay = Convert.ToDecimal(dt.Rows[i]["NetPay"]);
//                var amountSplit = netPay.ToString().Split('.');
//                string amount = GenerateStringWithZero(Convert.ToInt32(amountSplit[0]).ToString(), 11);
//                string decimalPart = amountSplit[1].ToString();
//                string prefix = "3";
//                footeramount += netPay;
//                var newLine = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
//                    prefix,
//                    accountNo,
//                    salaryNaraion1,
//                    salaryNaraion2,
//                    amount,
//                    decimalPart,
//                    "C",
//                    "BDT",
//                    "521 ", Environment.NewLine);
//                txt.Append(newLine);
//            }

//            #endregion

//            #region Expat Tax

//            var dtExpatTax = ds.Tables["ExPatTax"];
//            for (int i = 0; i < dtExpatTax.Rows.Count; i++)
//            {
//                salaryNaraion1 = string.Format(@"EXPAT TAX BOTH FOR THE MONTH ");
//                salaryNaraion2 = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(salaryMonth.Month).ToUpper() +
//                                 " " + salaryMonth.Year.ToString();
//                var finalSalaryNaration = salaryNaraion1 + salaryNaraion2;

//                finalSalaryNaration = GenerateStringWithBlankSpaceRightSide(finalSalaryNaration, 51);

//                trCount++;
//                //string accountNo = GenerateStringWithZero(dt.Rows[i]["BankAccountNo"].ToString().Trim(), 10);
//                string accountNo = dtExpatTax.Rows[i]["BankAccountNo"].ToString().Trim();
//                if (accountNo.Length != 13)
//                {
//                    var ertxt = new StringBuilder();
//                    var invalidMes = string.Format("Account No Length Mismatch-----{0}", accountNo);
//                    ertxt.AppendLine(invalidMes);
//                    File.WriteAllText(physicalPath, ertxt.ToString());
//                    return;
//                }

//                decimal netPay = Convert.ToDecimal(dtExpatTax.Rows[i]["NetPay"]);
//                var amountSplit = netPay.ToString().Split('.');
//                string amount = GenerateStringWithZero(Convert.ToInt32(amountSplit[0]).ToString(), 11);
//                string decimalPart = amountSplit[1].ToString();
//                string prefix = "3";
//                footeramount += netPay;
//                var newLine = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
//                    prefix,
//                    accountNo,
//                    finalSalaryNaration,
//                    "",
//                    amount,
//                    decimalPart,
//                    "C",
//                    "BDT",
//                    "521 ", Environment.NewLine);
//                txt.Append(newLine);
//            }

//            #endregion

//            #region Debit Account Head

//            var dtDebit = ds.Tables["ComponentWiseSalaryDebit"];
//            for (int i = 0; i < dtDebit.Rows.Count; i++)
//            {
//                var remunarationName = Convert.ToString(dtDebit.Rows[i]["CtcName"]);
//                salaryNaraion1 = string.Format(@"{0} FOR THE MONTH ", remunarationName.ToUpper());
//                salaryNaraion2 = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(salaryMonth.Month).ToUpper() +
//                                 " " + salaryMonth.Year.ToString();
//                var finalSalaryNaration = salaryNaraion1 + salaryNaraion2;

//                finalSalaryNaration = GenerateStringWithBlankSpaceRightSide(finalSalaryNaration, 51);


//                trCount++;

//                string branchCode = dtDebit.Rows[i]["BranchCode"].ToString().Trim();
//                string glCode = dtDebit.Rows[i]["AccountHeadCode"].ToString().Trim();
//                if (branchCode == "")
//                {
//                    if (glCode.Length != 13)
//                    {
//                        var ertxt = new StringBuilder();
//                        var invalidMes = string.Format("Account No Length Mismatch-----{0}", glCode);
//                        ertxt.AppendLine(invalidMes);
//                        File.WriteAllText(physicalPath, ertxt.ToString());
//                        return;
//                    }
//                }
//                else
//                {
//                    if (branchCode.Length != 4)
//                    {
//                        var ertxt = new StringBuilder();
//                        var invalidMes = string.Format("Cost Centre Length Mismatch-----{0}", branchCode);
//                        ertxt.AppendLine(invalidMes);
//                        File.WriteAllText(physicalPath, ertxt.ToString());
//                        return;
//                    }
//                    if (glCode.Length != 6)
//                    {
//                        var ertxt = new StringBuilder();
//                        var invalidMes = string.Format("Account No Length Mismatch-----{0}", glCode);
//                        ertxt.AppendLine(invalidMes);
//                        File.WriteAllText(physicalPath, ertxt.ToString());
//                        return;
//                    }
//                }

//                decimal ctcValue = Convert.ToDecimal(dtDebit.Rows[i]["CtcValue"]);
//                var amountSplit = ctcValue.ToString().Split('.');
//                string amount = GenerateStringWithZero(Convert.ToInt32(amountSplit[0]).ToString(), 11);
//                string decimalPart = amountSplit[1].ToString();
//                string transectionType = dtDebit.Rows[i]["CtcOperator"].ToString() == "1" ? "D" : "C";
//                string transectionCode = dtDebit.Rows[i]["CtcOperator"].ToString() == "1" ? "010 " : "521 ";
//                footeramount += Convert.ToDecimal(ctcValue);
//                string prefix = "3";
//                string currencyCode = branchCode == "" ? "" : "050";
//                var newLine = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}",
//                    prefix,
//                    branchCode,
//                    glCode,
//                    currencyCode,
//                    finalSalaryNaration,
//                    "",
//                    amount,
//                    decimalPart,
//                    transectionType,
//                    "BDT",
//                    transectionCode, Environment.NewLine);
//                txt.Append(newLine);
//            }

//            #endregion

//            #region Expat PF

//            var dtExpatPf = ds.Tables["ExPatPF"];
//            for (int i = 0; i < dtExpatPf.Rows.Count; i++)
//            {
//                var remunarationName = Convert.ToString(dtExpatPf.Rows[i]["CtcName"]);
//                salaryNaraion1 = string.Format(@"{0} FOR THE MONTH ", remunarationName.ToUpper());
//                salaryNaraion2 = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(salaryMonth.Month).ToUpper() +
//                                 " " + salaryMonth.Year.ToString();
//                var finalSalaryNaration = salaryNaraion1 + salaryNaraion2;

//                finalSalaryNaration = GenerateStringWithBlankSpaceRightSide(finalSalaryNaration, 51);


//                trCount++;

//                string branchCode = dtExpatPf.Rows[i]["BranchCode"].ToString().Trim();
//                string glCode = dtExpatPf.Rows[i]["AccountHeadCode"].ToString().Trim();
//                if (branchCode == "")
//                {
//                    if (glCode.Length != 13)
//                    {
//                        var ertxt = new StringBuilder();
//                        var invalidMes = string.Format("Account No Length Mismatch-----{0}", glCode);
//                        ertxt.AppendLine(invalidMes);
//                        File.WriteAllText(physicalPath, ertxt.ToString());
//                        return;
//                    }
//                }
//                else
//                {
//                    if (branchCode.Length != 4)
//                    {
//                        var ertxt = new StringBuilder();
//                        var invalidMes = string.Format("Cost Centre Length Mismatch-----{0}", branchCode);
//                        ertxt.AppendLine(invalidMes);
//                        File.WriteAllText(physicalPath, ertxt.ToString());
//                        return;
//                    }
//                    if (glCode.Length != 6)
//                    {
//                        var ertxt = new StringBuilder();
//                        var invalidMes = string.Format("Account No Length Mismatch-----{0}", glCode);
//                        ertxt.AppendLine(invalidMes);
//                        File.WriteAllText(physicalPath, ertxt.ToString());
//                        return;
//                    }
//                }

//                decimal ctcValue = Convert.ToDecimal(dtExpatPf.Rows[i]["CtcValue"]);
//                var amountSplit = ctcValue.ToString().Split('.');
//                string amount = GenerateStringWithZero(Convert.ToInt32(amountSplit[0]).ToString(), 11);
//                string decimalPart = amountSplit[1].ToString();
//                string transectionType = dtExpatPf.Rows[i]["CtcOperator"].ToString() == "1" ? "D" : "C";
//                string transectionCode = dtExpatPf.Rows[i]["CtcOperator"].ToString() == "1" ? "010 " : "521 ";
//                footeramount += Convert.ToDecimal(ctcValue);
//                string prefix = "3";
//                string currencyCode = branchCode == "" ? "" : "050";
//                var newLine = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}",
//                    prefix,
//                    branchCode,
//                    glCode,
//                    currencyCode,
//                    finalSalaryNaration,
//                    "",
//                    amount,
//                    decimalPart,
//                    transectionType,
//                    "BDT",
//                    transectionCode, Environment.NewLine);
//                txt.Append(newLine);
//            }

//            #endregion

//            var footerSplit = footeramount.ToString().Split('.');
//            string footerAmount = GenerateStringWithZero(Convert.ToInt32(footerSplit[0]).ToString(), 9);
//            string[] subStrings = new string[] { };
//            if (!string.IsNullOrEmpty(footerAmount))
//            {
//                subStrings = footerAmount.Split(' ');
//            }
//            string footerDecimalPart = "";
//            if (subStrings.Count() > 1)
//            {
//                footerDecimalPart = footerSplit[1].ToString();
//            }

//            string finalFooterAmt = footerDecimalPart + footerAmount;

//            string trCountWithZeroIndex = GenerateStringWithZero(Convert.ToString(trCount), 8);

//            var textFooter = string.Format("20001{1}                                                    {0}00 ",
//                finalFooterAmt, trCountWithZeroIndex);
//            txt.AppendLine(textFooter);
//            File.WriteAllText(physicalPath, txt.ToString());
//        }

//        private string GenerateStringWithZero(string fieldValue, int fieldlength)
//        {
//            string res = "";

//            if (fieldValue.Length < fieldlength)
//            {
//                int dis = fieldlength - fieldValue.Length;
//                for (var i = 0; i < dis; i++)
//                {
//                    res += "0";
//                }
//                res += fieldValue;
//            }
//            else
//            {
//                res = fieldValue;
//            }

//            return res;
//        }

//        private string GenerateStringWithBlankSpaceRightSide(string fieldValue, int fieldlength)
//        {
//            string res = "";

//            if (fieldValue.Length < fieldlength)
//            {
//                int dis = fieldlength - fieldValue.Length;
//                for (var i = 0; i < dis; i++)
//                {
//                    res += " ";
//                }
//                fieldValue += res;
//            }

//            return fieldValue;
//        }


//        public void GenerateTextFileForOtherPayment(DataSet ds, string physicalPath, DateTime salaryMonth,
//            string remunarationName, string naration)
//        {
//            var txt = new StringBuilder();

//            string salaryNaraion1 = remunarationName + " FOR THE MONTH ";
//            string salaryNaraion2 =
//                CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(salaryMonth.Month).ToUpper() + " " +
//                DateTime.Now.Day.ToString() + "," + salaryMonth.Year.ToString();
//            // var totalNaration = salaryNaraion1 + salaryNaraion2;
//            var totalNaration = naration;
//            totalNaration = GenerateStringWithBlankSpaceRightSide(totalNaration, 51);

//            var hdr = GenerateStringWithBlankSpaceRightSide(remunarationName, 19);

//            var dt = ds.Tables["NetPayoutCredit"];

//            var textHeader = string.Format("10001{0} {1}                                    OTHER PAYMENT ",
//                DateTime.Today.ToString("ddMMyyyy"), hdr);
//            txt.AppendLine(textHeader);
//            decimal footeramount = 0;
//            int trCount = 0;
//            for (int i = 0; i < dt.Rows.Count; i++)
//            {
//                trCount++;
//                string accountNo = GenerateStringWithZero(dt.Rows[i]["BankAccountNo"].ToString().Trim(), 10);
//                decimal netPay = Convert.ToDecimal(dt.Rows[i]["NetPay"]);
//                var amountSplit = netPay.ToString().Split('.');
//                string amount = GenerateStringWithZero(Convert.ToInt32(amountSplit[0]).ToString(), 11);
//                string decimalPart = amountSplit[1].ToString();
//                string prefix = "3";
//                footeramount += netPay;
//                var newLine = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
//                    prefix,
//                    accountNo,
//                    "",
//                    totalNaration,
//                    amount,
//                    decimalPart,
//                    "C",
//                    "BDT",
//                    "521 ", Environment.NewLine);
//                txt.Append(newLine);
//            }
//            var dtDebit = ds.Tables["ComponentWiseSalaryDebit"];
//            for (int i = 0; i < dtDebit.Rows.Count; i++)
//            {
//                trCount++;

//                string branchCode = dtDebit.Rows[i]["BranchCode"].ToString().Trim() == ""
//                    ? ""
//                    : GenerateStringWithZero(dtDebit.Rows[i]["BranchCode"].ToString().Trim(), 4);
//                string glCode = GenerateStringWithZero(dtDebit.Rows[i]["AccountHeadCode"].ToString().Trim(), 6);
//                decimal ctcValue = Convert.ToDecimal(dtDebit.Rows[i]["CtcValue"]);
//                var amountSplit = ctcValue.ToString().Split('.');
//                string amount = GenerateStringWithZero(Convert.ToInt32(amountSplit[0]).ToString(), 11);
//                string decimalPart = amountSplit[1].ToString();
//                string transectionType = dtDebit.Rows[i]["CtcOperator"].ToString() == "1" ? "D" : "C";
//                string transectionCode = dtDebit.Rows[i]["CtcOperator"].ToString() == "1" ? "010 " : "521 ";
//                footeramount += Convert.ToDecimal(ctcValue);
//                string prefix = "3";
//                string currencyCode = branchCode == "" ? "" : "050";
//                var newLine = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}",
//                    prefix,
//                    branchCode,
//                    glCode,
//                    currencyCode,
//                    "",
//                    totalNaration,
//                    amount,
//                    decimalPart,
//                    transectionType,
//                    "BDT",
//                    transectionCode, Environment.NewLine);
//                txt.Append(newLine);
//            }
//            var footerSplit = footeramount.ToString().Split('.');
//            string footerAmount = GenerateStringWithZero(Convert.ToInt32(footerSplit[0]).ToString(), 9);
//            string footerDecimalPart = footerSplit[1].ToString();
//            string finalFooterAmt = footerDecimalPart + footerAmount;

//            string trCountWithZeroIndex = GenerateStringWithZero(Convert.ToString(trCount), 8);

//            var textFooter = string.Format("20001{1}                                                    {0}00 ",
//                finalFooterAmt, trCountWithZeroIndex);
//            txt.AppendLine(textFooter);
//            File.WriteAllText(physicalPath, txt.ToString());
//        }

//        public void GenerateExcelForBankInstructionMeghna(DataSet receiveDS, string filePath, int rowcout, DateTime salaryMonth)
//        {
//            ds = receiveDS;
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
//                int excelSheetRow = this.WriteDataToTheSpecifiedRangeBankInstruction(ds.Tables[0], range, rowcout, salaryMonth);


//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

//                Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);

//                XLsheet.Columns.AutoFit();

//                XLsheet.Name = ds.Tables[0].TableName;
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

//        private int WriteDataToTheSpecifiedRangeBankInstruction(System.Data.DataTable dt, Range range, int rowount, DateTime salaryMonth)
//        {
//            int iRow;
//            int iCol;
//            //Traverse through the DataTable columns to write the headers on the first row of the excel sheet.

//            range[rowount, 5] = "Salary for the month of " + salaryMonth.ToString("MMMM-yyyy");

//            #region Column Header
//            //for (iCol = 0; iCol < dt.Columns.Count - 1; iCol++)
//            //{


//            //    if (iCol == 0)
//            //    {
//            //        range[rowount, (iCol + 1)] = "SL No.";
//            //        //range.WrapText = false;
//            //        //range.Font.Bold = true;
//            //        //range[rowount, (iCol + 1)].Font.Size = 14;



//            //        range[rowount, (iCol + 1)].Interior.Color =
//            //            System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//            //        range[rowount, (iCol + 1)].Borders.LineStyle =
//            //            Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//            //        range[rowount, (iCol + 1)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//            //    }
//            //    range[rowount, (iCol + 2)] = iCol < 4
//            //        ? Regex.Replace(dt.Columns[iCol].ColumnName, "([a-z])_?([A-Z])", "$1 $2")
//            //        : dt.Columns[iCol].ColumnName;
//            //    range.WrapText = false;
//            //    range.Font.Bold = true;
//            //    range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//            //    range[rowount, (iCol + 2)].Font.Size = 9;

//            //    range[rowount, (iCol + 2)].Interior.Color =
//            //        System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//            //    range[rowount, (iCol + 2)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;


//            //    range[rowount, (iCol + 2)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);




//            //}
//            #endregion


//            //Traverse through the rows and columns of the datatable to write the data in the sheet.
//            int excelSheetRow = rowount + 1;
//            int sl = 1;
//            var oldBr = "";
//            decimal subtotal = 0;
//            decimal total = 0;
//            #region All Data Row
//            for (iRow = 0; iRow < dt.Rows.Count; iRow++)
//            {
//                var brName = dt.Rows[iRow]["LocationName"].ToString();
//                if (oldBr == "" || oldBr == brName)
//                {
//                    if (oldBr == "")
//                    {
//                        rowount++;
//                        excelSheetRow++;
//                        range[rowount, 1] = brName;

//                        rowount++;
//                        excelSheetRow++;
//                        #region Column Header
//                        for (iCol = 0; iCol < dt.Columns.Count - 1; iCol++)
//                        {


//                            if (iCol == 0)
//                            {
//                                range[rowount, (iCol + 1)] = "SL No.";
//                                //range.WrapText = false;
//                                //range.Font.Bold = true;
//                                //range[rowount, (iCol + 1)].Font.Size = 14;



//                                range[rowount, (iCol + 1)].Interior.Color =
//                                    System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                                range[rowount, (iCol + 1)].Borders.LineStyle =
//                                    Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                                range[rowount, (iCol + 1)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                            }
//                            range[rowount, (iCol + 2)] = iCol < 4
//                                ? Regex.Replace(dt.Columns[iCol].ColumnName, "([a-z])_?([A-Z])", "$1 $2")
//                                : dt.Columns[iCol].ColumnName;
//                            range.WrapText = false;
//                            range.Font.Bold = true;
//                            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                            range[rowount, (iCol + 2)].Font.Size = 9;

//                            range[rowount, (iCol + 2)].Interior.Color =
//                                System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                            range[rowount, (iCol + 2)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;


//                            range[rowount, (iCol + 2)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);




//                        }
//                        #endregion
//                        rowount++;
//                        excelSheetRow++;
//                    }
//                    oldBr = brName;
//                    #region Row and Column

//                    for (iCol = 0; iCol < dt.Columns.Count - 1; iCol++)
//                    {
//                        if (iCol == 0)
//                        {
//                            range[(excelSheetRow), (iCol + 1)] = sl;
//                            range[(excelSheetRow), (iCol + 1)].Borders.LineStyle =
//                                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                            range[(excelSheetRow), (iCol + 1)].Borders.Color =
//                                System.Drawing.ColorTranslator.ToOle(Color.Black);
//                        }

//                        range[(excelSheetRow), (iCol + 2)] = dt.Rows[iRow][iCol].ToString();
//                        range[(excelSheetRow), (iCol + 2)].Borders.LineStyle =
//                            Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                        range[(excelSheetRow), (iCol + 2)].Borders.Color =
//                            System.Drawing.ColorTranslator.ToOle(Color.Black);
//                        if (iCol > 5)
//                        {
//                            range[(excelSheetRow), (iCol + 2)].NumberFormat = "#,###,###";
//                        }
//                        else
//                        {

//                            range[(excelSheetRow), (iCol + 2)].WrapText = false;
//                        }
//                        range[(excelSheetRow), (iCol + 2)].Font.Name = "Verdana";

//                        range[(excelSheetRow), (iCol + 2)].Font.Size = 9;


//                    }

//                    subtotal += Convert.ToDecimal(dt.Rows[iRow]["Total Salary(Tk.)"].ToString());
//                    total += Convert.ToDecimal(dt.Rows[iRow]["Total Salary(Tk.)"].ToString());

//                    excelSheetRow++;
//                    rowount++;
//                    sl++;

//                    #endregion
//                }
//                else
//                {



//                    #region Row and Column

//                    sl = 1;

//                    #region Column Header
//                    range[excelSheetRow, 1] = "Sub-Total";
//                    range[excelSheetRow, dt.Columns.Count] = subtotal;
//                    rowount++;
//                    excelSheetRow++;
//                    range[excelSheetRow, 1] = brName;
//                    rowount++;
//                    excelSheetRow++;

//                    for (iCol = 0; iCol < dt.Columns.Count - 1; iCol++)
//                    {


//                        if (iCol == 0)
//                        {
//                            range[excelSheetRow, (iCol + 1)] = "SL No.";
//                            //range.WrapText = false;
//                            //range.Font.Bold = true;
//                            //range[rowount, (iCol + 1)].Font.Size = 14;



//                            range[excelSheetRow, (iCol + 1)].Interior.Color =
//                                System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                            range[excelSheetRow, (iCol + 1)].Borders.LineStyle =
//                                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                            range[excelSheetRow, (iCol + 1)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                        }
//                        range[excelSheetRow, (iCol + 2)] = iCol < 4
//                            ? Regex.Replace(dt.Columns[iCol].ColumnName, "([a-z])_?([A-Z])", "$1 $2")
//                            : dt.Columns[iCol].ColumnName;
//                        range.WrapText = false;
//                        range.Font.Bold = true;
//                        range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                        range[excelSheetRow, (iCol + 2)].Font.Size = 9;

//                        range[excelSheetRow, (iCol + 2)].Interior.Color =
//                            System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                        range[excelSheetRow, (iCol + 2)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;


//                        range[excelSheetRow, (iCol + 2)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);




//                    }
//                    excelSheetRow++;
//                    #endregion
//                    oldBr = brName;
//                    for (iCol = 0; iCol < dt.Columns.Count - 1; iCol++)
//                    {
//                        if (iCol == 0)
//                        {
//                            range[(excelSheetRow), (iCol + 1)] = sl;
//                            range[(excelSheetRow), (iCol + 1)].Borders.LineStyle =
//                                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                            range[(excelSheetRow), (iCol + 1)].Borders.Color =
//                                System.Drawing.ColorTranslator.ToOle(Color.Black);
//                        }

//                        range[(excelSheetRow), (iCol + 2)] = dt.Rows[iRow][iCol].ToString();
//                        range[(excelSheetRow), (iCol + 2)].Borders.LineStyle =
//                            Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                        range[(excelSheetRow), (iCol + 2)].Borders.Color =
//                            System.Drawing.ColorTranslator.ToOle(Color.Black);
//                        if (iCol > 5)
//                        {
//                            range[(excelSheetRow), (iCol + 2)].NumberFormat = "#,###,###";
//                        }
//                        else
//                        {

//                            range[(excelSheetRow), (iCol + 2)].WrapText = false;
//                        }
//                        range[(excelSheetRow), (iCol + 2)].Font.Name = "Verdana";

//                        range[(excelSheetRow), (iCol + 2)].Font.Size = 9;


//                    }
//                    subtotal = 0;
//                    subtotal += Convert.ToDecimal(dt.Rows[iRow]["Total Salary(Tk.)"].ToString());
//                    total += Convert.ToDecimal(dt.Rows[iRow]["Total Salary(Tk.)"].ToString());
//                    excelSheetRow++;
//                    rowount++;
//                    sl++;

//                    #endregion
//                }

//            }
//            #endregion

//            range[excelSheetRow, 1] = "Sub-Total";
//            range[excelSheetRow, dt.Columns.Count] = subtotal;
//            excelSheetRow++;
//            range[excelSheetRow, 1] = "Total";
//            range[excelSheetRow, dt.Columns.Count] = total;



//            return excelSheetRow;
//        }



//        public void GenerateExcelWithHeaderForSalarySheetPDFWithDefaultFont(DataSet receiveDS, string filePath, int rowcout, string companyName,
//           string companyAddress, int columnScape, bool istotal, DateTime salaryMonth, string locationName, string costCenterName,
//            string disbursmentType, string reportLogo)
//        {
//            var title = "Salary Sheet for the Month of " + salaryMonth.ToString("MMMM-yyyy");
//            GenerateExcelWithHeaderForSalarySheetPDFWithDefaultFont(receiveDS, filePath, rowcout, companyName, companyAddress, columnScape,
//                istotal, title, locationName, costCenterName, disbursmentType, reportLogo);
//        }

//        public void GenerateExcelWithHeaderForSalarySheetPDFWithDefaultFont(DataSet receiveDS, string filePath, int rowcout, string companyName,
//           string companyAddress, int columnScape, bool istotal, string headerText, string locationName, string costCenterName, string disbursmentType,
//            string reportLogo)
//        {
//            ds = receiveDS;
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

//                #region Main Header

//                // if (companyName != "")
//                //{
//                var headerRange1 = XLsheet.Range[XLsheet.Cells[1, 1], XLsheet.Cells[rowcout - 2, 6]];
//                //headerRange1[1, 1] = companyName;
//                headerRange1.Font.Bold = true;
//                headerRange1.Font.Size = 16;
//                //headerRange1.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                //headerRange1.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                //headerRange1.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                headerRange1.Merge(Type.Missing);
//                headerRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

//                string picPath = System.Web.HttpContext.Current.Server.MapPath("/Images/report-logo.png");
//                if (reportLogo != "")
//                {
//                    picPath = System.Web.HttpContext.Current.Server.MapPath(reportLogo);
//                }

//                object missing = System.Reflection.Missing.Value;
//                Microsoft.Office.Interop.Excel.Range picPosition = headerRange1;
//                //GetPicturePosition(); // retrieve the range for picture insert
//                Microsoft.Office.Interop.Excel.Pictures p =
//                    XLsheet.Pictures(missing) as Microsoft.Office.Interop.Excel.Pictures;
//                Microsoft.Office.Interop.Excel.Picture pic = null;
//                pic = p.Insert(picPath, missing);
//                pic.Left = Convert.ToDouble(picPosition.Left);
//                pic.Top = picPosition.Top;


//                //  }
//                if (companyName != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 3, 7], XLsheet.Cells[rowcout - 3, 14]];
//                    headerRange2[1, 1] = companyName; //companyAddress;
//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 16;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                }

//                if (costCenterName != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 3, ds.Tables[0].Columns.Count - 6], XLsheet.Cells[rowcout - 3, ds.Tables[0].Columns.Count]];
//                    headerRange2[1, 1] = costCenterName; //companyAddress;
//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 16;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
//                }


//                if (locationName != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 2, 7], XLsheet.Cells[rowcout - 2, 14]];
//                    headerRange2[1, 1] = locationName; //companyAddress;
//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 16;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                }

//                if (disbursmentType != "")
//                {
//                    var headerRange2 = XLsheet.Range[XLsheet.Cells[rowcout - 2, ds.Tables[0].Columns.Count - 6], XLsheet.Cells[rowcout - 2, ds.Tables[0].Columns.Count]];
//                    headerRange2[1, 1] = disbursmentType; //companyAddress;
//                    headerRange2.Font.Bold = true;
//                    headerRange2.Font.Size = 16;
//                    headerRange2.Merge(Type.Missing);
//                    headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
//                }

//                // if (companyName != "")
//                // {
//                var headerRange3 = XLsheet.Range[XLsheet.Cells[rowcout - 1, 1], XLsheet.Cells[rowcout - 1, 6]];
//                headerRange3[1, 1] = headerText;
//                // "Salary Control Register for the Month of " + salaryMonth.ToString("MMMM-yyyy");//companyAddress;
//                headerRange3.Font.Bold = true;
//                headerRange3.Font.Size = 16;
//                headerRange3.Merge(Type.Missing);
//                headerRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
//                //}
//                //var headerRang = XLsheet.get_Range(XLsheet.Cells[1, 1], XLsheet.Cells[1, 43]);

//                #endregion



//                int excelSheetRow = this.WriteDataToTheSpecifiedRangeForSalarySheetPDFWithDefaultFont(ds.Tables[0], range, rowcout);

//                if (istotal)
//                {
//                    var colCount = ds.Tables[0].Columns.Count;
//                    var dataRowCount = ds.Tables[0].Rows.Count;
//                    var headerRangeTotal =
//                        XLsheet.Range[
//                            XLsheet.Cells[rowcout + dataRowCount + 1, 1],
//                            XLsheet.Cells[rowcout + dataRowCount + 1, columnScape]];
//                    headerRangeTotal[1, 1] = "Total";
//                    headerRangeTotal.Font.Bold = true;
//                    headerRangeTotal.Font.Size = 16;
//                    headerRangeTotal.Interior.Color =
//                        System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                    headerRangeTotal.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    headerRangeTotal.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    headerRangeTotal.Merge(Type.Missing);
//                    headerRangeTotal.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                    //int i = columnScape - 1;

//                    for (int i = 0; i < (colCount - columnScape) + 1; i++)
//                    {
//                        var rangeTotal =
//                            XLsheet.Range[
//                                XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1],
//                                XLsheet.Cells[rowcout + dataRowCount + 1, i + columnScape + 1]];
//                        string startColumnLetter = ExcelColumnLetter(i + columnScape);
//                        string endColumnLetter = ExcelColumnLetter(i + columnScape);
//                        rangeTotal[1, 1] = "=SUM(" + startColumnLetter + "" + (rowcout + 1) + ":" + endColumnLetter + "" +
//                                           (rowcout + dataRowCount) + ")";
//                        rangeTotal[1, 1].Interior.Color =
//                            System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                        rangeTotal[1, 1].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                        rangeTotal[1, 1].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                        rangeTotal[1, 1].Font.Size = 16;

//                    }
//                    headerRangeTotal.Rows.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                    headerRangeTotal.Rows.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                }


//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");


//                XLsheet.PageSetup.PrintTitleRows = "$1:$5";

//                XLsheet.PageSetup.LeftFooter = "&16 &BPrepared By                      Checked By&B  \n " + "Empress| Payroll | Print Date, Time && Page No : " + DateTime.Now.ToString("MMMM dd, yyyy") + " | " + DateTime.Now.ToString("h:mm tt") + " | " + "Page &P of &N";
//                XLsheet.PageSetup.CenterFooter = "&16 &BAudited By                 Authorized by&B \n";
//                XLsheet.PageSetup.RightFooter = "&16 &BAuthorized by               Authorized by&B \n";

//                XLsheet.Columns.AutoFit();

//                XLsheet.Name = ds.Tables[0].TableName;
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

//        private int WriteDataToTheSpecifiedRangeForSalarySheetPDFWithDefaultFont(System.Data.DataTable dt, Range range, int rowount)
//        {
//            int iRow;
//            int iCol;
//            //Traverse through the DataTable columns to write the headers on the first row of the excel sheet.
//            for (iCol = 0; iCol < dt.Columns.Count; iCol++)
//            {
//                //range = (Range)worksheet.Cells[1, (iCol + 1)];
//                //range.Value2 = dt.Columns[iCol].ColumnName;
//                if (iCol == 0)
//                {
//                    range[rowount, (iCol + 1)] = "SL";
//                    range.WrapText = false;
//                    range.Font.Bold = true;
//                    range[rowount, (iCol + 1)].Font.Size = 16;



//                    range[rowount, (iCol + 1)].Interior.Color =
//                        System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                    range[rowount, (iCol + 1)].Borders.LineStyle =
//                        Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    range[rowount, (iCol + 1)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    range[rowount, (iCol + 1)].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                }
//                range[rowount, (iCol + 2)] = iCol < 4
//                    ? Regex.Replace(dt.Columns[iCol].ColumnName, "([a-z])_?([A-Z])", "$1 $2")
//                    : dt.Columns[iCol].ColumnName;
//                range.WrapText = false;
//                range.Font.Bold = true;
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                range[rowount, (iCol + 2)].Font.Size = 16;
//                range[rowount, (iCol + 2)].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                range[rowount, (iCol + 2)].Interior.Color =
//                    System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                range[rowount, (iCol + 2)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;


//                range[rowount, (iCol + 2)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);

//                if (iCol > 10)
//                {
//                    range[rowount, (iCol + 2)].Style.WrapText = true;
//                    //range[rowount, (iCol + 1)].EntireRow.AutoFit();
//                    //range.EntireColumn.AutoFit();
//                }
//                else
//                {
//                    range[rowount, (iCol + 2)].Style.WrapText = false;
//                }


//            }



//            //Traverse through the rows and columns of the datatable to write the data in the sheet.
//            int excelSheetRow = rowount + 1;
//            int sl = 1;
//            for (iRow = 0; iRow < dt.Rows.Count; iRow++)
//            {
//                for (iCol = 0; iCol < dt.Columns.Count; iCol++)
//                {
//                    if (iCol == 0)
//                    {
//                        range[(excelSheetRow), (iCol + 1)] = sl;
//                        range[(excelSheetRow), (iCol + 1)].Borders.LineStyle =
//                            Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                        range[(excelSheetRow), (iCol + 1)].Borders.Color =
//                            System.Drawing.ColorTranslator.ToOle(Color.Black);

//                        range[(excelSheetRow), (iCol + 1)].Font.Size = 16;
//                        range[(excelSheetRow), (iCol + 1)].Font.Bold = true;
//                        // range[(excelSheetRow), (iCol + 1)].Font.Size = 12;
//                    }

//                    range[(excelSheetRow), (iCol + 2)] = dt.Rows[iRow][iCol].ToString();
//                    range[(excelSheetRow), (iCol + 2)].Borders.LineStyle =
//                        Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    range[(excelSheetRow), (iCol + 2)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    if (iCol > 5)
//                    {

//                        try
//                        {
//                            decimal ctcAmt = Convert.ToDecimal(dt.Rows[iRow][iCol].ToString());
//                            if (ctcAmt < 0)
//                            {
//                                var nt = Convert.ToDecimal(dt.Rows[iRow]["Net Payout"].ToString());
//                                range[(excelSheetRow), (iCol + 2)] = "(" + dt.Rows[iRow][iCol].ToString() + ")";
//                                if (nt < 0)
//                                {
//                                    dt.Rows[iRow]["Remarks"] = "Adjusted With Bank";
//                                }

//                            }
//                        }
//                        catch
//                        {

//                        }

//                        range[(excelSheetRow), (iCol + 2)].NumberFormat = "#,###,###";
//                        range[(excelSheetRow), (iCol + 2)].WrapText = true;
//                    }
//                    else
//                    {

//                        range[(excelSheetRow), (iCol + 2)].WrapText = false;
//                    }

//                    range[(excelSheetRow), (iCol + 2)].Font.Size = 16;
//                    range[(excelSheetRow), (iCol + 2)].Font.Bold = true;
//                    // range[(excelSheetRow), (iCol + 2)].Font.Size = 12;


//                }
//                range.Rows[excelSheetRow].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                range.Rows[excelSheetRow].VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

//                excelSheetRow++;
//                sl++;
//            }


//            return excelSheetRow;
//        }

//        public string GenerateGLTextFileForOrionSalary(DataSet ds, string physicalPath, DateTime salaryMonth, DateTime voucharDate)
//        {
//            var txt = new StringBuilder();

//            string salaryNaraion1 = "[Source=Payroll] ";
//            string salaryNaraion2 = voucharDate.ToString("dd MMM yyyy") + ",[" +
//                CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(salaryMonth.Month) + " " +
//                salaryMonth.Year.ToString() + "]";

//            var dt = ds.Tables["CostCenterWiseSalaryDebitCredit"];
//            if (dt != null)
//            {
//                txt.Append(salaryNaraion1);
//                txt.Append(Environment.NewLine);
//                //txt.Append("[New]," + dt.Rows[0]["SJV_Number"].ToString() + "," + salaryNaraion2 + " Salary & Wages - " + dt.Rows[0]["CostCentreName"].ToString());
//                txt.Append("[New]," + dt.Rows[0]["SJV_Number"].ToString() + "," + salaryNaraion2 + " " + dt.Rows[0]["SJVDesc"].ToString() + " - " + dt.Rows[0]["BranchName"].ToString());
//                txt.Append(Environment.NewLine);

//                decimal footeramount = 0;
//                int trCount = 0;

//                #region  CostCenterWiseSalaryDebitCredit

//                for (int i = 0; i < dt.Rows.Count; i++)
//                {
//                    trCount++;
//                    string accountHeadCode = dt.Rows[i]["AccountHeadCode"].ToString().Trim();
//                    string costCentreCode = dt.Rows[i]["CostCentreCode"].ToString().Trim();
//                    int amount = Convert.ToInt32(Convert.ToDecimal(dt.Rows[i]["Amount"].ToString()));
//                    string accountType = dt.Rows[i]["AccountType"].ToString().Trim();

//                    var newLine = string.Format("{0},{1},{2},{3}{4}",
//                        accountHeadCode,
//                        costCentreCode,
//                        amount,
//                        accountType, Environment.NewLine);
//                    txt.Append(newLine);
//                }
//            }
//                #endregion


//            #region  EmpAddionalPayment

//            var dtEmpAddionalPayment = ds.Tables["EmpAddionalPayment"];

//            if (dtEmpAddionalPayment != null)
//            {
//                for (int i = 0; i < dtEmpAddionalPayment.Rows.Count; i++)
//                {
//                    string empIdFull = "";
//                    txt.Append("[AddInfo],");

//                    string accountHeadCode = dtEmpAddionalPayment.Rows[i]["AccountHeadCode"].ToString().Trim();
//                    empIdFull = dtEmpAddionalPayment.Rows[i]["EmployeeId"].ToString().Trim();
//                    string employeeId = empIdFull.Substring(2);

//                    int amount = Convert.ToInt32(Convert.ToDecimal(dtEmpAddionalPayment.Rows[i]["Amount"].ToString()));

//                    var newLine = string.Format("{0},{1},{2},{3}{4}",
//                        accountHeadCode,
//                        employeeId,
//                        DateTime.Now.ToString("dd MMM yyyy"),
//                        amount, Environment.NewLine);
//                    txt.Append(newLine);
//                }
//            }
//            #endregion

//            // File.WriteAllText(physicalPath, txt.ToString());
//            // File.Open(physicalPath);

//            var streamWriter = new StreamWriter(physicalPath);
//            streamWriter.WriteLine(txt.ToString());
//            streamWriter.Flush();
//            streamWriter.Close();

//            return txt.ToString();
//            //Only Read
//        }

//        public string GenerateGLTextFileForOrion(DataSet ds, string physicalPath, DateTime salaryMonth, DateTime voucharDate)
//        {
//            var txt = new StringBuilder();

//            string salaryNaraion1 = "[Source=Payroll] ";
//            string salaryNaraion2 = voucharDate.ToString("dd MMM yyyy") + ",[" +
//                CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(salaryMonth.Month) + " " +
//                salaryMonth.Year.ToString() + "]";

//            var dt = ds.Tables["CostCenterWiseSalaryDebitCredit"];
//            if (dt != null)
//            {
//                txt.Append(salaryNaraion1);
//                txt.Append(Environment.NewLine);
//                //txt.Append("[New]," + dt.Rows[0]["SJV_Number"].ToString() + "," + salaryNaraion2 + " Salary & Wages - " + dt.Rows[0]["CostCentreName"].ToString());
//                txt.Append("[New]," + dt.Rows[0]["SJV_Number"].ToString() + "," + salaryNaraion2 + " " + dt.Rows[0]["SJVDesc"].ToString() + " - " + dt.Rows[0]["CostCentreName"].ToString() + " - " + dt.Rows[0]["BranchName"].ToString());
//                txt.Append(Environment.NewLine);

//                decimal footeramount = 0;
//                int trCount = 0;

//                #region  CostCenterWiseSalaryDebitCredit

//                for (int i = 0; i < dt.Rows.Count; i++)
//                {
//                    trCount++;
//                    string accountHeadCode = dt.Rows[i]["AccountHeadCode"].ToString().Trim();
//                    string costCentreCode = dt.Rows[i]["CostCentreCode"].ToString().Trim();
//                    int amount = Convert.ToInt32(Convert.ToDecimal(dt.Rows[i]["Amount"].ToString()));
//                    string accountType = dt.Rows[i]["AccountType"].ToString().Trim();

//                    var newLine = string.Format("{0},{1},{2},{3}{4}",
//                        accountHeadCode,
//                        costCentreCode,
//                        amount,
//                        accountType, Environment.NewLine);
//                    txt.Append(newLine);
//                }
//            }
//                #endregion


//            #region  EmpAddionalPayment

//            var dtEmpAddionalPayment = ds.Tables["EmpAddionalPayment"];

//            if (dtEmpAddionalPayment != null)
//            {
//                for (int i = 0; i < dtEmpAddionalPayment.Rows.Count; i++)
//                {
//                    string empIdFull = "";
//                    txt.Append("[AddInfo],");

//                    string accountHeadCode = dtEmpAddionalPayment.Rows[i]["AccountHeadCode"].ToString().Trim();
//                    empIdFull = dtEmpAddionalPayment.Rows[i]["EmployeeId"].ToString().Trim();
//                    string employeeId = empIdFull.Substring(2);

//                    int amount = Convert.ToInt32(Convert.ToDecimal(dtEmpAddionalPayment.Rows[i]["Amount"].ToString()));

//                    var newLine = string.Format("{0},{1},{2},{3}{4}",
//                        accountHeadCode,
//                        employeeId,
//                        DateTime.Now.ToString("dd MMM yyyy"),
//                        amount, Environment.NewLine);
//                    txt.Append(newLine);
//                }
//            }
//            #endregion

//            // File.WriteAllText(physicalPath, txt.ToString());
//            // File.Open(physicalPath);

//            var streamWriter = new StreamWriter(physicalPath);
//            streamWriter.WriteLine(txt.ToString());
//            streamWriter.Flush();
//            streamWriter.Close();

//            return txt.ToString();
//            //Only Read
//        }

//        public void SAPSalaryExportToExcelForBG(System.Data.DataTable dataTable, string filePath)
//        {
//            strFileName = filePath;
//            try
//            {
//                int rowcout = dataTable.Rows.Count;

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
//                int excelSheetRow = this.WriteDataSAPSalaryToTheSpecifiedRangeForBG(dataTable, range, 1);

//                range = ((Worksheet)workbook.ActiveSheet).get_Range("A1", "AZ1");
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


//                Worksheet XLsheet = ((Worksheet)workbook.ActiveSheet);

//                XLsheet.Columns.AutoFit();

//                XLsheet.Name = "Sheet1";
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

//        private int WriteDataSAPSalaryToTheSpecifiedRangeForBG(System.Data.DataTable dt, Range range, int rowount)
//        {
//            int iRow;
//            int iCol;
//            //Traverse through the DataTable columns to write the headers on the first row of the excel sheet.
//            for (iCol = 0; iCol < dt.Columns.Count; iCol++)
//            {
//                //range = (Range)worksheet.Cells[1, (iCol + 1)];
//                //range.Value2 = dt.Columns[iCol].ColumnName;
//                //if (iCol == 0)
//                //{
//                //    range[rowount, (iCol + 1)] = "SL";
//                //    range.WrapText = true;
//                //    range.Font.Bold = false;
//                //    range[rowount, (iCol + 1)].Font.Size = 10;



//                //    range[rowount, (iCol + 1)].Interior.Color =
//                //        System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                //    range[rowount, (iCol + 1)].Borders.LineStyle =
//                //        Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                //    range[rowount, (iCol + 1)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                //}
//                range[rowount, (iCol + 1)] = iCol < 4
//                    ? Regex.Replace(dt.Columns[iCol].ColumnName, "([a-z])_?([A-Z])", "$1 $2")
//                    : dt.Columns[iCol].ColumnName;
//                range.WrapText = true;
//                range.Font.Bold = false;
//                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
//                range[rowount, (iCol + 1)].Font.Size = 10;

//                range[rowount, (iCol + 1)].Interior.Color =
//                    System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#CC99FF"));
//                range[rowount, (iCol + 1)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;


//                range[rowount, (iCol + 1)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);

//                if (iCol > 5)
//                {
//                    range[rowount, (iCol + 1)].Style.WrapText = true;
//                    //range[rowount, (iCol + 1)].EntireRow.AutoFit();
//                    //range.EntireColumn.AutoFit();
//                }
//                else
//                {
//                    range[rowount, (iCol + 1)].Style.WrapText = true;
//                }


//            }



//            //Traverse through the rows and columns of the datatable to write the data in the sheet.
//            int excelSheetRow = rowount + 1;
//            int sl = 1;
//            for (iRow = 0; iRow < dt.Rows.Count; iRow++)
//            {
//                for (iCol = 0; iCol < dt.Columns.Count; iCol++)
//                {
//                    //if (iCol == 0)
//                    //{
//                    //    range[(excelSheetRow), (iCol + 1)] = sl;
//                    //    range[(excelSheetRow), (iCol + 1)].Borders.LineStyle =
//                    //        Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    //    range[(excelSheetRow), (iCol + 1)].Borders.Color =
//                    //        System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    //}

//                    range[(excelSheetRow), (iCol + 1)] = dt.Rows[iRow][iCol].ToString();
//                    range[(excelSheetRow), (iCol + 1)].Borders.LineStyle =
//                        Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
//                    range[(excelSheetRow), (iCol + 1)].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
//                    if (iCol > 11 && iCol<13)
//                    {
//                        try
//                        {



//                            decimal ctcAmt = Convert.ToDecimal(dt.Rows[iRow][iCol].ToString());
//                            if (ctcAmt < 0)
//                            {
//                                var nt = Convert.ToDecimal(dt.Rows[iRow]["Net Payout"].ToString());
//                                range[(excelSheetRow), (iCol + 1)] = "(" + dt.Rows[iRow][iCol].ToString() + ")";
//                                if (nt < 0)
//                                {
//                                    dt.Rows[iRow]["Remarks"] = "Adjusted With Bank";
//                                }

//                            }
//                        }
//                        catch
//                        {

//                        }

//                        range[(excelSheetRow), (iCol + 1)].NumberFormat = "#,###,###";
//                        range[(excelSheetRow), (iCol + 1)].WrapText = true;
//                    }
//                    else
//                    {

//                        range[(excelSheetRow), (iCol + 1)].WrapText = true;
//                        range[(excelSheetRow), (iCol + 1)].NumberFormat = "@";
//                    }
//                    range[(excelSheetRow), (iCol + 1)].Font.Name = "Verdana";

//                    range[(excelSheetRow), (iCol + 1)].Font.Size = 10;

//                    //if (dt.Columns[iCol].ColumnName == "Material")
//                    //{
//                    //    range[(excelSheetRow), (iCol + 1)] = "'" + dt.Rows[iRow][iCol].ToString();
//                    //}
//                    //else
//                    //{
//                    //    range[(excelSheetRow), (iCol + 1)] = dt.Rows[iRow][iCol].ToString();
//                    //}


//                }
//                excelSheetRow++;
//                sl++;
//            }


//            return excelSheetRow;
//        }
//    }
//}