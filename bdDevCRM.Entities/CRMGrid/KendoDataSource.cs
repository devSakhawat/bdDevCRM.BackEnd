//using bdDevCRM.Entities.CRMGrid.GRID;
//using System.Data;
//using System.Text;

//namespace Utilities
//{
//  public static class Kendo<T>
//  {
//    public class Grid
//    {
//      public static GridEntity<T> DataSource(GridOptions options, string query, string orderBy, string condition)
//      {
//        var _connection = new CommonConnection();
//        try
//        {
//          query = query.Replace(';', ' ');
//          string orderby = "";
//          string sqlQuery = query;
//          if (options != null)
//          {
//            if (options.pageSize > 0)
//            {
//              sqlQuery = GridQueryBuilder<T>.Query(options, query, orderBy, condition);
//            }
//            else
//            {
//              if (orderBy != "")
//              {
//                if (orderBy.ToLower().Contains("asc") || orderBy.ToLower().Contains("desc"))
//                {
//                  orderby = string.Format(" order by {0}", orderBy);
//                }
//                else
//                {
//                  orderby = string.Format(" order by {0} asc ", orderBy);
//                }
//              }
//            }
//          }
//          else
//          {
//            if (orderBy != "")
//            {
//              if (orderBy.ToLower().Contains("asc") || orderBy.ToLower().Contains("desc"))
//              {
//                orderby = string.Format(" order by {0}", orderBy);
//              }
//              else
//              {
//                orderby = string.Format(" order by {0} asc ", orderBy);
//              }
//            }
//          }

//          if (!string.IsNullOrEmpty(condition))
//          {
//            condition = " WHERE " + condition;
//          }

//          var condition1 = "";
//          if (options != null)
//          {
//            if (options.filter != null)
//            {
//              condition1 = GridQueryBuilder<T>.FilterCondition(options.filter).Trim();
//            }
//          }
//          if (!string.IsNullOrEmpty(condition1))
//          {
//            if (!string.IsNullOrEmpty(condition))
//            {
//              condition += " And " + condition1;
//            }
//            else
//            {
//              condition = " WHERE " + condition1;
//            }
//          }
//          sqlQuery = "SELECT * FROM (" + sqlQuery + " ) As tbl " + condition;

//          DataTable dataTable = _connection.GetDataTable(sqlQuery + orderby);

//          String sqlCount = "";
//          if (_connection.DatabaseType == DatabaseType.SQL)
//          {
//            sqlCount = "SELECT COUNT(*) FROM (" + query + " ) As tbl " + condition;
//          }
//          else if (_connection.DatabaseType == DatabaseType.Oracle)
//          {
//            sqlCount = "SELECT COUNT(*) FROM (" + query + " )" + condition;
//          }

//          int totalCount = _connection.GetScaler(sqlCount);
//          var dataList = (List<T>)ListConversion.ConvertTo<T>(dataTable);
//          var result = new GridResult<T>().Data(dataList, totalCount);


//          return result;
//        }
//        catch (Exception ex)
//        {
//          throw ex;
//        }
//        finally
//        {
//          _connection.Close();
//        }
//      }

//      public static GridEntity<T> DataSourceWithDateQuary(GridOptions options, string query, string orderBy,
//          string condition, string withDateQuary)
//      {
//        //string sql = "SELECT * FROM " + tableName;
//        var _connection = new CommonConnection();
//        try
//        {
//          query = query.Replace(';', ' ');

//          string sqlQuery = options != null
//              ? GridQueryBuilder<T>.Query(options, query, orderBy, condition)
//              : query;

//          if (!string.IsNullOrEmpty(condition))
//          {
//            condition = " WHERE " + condition;
//          }

//          var condition1 = options != null ? GridQueryBuilder<T>.FilterCondition(options.filter) : "";
//          if (!string.IsNullOrEmpty(condition1))
//          {
//            if (!string.IsNullOrEmpty(condition))
//            {
//              condition += " And " + condition1;
//            }
//            else
//            {
//              condition = " WHERE " + condition1;
//            }
//          }

//          if (withDateQuary != "")
//          {
//            sqlQuery = withDateQuary + sqlQuery;
//          }

//          DataTable dataTable = _connection.GetDataTable(sqlQuery);

//          String sqlCount = "";
//          if (_connection.DatabaseType == DatabaseType.SQL)
//          {
//            sqlCount = withDateQuary + " SELECT COUNT(*) FROM (" + query + " ) As tbl " + condition;
//          }
//          else if (_connection.DatabaseType == DatabaseType.Oracle)
//          {
//            sqlCount = withDateQuary + " SELECT COUNT(*) FROM (" + query + " )" + condition;
//          }

//          int totalCount = 0;
//          try
//          {
//            totalCount = _connection.GetScaler(sqlCount);
//          }
//          catch
//          {

//          }

//          var dataList = (List<T>)ListConversion.ConvertTo<T>(dataTable);
//          var result = new GridResult<T>().Data(dataList, totalCount);


//          return result;
//        }
//        catch (Exception ex)
//        {
//          throw ex;
//        }
//        finally
//        {
//          _connection.Close();
//        }
//      }

//      public static DataSet GetDataSet(GridOptions options, string query, string orderBy)
//      {
//        //string sql = "SELECT * FROM " + tableName;
//        DataSet gridDataSet = new DataSet();
//        var _connection = new CommonConnection();
//        string condition = "";
//        try
//        {
//          query = query.Replace(';', ' ');

//          string sqlQuery = options != null
//              ? GridQueryBuilder<T>.Query(options, query, orderBy, condition)
//              : query;

//          if (!string.IsNullOrEmpty(condition))
//          {
//            condition = " WHERE " + condition;
//          }

//          var condition1 = options != null ? GridQueryBuilder<T>.FilterCondition(options.filter) : "";
//          if (!string.IsNullOrEmpty(condition1))
//          {
//            if (!string.IsNullOrEmpty(condition))
//            {
//              condition += " And " + condition1;
//            }
//            else
//            {
//              condition = " WHERE " + condition1;
//            }
//          }

//          DataTable dataTable = _connection.GetDataTable(sqlQuery);
//          gridDataSet.Tables.Add(dataTable);
//          String sqlCount = "";
//          if (_connection.DatabaseType == DatabaseType.SQL)
//          {
//            sqlCount = "SELECT COUNT(*) FROM (" + query + " ) As tbl " + condition;
//          }
//          else if (_connection.DatabaseType == DatabaseType.Oracle)
//          {
//            sqlCount = "SELECT COUNT(*) FROM (" + query + " )" + condition;
//          }

//          int totalCount = _connection.GetScaler(sqlCount);
//          DataTable totalCountDt = new DataTable("TotalCount");
//          DataColumn col = new DataColumn("totalCount");
//          col.DataType = Type.GetType("System.Int32");
//          totalCountDt.Columns.Add(col);
//          DataRow dr = totalCountDt.NewRow();
//          dr["totalCount"] = totalCount;
//          totalCountDt.Rows.Add(dr);

//          //var dataList = (List<T>)ListConversion.ConvertTo<T>(dataTable);
//          //var result = new GridResult<T>().Data(dataList, totalCount);
//          gridDataSet.Tables.Add(totalCountDt);


//          return gridDataSet;
//        }
//        catch (Exception ex)
//        {
//          throw ex;
//        }
//        finally
//        {
//          _connection.Close();
//        }
//      }

//      public static GridEntity<T> DataSource(GridOptions options, string query, string orderBy)
//      {
//        return DataSource(options, query, orderBy, "");
//      }

//      public static GridEntity<T> DataSourceWithDateQuary(GridOptions options, string query, string orderBy,
//          string withQuary)
//      {
//        return DataSourceWithDateQuary(options, query, orderBy, "", withQuary);
//      }


//      public static GridEntity<T> GenericDataSource(GridOptions options, string query, string orderBy,
//          string condition)
//      {
//        var _connection = new CommonConnection();
//        StringBuilder gridQuery;
//        StringBuilder totalQuery;
//        GetGridPagingQuery(options, query, orderBy, condition, out gridQuery, out totalQuery,
//            _connection.DatabaseType);
//        DataTable dataTable = _connection.GetDataTable(gridQuery.ToString());
//        int totalCount = _connection.GetScaler(totalQuery.ToString());

//        var dataList = (List<T>)GenericListGenerator.GetList<T>(dataTable);
//        var result = new GridResult<T>().Data(dataList, totalCount);
//        return result;
//      }

//      public static GridEntity<T> GenericDataSource(GridOptions options, string query, string orderBy)
//      {
//        return GenericDataSource(options, query, orderBy, "");
//      }

//      private static void GetGridPagingQuery(GridOptions options, string query, string orderBy, string condition,
//          out StringBuilder gridQuery, out StringBuilder totalQuery, DatabaseType databaseType)
//      {
//        //string sql = "SELECT * FROM " + tableName;
//        // var _connection = new CommonConnection();
//        try
//        {
//          gridQuery = new StringBuilder();
//          totalQuery = new StringBuilder();

//          query = query.Replace(';', ' ');

//          string strQuery = options != null
//              ? GridQueryBuilder<T>.Query(options, query, orderBy, condition)
//              : query;

//          if (!string.IsNullOrEmpty(condition))
//          {
//            condition = " WHERE " + condition;
//          }

//          var condition1 = options != null ? GridQueryBuilder<T>.FilterCondition(options.filter) : "";
//          if (!string.IsNullOrEmpty(condition1))
//          {
//            if (!string.IsNullOrEmpty(condition))
//            {
//              condition += " And " + condition1;
//            }
//            else
//            {
//              condition = " WHERE " + condition1;
//            }
//          }
//          string tQuery = "";
//          if (databaseType == DatabaseType.SQL)
//          {
//            tQuery = "SELECT COUNT(*) FROM (" + query + " ) As tbl " + condition;
//          }
//          else if (databaseType == DatabaseType.Oracle)
//          {
//            tQuery = "SELECT COUNT(*) FROM (" + query + " )" + condition;
//          }

//          gridQuery.Append(strQuery);
//          totalQuery.Append(tQuery);
//        }
//        catch (Exception ex)
//        {
//          throw ex;
//        }
//        finally
//        {
//          //_connection.Close();
//        }
//      }




//    }

//    public class Combo
//    {
//      public static List<T> DataSource(string query)
//      {
//        var returnList = new List<T>();
//        var connection = new CommonConnection();
//        try
//        {
//          DataTable dataTable = connection.GetDataTable(query);

//          var data = (List<T>)ListConversion.ConvertTo<T>(dataTable);
//          if (data != null)
//          {
//            return data;
//          }
//        }
//        catch (Exception ex)
//        {
//          throw ex;
//        }
//        finally
//        {
//          connection.Close();
//        }
//        return returnList;
//      }
//    }

//    public class AutoComplete
//    {
//      public static dynamic DataSource(AutoCompOptions options, string query, string orderBy)
//      {
//        var connection = new CommonConnection();
//        try
//        {
//          query = query.Replace(';', ' ');

//          string sqlQuery = GridQueryBuilder<T>.Query(options, query, orderBy, "");


//          DataTable dataTable = connection.GetDataTable(sqlQuery);

//          var dataList = (List<T>)ListConversion.ConvertTo<T>(dataTable);


//          return dataList;
//        }
//        catch (Exception ex)
//        {
//          throw;
//        }
//        finally
//        {
//          connection.Close();
//        }
//      }
//    }

//    public class Scheduler
//    {
//      public static dynamic DataSource(string query)
//      {
//        return Data<T>.SchedulerDataSource(query);
//      }
//    }

//    public class Diagram
//    {
//      public static List<T> OrganogramDataSource(string query)
//      {
//        var data = Data<T>.DataSource(query);
//        return data;
//      }
//    }

//    public class MultiSelect
//    {
//      public static dynamic DataSource(MultiSelectOptions options, string query, string orderBy)
//      {
//        var objOptions = new AutoCompOptions();
//        if (options != null)
//        {
//          objOptions.take = options.take;
//          objOptions.page = options.page;
//          objOptions.pageSize = options.pageSize;
//          objOptions.skip = options.skip;
//          objOptions.filter = options.filter;
//        }

//        return AutoComplete.DataSource(objOptions, query, orderBy);
//      }
//    }




//  }

//  public class Data
//  {
//    public static DataTable DataTable(string query)
//    {
//      var connection = new CommonConnection();
//      try
//      {
//        DataTable dataTable = connection.GetDataTable(query);

//        return dataTable;
//      }
//      catch (Exception ex)
//      {
//        throw ex;
//      }
//      finally
//      {
//        connection.Close();
//      }
//    }
//  }

//  public class Data<T>
//  {
//    public static List<T> DataSource(string query)
//    {
//      var connection = new CommonConnection();
//      try
//      {
//        DataTable dataTable = connection.GetDataTable(query);
//        var objData = (List<T>)ListConversion.ConvertTo<T>(dataTable);
//        return objData;
//      }
//      catch (Exception ex)
//      {
//        throw ex;
//      }
//      finally
//      {
//        connection.Close();
//      }
//    }

//    public static List<T> GenericDataSource(string query)
//    {
//      var connection = new CommonConnection();
//      try
//      {
//        DataTable dataTable = connection.GetDataTable(query);

//        var objData = (List<T>)GenericListGenerator.GetList<T>(dataTable);
//        if (objData.Count == 0)
//        {
//          return new List<T>();
//        }
//        return objData;
//      }
//      catch (Exception ex)
//      {
//        throw ex;
//      }
//      finally
//      {
//        connection.Close();
//      }
//    }

//    public static List<T> SchedulerDataSource(string query)
//    {
//      return GenericDataSource(query);
//    }

//    public static T SingleData(string query)
//    {
//      //query = string.Format("Select top 1 * from ({0}) T", query);
//      var data = DataSource(query);
//      if (data != null && data.Count > 0)
//      {
//        if (data.Count > 1)
//        {
//          throw new Exception("Duplicate Data Found");

//        }
//        else
//        {
//          return data.SingleOrDefault();

//        }
//      }

//      return Activator.CreateInstance<T>();
//    }


//  }
//}