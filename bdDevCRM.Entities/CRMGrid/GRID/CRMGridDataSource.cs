//using System.Data;
//using Utilities;

//namespace bdDevCRM.Entities.CRMGrid.GRID;

//public class CRMGridDataSource<T>
//{

//  public static string DataSourceQuery(CRMGridOptions options, string query, string orderBy ,string condition)
//  {
//    try
//    {
//      query = query.Replace(';', ' ');
//      string orderby = "";
//      string sqlQuery = query;
//      if (options != null)
//      {
//        if (options.pageSize > 0)
//        {
//          sqlQuery = CRMGridDataBuilder<T>.Query(options, query, orderBy, condition);
//        }
//        else
//        {
//          if (orderBy != "")
//          {
//            if (orderBy.ToLower().Contains("asc") || orderBy.ToLower().Contains("desc"))
//            {
//              orderby = string.Format(" order by {0}", orderBy);
//            }
//            else
//            {
//              orderby = string.Format(" order by {0} asc ", orderBy);
//            }
//          }
//        }
//      }
//      else
//      {
//        if (orderBy != "")
//        {
//          if (orderBy.ToLower().Contains("asc") || orderBy.ToLower().Contains("desc"))
//          {
//            orderby = string.Format(" order by {0}", orderBy);
//          }
//          else
//          {
//            orderby = string.Format(" order by {0} asc ", orderBy);
//          }
//        }
//      }

//      if (!string.IsNullOrEmpty(condition))
//      {
//        condition = " WHERE " + condition;
//      }

//      var condition1 = "";
//      if (options != null)
//      {
//        if (options.filter != null)
//        {
//          condition1 = CRMGridDataBuilder<T>.FilterCondition(options.filter).Trim();
//        }
//      }
//      if (!string.IsNullOrEmpty(condition1))
//      {
//        if (!string.IsNullOrEmpty(condition))
//        {
//          condition += " And " + condition1;
//        }
//        else
//        {
//          condition = " WHERE " + condition1;
//        }
//      }
//      sqlQuery = "SELECT * FROM (" + sqlQuery + " ) As tbl " + condition;

//      return sqlQuery + orderby;
//    }
//    catch (Exception ex)
//    {
//      throw ex;
//    }
//  }
  
//  //public static GridEntity<T> DataSource(GridOptions options, string query, string orderBy)
//  //{
//  //  return DataSource(options, query, orderBy, "");
//  //}

//  //public static GridEntity<T> DataSource(GridOptions options, string query, string orderBy, string condition)
//  //{
//  //  //var _connection = new CommonConnection();
//  //  try
//  //  {
//  //    query = query.Replace(';', ' ');
//  //    string orderby = "";
//  //    string sqlQuery = query;
//  //    if (options != null)
//  //    {
//  //      if (options.pageSize > 0)
//  //      {
//  //        sqlQuery = GridQueryBuilder<T>.Query(options, query, orderBy, condition);
//  //      }
//  //      else
//  //      {
//  //        if (orderBy != "")
//  //        {
//  //          if (orderBy.ToLower().Contains("asc") || orderBy.ToLower().Contains("desc"))
//  //          {
//  //            orderby = string.Format(" order by {0}", orderBy);
//  //          }
//  //          else
//  //          {
//  //            orderby = string.Format(" order by {0} asc ", orderBy);
//  //          }
//  //        }
//  //      }
//  //    }
//  //    else
//  //    {
//  //      if (orderBy != "")
//  //      {
//  //        if (orderBy.ToLower().Contains("asc") || orderBy.ToLower().Contains("desc"))
//  //        {
//  //          orderby = string.Format(" order by {0}", orderBy);
//  //        }
//  //        else
//  //        {
//  //          orderby = string.Format(" order by {0} asc ", orderBy);
//  //        }
//  //      }
//  //    }

//  //    if (!string.IsNullOrEmpty(condition))
//  //    {
//  //      condition = " WHERE " + condition;
//  //    }

//  //    var condition1 = "";
//  //    if (options != null)
//  //    {
//  //      if (options.filter != null)
//  //      {
//  //        condition1 = GridQueryBuilder<T>.FilterCondition(options.filter).Trim();
//  //      }
//  //    }
//  //    if (!string.IsNullOrEmpty(condition1))
//  //    {
//  //      if (!string.IsNullOrEmpty(condition))
//  //      {
//  //        condition += " And " + condition1;
//  //      }
//  //      else
//  //      {
//  //        condition = " WHERE " + condition1;
//  //      }
//  //    }
//  //    sqlQuery = "SELECT * FROM (" + sqlQuery + " ) As tbl " + condition;

//  //    DataTable dataTable = _connection.GetDataTable(sqlQuery + orderby);

//  //    String sqlCount = "";
//  //    if (_connection.DatabaseType == DatabaseType.SQL)
//  //    {
//  //      sqlCount = "SELECT COUNT(*) FROM (" + query + " ) As tbl " + condition;
//  //    }
//  //    else if (_connection.DatabaseType == DatabaseType.Oracle)
//  //    {
//  //      sqlCount = "SELECT COUNT(*) FROM (" + query + " )" + condition;
//  //    }

//  //    int totalCount = _connection.GetScaler(sqlCount);
//  //    var dataList = (List<T>)ListConversion.ConvertTo<T>(dataTable);
//  //    var result = new GridResult<T>().Data(dataList, totalCount);


//  //    return result;
//  //  }
//  //  catch (Exception ex)
//  //  {
//  //    throw ex;
//  //  }
//  //  finally
//  //  {
//  //    _connection.Close();
//  //  }
//  //}

//}
