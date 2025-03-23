//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using Utilities.StaticData;

//namespace Utilities.CustomData
//{
//    public class CustomDataTableBuilder
//    {



//        private static DataRow CustomDataRow(DataTable tableStructure, DataRow rowWithValue, int userId)
//        {
//            DataRow dr = tableStructure.NewRow();

//            foreach (DataColumn column in tableStructure.Columns)
//            {
//                dynamic columnVal = "";
//                bool isContain = rowWithValue.Table.Columns.Contains(column.ColumnName);
//                if (isContain)
//                {
//                    //dynamic columnValue = rowWithValue[column.ColumnName];
//                    //columnValue = columnValue ?? "";
//                    //columnVal = CustomValueBind.ValueConverter(column.DataType, columnValue.ToString());
//                    //dr[column.ColumnName] = columnVal;

//                    dynamic columnValue = rowWithValue[column.ColumnName];
//                    columnVal = CustomValueBind.ValueConverter(column.DataType, columnValue);
//                    dr[column.ColumnName] = columnVal ?? DBNull.Value;

//                }
//                else
//                {
//                    if (column.ColumnName == "UserId")
//                    {
//                        if (userId > 0)
//                        {
//                            columnVal = userId;
//                            dr[column.ColumnName] = columnVal;
//                        }

//                    }

//                }


//            }

//            return dr;
//        }


//        public static DataTable GetCustomTable(DataTable dataTable, string destinationName, CustomAction customAction)
//        {

//            return GetCustomTable(dataTable, destinationName, (int)customAction);
//        }

//        public static DataTable GetCustomTable(DataTable dataTable, string destinationName)
//        {

//            return GetCustomTable(dataTable, destinationName, -1);
//        }



//        public static DataTable GetCustomTable(DataTable dataTable, string tempTableName, int userId)
//        {
//            DataTable rDataTable = new DataTable(tempTableName);
//            CommonConnection connection = new CommonConnection();
//            try
//            {
//                if (tempTableName.Trim() != string.Empty)
//                {
//                    string strDelete = "";
//                    if (userId > 0)
//                    {
//                        strDelete = string.Format("truncate table {0}", tempTableName);

//                    }
//                    else if (userId == 0)
//                    {

//                        strDelete = "";
//                    }
//                    else if (userId == -1)
//                    {
//                        strDelete = string.Format("Delete {0} ", tempTableName);

//                    }
//                    if (strDelete != "")
//                    {
//                        connection.ExecuteNonQuery(strDelete);

//                    }
//                }

//                String sql = "Select top (0) * from " + tempTableName;
//                var tableStructure = connection.GetDataTable(sql);
//                foreach (DataRow rowWithValue in dataTable.Rows)
//                {
//                    tableStructure.Rows.Add(CustomDataRow(tableStructure, rowWithValue, userId));

//                }
//                rDataTable = tableStructure;
//            }
//            catch (Exception e)
//            {
//                var message = e;
//                throw;
//            }
//            finally
//            {
//                connection.Close();
//            }



//            return rDataTable;
//        }

//        public static DataTable CreateCustomTable(DataTable dataTable, string tempTableName)
//        {
//            DataTable rDataTable = new DataTable(tempTableName);
//            CommonConnection connection = new CommonConnection();
//            try
//            {


//                String sql = "Select top (0) * from " + tempTableName;
//                var tableStructure = connection.GetDataTable(sql);
//                foreach (DataRow rowWithValue in dataTable.Rows)
//                {
//                    tableStructure.Rows.Add(CustomDataRow(tableStructure, rowWithValue, 0));

//                }
//                rDataTable = tableStructure;
//            }
//            catch (Exception)
//            {

//                throw;
//            }
//            finally
//            {
//                connection.Close();
//            }



//            return rDataTable;
//        }






//        public static DataTable GetBulkDataTable(DataTable dataTable, string tempTableName, string refName, int refValue)
//        {
//            DataTable rDataTable = new DataTable(tempTableName);
//            CommonConnection connection = new CommonConnection();
//            try
//            {


//                String sql = "Select top (0) * from " + tempTableName;
//                var tableStructure = connection.GetDataTable(sql);
//                foreach (DataRow rowWithValue in dataTable.Rows)
//                {
//                    tableStructure.Rows.Add(CustomDataRow(tableStructure, rowWithValue, refName, refValue));

//                }
//                rDataTable = tableStructure;
//            }
//            catch (Exception)
//            {

//                throw;
//            }
//            finally
//            {
//                connection.Close();
//            }



//            return rDataTable;
//        }


//        private static DataRow CustomDataRow(DataTable tableStructure, DataRow rowWithValue, string refName, int refValue)
//        {
//            DataRow dr = tableStructure.NewRow();

//            foreach (DataColumn column in tableStructure.Columns)
//            {
//                dynamic columnVal;
//                bool isContain = rowWithValue.Table.Columns.Contains(column.ColumnName);
//                if (isContain)
//                {
//                    dynamic columnValue = rowWithValue[column.ColumnName];

//                    columnVal = CustomValueBind.ValueConverter(column.DataType, columnValue);

//                    if (columnVal == null)
//                    {
//                        dr[column.ColumnName] = System.DBNull.Value;
//                    }
//                    else
//                    {
//                        dr[column.ColumnName] = columnVal;
//                    }
//                }
//                else
//                {
//                    if (column.ColumnName == refName)
//                    {


//                        dr[column.ColumnName] = refValue;

//                    }

//                }


//            }

//            return dr;
//        }
//    }
//}
