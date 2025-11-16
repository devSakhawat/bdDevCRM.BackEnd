

//namespace bdDevCRM.Entities.CRMGrid.GRID;

//public class CRMGridDataBuilder<T>
//{
//  #region for Grid

//  public static string Query(CRMGridOptions options, string query, string orderBy, string gridCondition)
//  {
//    string condition = "";


//    if (options != null)
//    {
//      condition = FilterCondition(options.filter).Trim();
//    }

//    if (!string.IsNullOrEmpty(condition))
//    {
//      condition = " WHERE " + condition;
//    }


//    if (!string.IsNullOrEmpty(gridCondition))
//    {
//      if (string.IsNullOrEmpty(condition))
//      {
//        condition = " WHERE " + gridCondition;
//      }
//      else
//      {
//        condition += " AND " + gridCondition;
//      }
//    }

//    string orderby = "";
//    if (options != null)
//    {
//      if (options.sort != null)
//      {
//        foreach (var gridSort in options.sort)
//        {
//          if (orderby == "")
//          {
//            orderby += "ORDER by " + gridSort.field + " " + gridSort.dir;
//          }
//          else
//          {
//            orderby += " , " + gridSort.field + " " + gridSort.dir;
//          }
//        }
//      }
//    }

//    if (orderby == "")
//    {
//      if (!String.IsNullOrEmpty(orderBy))
//      {
//        orderby = " ORDER BY " + orderBy;
//      }
//      else
//      {
//        throw new Exception("Must be set Orderby column Name");
//      }
//    }
//    int pageupperBound = 0;
//    int skip = 0;
//    if (options != null)
//    {
//      skip = options.skip;
//      pageupperBound = skip + options.take;
//    }
//    var sql =
//        string.Format(
//            @"SELECT * FROM (SELECT ROW_NUMBER() OVER({4}) AS RowIndex, T.* FROM ({0}) T {2}) tbl WHERE RowIndex >{1} AND RowIndex <={3}",
//            query, skip, condition, pageupperBound, orderby);

//    return sql;
//  }

//  public static string FilterCondition(CRMFilter.GridFilters filter)
//  {
//    string whereClause = "";

//    if (filter != null && (filter.Filters != null && filter.Filters.Count > 0))
//    {
//      var parameters = new List<object>();
//      var filters = filter.Filters;

//      for (var i = 0; i < filters.Count; i++)
//      {
//        if (i == 0)
//        {
//          if (filters[i].Value == null)
//          {
//            i = i + 1;
//            if (filters.Count == i)
//            {
//              break;
//            }
//          }

//          whereClause += string.Format(" {1}", CRMUtilityCommon.ToLinqOperator(filter.Logic), CRMUtilityCommon.BuildWhereClause<T>(i, filter.Logic,
//                     filters[i], parameters));
//        }
//        else
//        {
//          if (filters[i].Value != null)
//          {
//            whereClause += string.Format(" {0} {1}",  CRMUtilityCommon.ToLinqOperator(filter.Logic), CRMUtilityCommon.BuildWhereClause<T>(i, filter.Logic,
//                    filters[i], parameters));
//          }
//        }
//      }
//    }
//    return whereClause;
//  }

//  #endregion

//}
