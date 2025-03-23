using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Entities.CRMGrid.GRID;

public static class DateFormatter
{
  public static dynamic DateForQuery(DateTime date, DatabaseType dType)
  {
    if (dType == DatabaseType.Oracle)
    {
      return "to_date('" + date.ToShortDateString() + "','MM/DD/YYYY')";
    }
    if (dType == DatabaseType.SQL)
    {
      return "'" + date.ToString("MM/dd/yyyy") + "'";
    }
    else
    {
      return date;
    }
  }
  public static dynamic DateTimeString(DateTime date)
  {

    if (date == DateTime.MinValue)
    {
      return "NULL";
    }

    var d = "'" + date.ToString("MM/dd/yyyy  HH:mm:ss") + "'";
    return d;
  }

  public static dynamic Date(DateTime date)
  {

    if (date == DateTime.MinValue)
    {
      return "NULL";
    }

    return "'" + date.ToString("MM/dd/yyyy") + "'";

  }

  public static dynamic DateTimeForQuery(DateTime date, DatabaseType dType)
  {
    if (date == DateTime.MinValue)
    {
      return "NULL";
    }

    if (dType == DatabaseType.Oracle)
    {
      return "to_date('" + date.ToString("MM/dd/yyyy HH:mm:ss") + "','MM/DD/YYYY hh24:mi:ss')";
    }
    if (dType == DatabaseType.SQL)
    {
      var d = "'" + date.ToString("MM/dd/yyyy  HH:mm:ss") + "'";
      return d;
    }
    else
    {
      return date;
    }
  }

  public static dynamic MonthYearForQuery(DateTime date, DatabaseType dType)
  {
    if (date == DateTime.MinValue)
    {
      return "NULL";
    }
    if (dType == DatabaseType.Oracle)
    {
      return "to_date('" + date.ToString("MM/yyyy") + "','MM/YYYY')";
    }
    if (dType == DatabaseType.SQL)
    {
      var d = "'" + date.ToString("MM/yyyy") + "'";
      return d;
    }
    else
    {
      return date;
    }
  }

  public static dynamic MonthForQuery(DateTime date, DatabaseType dType)
  {
    if (date == DateTime.MinValue)
    {
      return "NULL";
    }
    var month = new DateTime(date.Year, date.Month, 1);
    if (dType == DatabaseType.Oracle)
    {
      return "to_date('" + month.ToString("MM/dd/yyyy") + "','MM/dd/YYYY')";
    }
    if (dType == DatabaseType.SQL)
    {
      var d = "'" + month.ToString("MM/dd/yyyy") + "'";
      return d;
    }
    else
    {
      return "";
    }
  }

  public static dynamic BetweenMonthYear(DateTime fromDate, DateTime toDate, string columnName, DatabaseType dType)
  {
    string strRv = "";
    if (dType == DatabaseType.Oracle)
    {
      strRv = string.Format(" {0} between to_date('{1}','MM/yyyy') and to_date('{2}','MM/yyyy') ", columnName,
          fromDate.ToString("MM/yyyy"), toDate.ToString("MM/yyyy"));
    }
    if (dType == DatabaseType.SQL)
    {
      strRv = string.Format(" RIGHT(CONVERT(VARCHAR(10), {0}, 103), 7)  between '{1}' and '{2}' ", columnName,
          fromDate.ToString("MM/yyyy"), toDate.ToString("MM/yyyy"));
    }

    return strRv;
    //  
  }
}
