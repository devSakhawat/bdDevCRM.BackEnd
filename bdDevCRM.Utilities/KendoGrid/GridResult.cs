//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace bdDevCRM.Utilities.KendoGrid;


//public static class GridResult<T>
//{
//  public static GridEntity<T> Data(List<T> list, GridOptions options)
//  {
//    GridEntity<T> gridEntity = new GridEntity<T>();
//    gridEntity.Items = new List<T>();
//    List<T> list2 = new List<T>();
//    int count = (options.Page - 1) * options.PageSize;
//    IQueryable<T> source = list.AsQueryable();
//    if (options.Filter != null)
//    {
//      foreach (CRMFilter.GridFilter filter in options.Filter.Filters)
//      {
//        filter.Field = char.ToUpper(filter.Field.First()) + filter.Field.Substring(1);
//        if (filter.Field.Contains("."))
//        {
//          string[] array = filter.Field.Split(".");
//          int num = array.Count();
//          if (num != 2)
//          {
//            continue;
//          }

//          string name = char.ToUpper(array[0].First()) + array[0].Substring(1);
//          string name2 = char.ToUpper(array[1].First()) + array[1].Substring(1);
//          int num2 = 0;
//          List<T> list3 = new List<T>();
//          list = (List<T>)((gridEntity.Items.Count > 0) ? ((IList)(List<T>)gridEntity.Items) : ((IList)list));
//          foreach (T item in list)
//          {
//            num2++;
//            PropertyInfo property = item.GetType().GetProperty(name);
//            if (property == null)
//            {
//              name = char.ToLower(array[0].First()) + array[0].Substring(1);
//              property = item.GetType().GetProperty(name);
//            }

//            string text = array[1];
//            object value = property.GetValue(item, null);
//            PropertyInfo property2 = value.GetType().GetProperty(name2);
//            string text2 = ((property2.GetValue(value, null) == null) ? "" : property2.GetValue(value, null).ToString());
//            try
//            {
//              Regex regex = new Regex("\\d\\d\\d\\d-\\d\\d-\\d\\d");
//              if (regex.Match(filter.Value).Success)
//              {
//                filter.Value = Convert.ToDateTime(filter.Value).ToString("yyyy-MM-dd");
//                text2 = Convert.ToDateTime(text2).ToString("yyyy-MM-dd");
//              }
//            }
//            catch (Exception)
//            {
//            }

//            if (filter.Operator.Equals("contains"))
//            {
//              if (filter.Value.ToLower().Equals("active") || filter.Value.ToLower().Equals("inactive"))
//              {
//                string text3 = filter.Value.ToLower();
//                if ("active".Contains(text3.ToLower()))
//                {
//                  if (text2.ToLower().Equals("true"))
//                  {
//                    list3.Add(item);
//                  }
//                }
//                else if ("inactive".Contains(text3.ToLower()) && text2.ToLower().Equals("false"))
//                {
//                  list3.Add(item);
//                }
//              }
//              else if (filter.Value.ToLower().Equals("yes") || filter.Value.ToLower().Equals("no"))
//              {
//                string text4 = filter.Value.ToLower();
//                if ("yes".Contains(text4.ToLower()))
//                {
//                  if (text2.ToLower().Equals("true"))
//                  {
//                    list3.Add(item);
//                  }
//                }
//                else if ("no".Contains(text4.ToLower()) && text2.ToLower().Equals("false"))
//                {
//                  list3.Add(item);
//                }
//              }
//              else if (text2.ToLower().Contains(filter.Value.ToLower()))
//              {
//                list3.Add(item);
//              }
//            }
//            else if (filter.Operator.Equals("doesnotcontain"))
//            {
//              if (filter.Value.ToLower().Equals("active") || filter.Value.ToLower().Equals("inactive"))
//              {
//                string text5 = filter.Value.ToLower();
//                if ("active".Contains(text5.ToLower()))
//                {
//                  if (text2.ToLower().Equals("false"))
//                  {
//                    list3.Add(item);
//                  }
//                }
//                else if ("inactive".Contains(text5.ToLower()) && text2.ToLower().Equals("true"))
//                {
//                  list3.Add(item);
//                }
//              }
//              else if (filter.Value.ToLower().Equals("yes") || filter.Value.ToLower().Equals("no"))
//              {
//                string text6 = filter.Value.ToLower();
//                if ("yes".Contains(text6.ToLower()))
//                {
//                  if (text2.ToLower().Equals("false"))
//                  {
//                    list3.Add(item);
//                  }
//                }
//                else if ("no".Contains(text6.ToLower()) && text2.ToLower().Equals("true"))
//                {
//                  list3.Add(item);
//                }
//              }
//              else if (!text2.ToLower().Contains(filter.Value.ToLower()))
//              {
//                list3.Add(item);
//              }
//            }
//            else if (filter.Operator.Equals("startswith"))
//            {
//              if (filter.Value.ToLower().Equals("active") || filter.Value.ToLower().Equals("inactive"))
//              {
//                string text7 = filter.Value.ToLower();
//                if ("active".Contains(text7.ToLower()))
//                {
//                  if (text2.ToLower().Equals("true"))
//                  {
//                    list3.Add(item);
//                  }
//                }
//                else if ("inactive".Contains(text7.ToLower()) && text2.ToLower().Equals("false"))
//                {
//                  list3.Add(item);
//                }
//              }
//              else if (filter.Value.ToLower().Equals("yes") || filter.Value.ToLower().Equals("no"))
//              {
//                string text8 = filter.Value.ToLower();
//                if ("yes".Contains(text8.ToLower()))
//                {
//                  if (text2.ToLower().Equals("true"))
//                  {
//                    list3.Add(item);
//                  }
//                }
//                else if ("no".Contains(text8.ToLower()) && text2.ToLower().Equals("false"))
//                {
//                  list3.Add(item);
//                }
//              }
//              else if (text2.ToLower().StartsWith(filter.Value.ToLower()))
//              {
//                list3.Add(item);
//              }
//            }
//            else if (filter.Operator.Equals("endswith"))
//            {
//              if (filter.Value.ToLower().Equals("active") || filter.Value.ToLower().Equals("inactive"))
//              {
//                string text9 = filter.Value.ToLower();
//                if ("active".Contains(text9.ToLower()))
//                {
//                  if (text2.ToLower().Equals("true"))
//                  {
//                    list3.Add(item);
//                  }
//                }
//                else if ("inactive".Contains(text9.ToLower()) && text2.ToLower().Equals("false"))
//                {
//                  list3.Add(item);
//                }
//              }
//              else if (filter.Value.ToLower().Equals("yes") || filter.Value.ToLower().Equals("no"))
//              {
//                string text10 = filter.Value.ToLower();
//                if ("yes".Contains(text10.ToLower()))
//                {
//                  if (text2.ToLower().Equals("true"))
//                  {
//                    list3.Add(item);
//                  }
//                }
//                else if ("no".Contains(text10.ToLower()) && text2.ToLower().Equals("false"))
//                {
//                  list3.Add(item);
//                }
//              }
//              else if (text2.ToLower().EndsWith(filter.Value.ToLower()))
//              {
//                list3.Add(item);
//              }
//            }
//            else if (filter.Operator.Equals("neq"))
//            {
//              if (filter.Value.ToLower().Equals("active") || filter.Value.ToLower().Equals("inactive"))
//              {
//                string text11 = filter.Value.ToLower();
//                if ("active".Contains(text11.ToLower()))
//                {
//                  if (text2.ToLower().Equals("false"))
//                  {
//                    list3.Add(item);
//                  }
//                }
//                else if ("inactive".Contains(text11.ToLower()) && text2.ToLower().Equals("true"))
//                {
//                  list3.Add(item);
//                }
//              }
//              else if (filter.Value.ToLower().Equals("yes") || filter.Value.ToLower().Equals("no"))
//              {
//                string text12 = filter.Value.ToLower();
//                if ("yes".Contains(text12.ToLower()))
//                {
//                  if (text2.ToLower().Equals("false"))
//                  {
//                    list3.Add(item);
//                  }
//                }
//                else if ("no".Contains(text12.ToLower()) && text2.ToLower().Equals("true"))
//                {
//                  list3.Add(item);
//                }
//              }
//              else if (filter.Value.ToLower() != text2.ToLower())
//              {
//                list3.Add(item);
//              }
//            }
//            else if (filter.Operator.Equals("gt"))
//            {
//              try
//              {
//                DateTime dateTime = Convert.ToDateTime(filter.Value);
//                DateTime dateTime2 = Convert.ToDateTime(value);
//                if (dateTime < dateTime2)
//                {
//                  list3.Add(item);
//                }
//              }
//              catch (Exception)
//              {
//              }
//            }
//            else if (filter.Operator.Equals("gte"))
//            {
//              try
//              {
//                DateTime dateTime3 = Convert.ToDateTime(filter.Value);
//                DateTime dateTime4 = Convert.ToDateTime(value);
//                if (dateTime3 <= dateTime4)
//                {
//                  list3.Add(item);
//                }
//              }
//              catch (Exception)
//              {
//              }
//            }
//            else if (filter.Operator.Equals("lt"))
//            {
//              try
//              {
//                DateTime dateTime5 = Convert.ToDateTime(filter.Value);
//                DateTime dateTime6 = Convert.ToDateTime(value);
//                if (dateTime5 > dateTime6)
//                {
//                  list3.Add(item);
//                }
//              }
//              catch (Exception)
//              {
//              }
//            }
//            else if (filter.Operator.Equals("lte"))
//            {
//              try
//              {
//                DateTime dateTime7 = Convert.ToDateTime(filter.Value);
//                DateTime dateTime8 = Convert.ToDateTime(value);
//                if (dateTime7 >= dateTime8)
//                {
//                  list3.Add(item);
//                }
//              }
//              catch (Exception)
//              {
//              }
//            }
//            else if (filter.Value.ToLower().Equals("active") || filter.Value.ToLower().Equals("inactive"))
//            {
//              string text13 = filter.Value.ToLower();
//              if ("active".Contains(text13.ToLower()))
//              {
//                if (text2.ToLower().Equals("true"))
//                {
//                  list3.Add(item);
//                }
//              }
//              else if ("inactive".Contains(text13.ToLower()) && text2.ToLower().Equals("false"))
//              {
//                list3.Add(item);
//              }
//            }
//            else if (filter.Value.ToLower().Equals("yes") || filter.Value.ToLower().Equals("no"))
//            {
//              string text14 = filter.Value.ToLower();
//              if ("yes".Contains(text14.ToLower()))
//              {
//                if (text2.ToLower().Equals("true"))
//                {
//                  list3.Add(item);
//                }
//              }
//              else if ("no".Contains(text14.ToLower()) && text2.ToLower().Equals("false"))
//              {
//                list3.Add(item);
//              }
//            }
//            else if (filter.Value.ToLower() == text2.ToLower())
//            {
//              list3.Add(item);
//            }
//          }

//          if (list3.Count > 0)
//          {
//            if (gridEntity.TotalCount > 0 && gridEntity.TotalCount != list.Count)
//            {
//              if (options.Filter.Logic == "or")
//              {
//                for (int i = 0; i < list3.Count; i++)
//                {
//                  int num3 = 0;
//                  PropertyInfo property3 = list3[i].GetType().GetProperty(filter.Field);
//                  if (property3 == null)
//                  {
//                    continue;
//                  }

//                  string text15 = property3.GetValue(list3[i], null).ToString();
//                  foreach (T item2 in list2)
//                  {
//                    PropertyInfo property4 = item2.GetType().GetProperty(filter.Field);
//                    if (!(property4 == null))
//                    {
//                      string text16 = property4.GetValue(item2, null).ToString();
//                      if (text15.ToLower() == text16.ToLower())
//                      {
//                        num3 = 1;
//                      }
//                    }
//                  }

//                  if (num3 == 0)
//                  {
//                    gridEntity.Items.Add(list3[i]);
//                    gridEntity.TotalCount++;
//                    list2.Add(list3[i]);
//                  }
//                }
//              }
//              else
//              {
//                gridEntity.Items = list3.Skip(count).Take(options.PageSize).ToList();
//                gridEntity.TotalCount = list3.Count();
//                list2 = list3;
//              }
//            }

//            gridEntity.Items = list3.Skip(count).Take(options.PageSize).ToList();
//            gridEntity.TotalCount = list3.Count();
//            list2 = list3;
//          }
//          else
//          {
//            gridEntity.Items = null;
//            gridEntity.TotalCount = 0;
//          }

//          continue;
//        }

//        try
//        {
//          List<T> list4 = new List<T>();
//          int num4 = 0;
//          if (gridEntity.TotalCount > 0 && gridEntity.TotalCount != list.Count && options.Filter.Logic == "and")
//          {
//            list = (List<T>)((gridEntity.Items.Count > 0) ? ((IList)(List<T>)gridEntity.Items) : ((IList)list));
//          }

//          foreach (T item3 in list)
//          {
//            num4++;
//            PropertyInfo property5 = item3.GetType().GetProperty(filter.Field);
//            if (property5 == null)
//            {
//              property5 = item3.GetType().GetProperty(filter.Field.ToLower());
//            }

//            string text17 = ((property5.GetValue(item3, null) == null) ? "" : property5.GetValue(item3, null).ToString());
//            try
//            {
//              Regex regex2 = new Regex("\\d\\d\\d\\d-\\d\\d-\\d\\d");
//              if (regex2.Match(filter.Value).Success)
//              {
//                filter.Value = Convert.ToDateTime(filter.Value).ToString("yyyy-MM-dd");
//                text17 = Convert.ToDateTime(text17).ToString("yyyy-MM-dd");
//              }
//            }
//            catch (Exception)
//            {
//            }

//            if (filter.Operator.Equals("contains"))
//            {
//              if (filter.Value.ToLower().Equals("active") || filter.Value.ToLower().Equals("inactive"))
//              {
//                string text18 = filter.Value.ToLower();
//                if ("active".Contains(text18.ToLower()))
//                {
//                  if (text17.ToLower().Equals("true"))
//                  {
//                    list4.Add(item3);
//                  }
//                }
//                else if ("inactive".Contains(text18.ToLower()) && text17.ToLower().Equals("false"))
//                {
//                  list4.Add(item3);
//                }
//              }
//              else if (filter.Value.ToLower().Equals("yes") || filter.Value.ToLower().Equals("no"))
//              {
//                string text19 = filter.Value.ToLower();
//                if ("yes".Contains(text19.ToLower()))
//                {
//                  if (text17.ToLower().Equals("true"))
//                  {
//                    list4.Add(item3);
//                  }
//                }
//                else if ("no".Contains(text19.ToLower()) && text17.ToLower().Equals("false"))
//                {
//                  list4.Add(item3);
//                }
//              }
//              else if (text17.ToLower().Contains(filter.Value.ToLower()))
//              {
//                list4.Add(item3);
//              }
//            }
//            else if (filter.Operator.Equals("doesnotcontain"))
//            {
//              if (filter.Value.ToLower().Equals("active") || filter.Value.ToLower().Equals("inactive"))
//              {
//                string text20 = filter.Value.ToLower();
//                if ("active".Contains(text20.ToLower()))
//                {
//                  if (text17.ToLower().Equals("false"))
//                  {
//                    list4.Add(item3);
//                  }
//                }
//                else if ("inactive".Contains(text20.ToLower()) && text17.ToLower().Equals("true"))
//                {
//                  list4.Add(item3);
//                }
//              }
//              else if (filter.Value.ToLower().Equals("yes") || filter.Value.ToLower().Equals("no"))
//              {
//                string text21 = filter.Value.ToLower();
//                if ("yes".Contains(text21.ToLower()))
//                {
//                  if (text17.ToLower().Equals("false"))
//                  {
//                    list4.Add(item3);
//                  }
//                }
//                else if ("no".Contains(text21.ToLower()) && text17.ToLower().Equals("true"))
//                {
//                  list4.Add(item3);
//                }
//              }
//              else if (!text17.ToLower().Contains(filter.Value.ToLower()))
//              {
//                list4.Add(item3);
//              }
//            }
//            else if (filter.Operator.Equals("startswith"))
//            {
//              if (filter.Value.ToLower().Equals("active") || filter.Value.ToLower().Equals("inactive"))
//              {
//                string text22 = filter.Value.ToLower();
//                if ("active".Contains(text22.ToLower()))
//                {
//                  if (text17.ToLower().Equals("true"))
//                  {
//                    list4.Add(item3);
//                  }
//                }
//                else if ("inactive".Contains(text22.ToLower()) && text17.ToLower().Equals("false"))
//                {
//                  list4.Add(item3);
//                }
//              }
//              else if (filter.Value.ToLower().Equals("yes") || filter.Value.ToLower().Equals("no"))
//              {
//                string text23 = filter.Value.ToLower();
//                if ("yes".Contains(text23.ToLower()))
//                {
//                  if (text17.ToLower().Equals("true"))
//                  {
//                    list4.Add(item3);
//                  }
//                }
//                else if ("no".Contains(text23.ToLower()) && text17.ToLower().Equals("false"))
//                {
//                  list4.Add(item3);
//                }
//              }
//              else if (text17.ToLower().StartsWith(filter.Value.ToLower()))
//              {
//                list4.Add(item3);
//              }
//            }
//            else if (filter.Operator.Equals("endswith"))
//            {
//              if (filter.Value.ToLower().Equals("active") || filter.Value.ToLower().Equals("inactive"))
//              {
//                string text24 = filter.Value.ToLower();
//                if ("active".Contains(text24.ToLower()))
//                {
//                  if (text17.ToLower().Equals("true"))
//                  {
//                    list4.Add(item3);
//                  }
//                }
//                else if ("inactive".Contains(text24.ToLower()) && text17.ToLower().Equals("false"))
//                {
//                  list4.Add(item3);
//                }
//              }
//              else if (filter.Value.ToLower().Equals("yes") || filter.Value.ToLower().Equals("no"))
//              {
//                string text25 = filter.Value.ToLower();
//                if ("yes".Contains(text25.ToLower()))
//                {
//                  if (text17.ToLower().Equals("true"))
//                  {
//                    list4.Add(item3);
//                  }
//                }
//                else if ("no".Contains(text25.ToLower()) && text17.ToLower().Equals("false"))
//                {
//                  list4.Add(item3);
//                }
//              }
//              else if (text17.ToLower().EndsWith(filter.Value.ToLower()))
//              {
//                list4.Add(item3);
//              }
//            }
//            else if (filter.Operator.Equals("neq"))
//            {
//              if (filter.Value.ToLower().Equals("active") || filter.Value.ToLower().Equals("inactive"))
//              {
//                string text26 = filter.Value.ToLower();
//                if ("active".Contains(text26.ToLower()))
//                {
//                  if (text17.ToLower().Equals("false"))
//                  {
//                    list4.Add(item3);
//                  }
//                }
//                else if ("inactive".Contains(text26.ToLower()) && text17.ToLower().Equals("true"))
//                {
//                  list4.Add(item3);
//                }
//              }
//              else if (filter.Value.ToLower().Equals("yes") || filter.Value.ToLower().Equals("no"))
//              {
//                string text27 = filter.Value.ToLower();
//                if ("yes".Contains(text27.ToLower()))
//                {
//                  if (text17.ToLower().Equals("false"))
//                  {
//                    list4.Add(item3);
//                  }
//                }
//                else if ("no".Contains(text27.ToLower()) && text17.ToLower().Equals("true"))
//                {
//                  list4.Add(item3);
//                }
//              }
//              else if (filter.Value.ToLower() != text17.ToLower())
//              {
//                list4.Add(item3);
//              }
//            }
//            else if (filter.Operator.Equals("gt"))
//            {
//              try
//              {
//                DateTime dateTime9 = Convert.ToDateTime(filter.Value);
//                DateTime dateTime10 = Convert.ToDateTime(text17);
//                if (dateTime9 < dateTime10)
//                {
//                  list4.Add(item3);
//                }
//              }
//              catch (Exception)
//              {
//              }
//            }
//            else if (filter.Operator.Equals("gte"))
//            {
//              try
//              {
//                DateTime dateTime11 = Convert.ToDateTime(filter.Value);
//                DateTime dateTime12 = Convert.ToDateTime(text17);
//                if (dateTime11 <= dateTime12)
//                {
//                  list4.Add(item3);
//                }
//              }
//              catch (Exception)
//              {
//              }
//            }
//            else if (filter.Operator.Equals("lt"))
//            {
//              try
//              {
//                DateTime dateTime13 = Convert.ToDateTime(filter.Value);
//                DateTime dateTime14 = Convert.ToDateTime(text17);
//                if (dateTime13 > dateTime14)
//                {
//                  list4.Add(item3);
//                }
//              }
//              catch (Exception)
//              {
//              }
//            }
//            else if (filter.Operator.Equals("lte"))
//            {
//              try
//              {
//                DateTime dateTime15 = Convert.ToDateTime(filter.Value);
//                DateTime dateTime16 = Convert.ToDateTime(text17);
//                if (dateTime15 >= dateTime16)
//                {
//                  list4.Add(item3);
//                }
//              }
//              catch (Exception)
//              {
//              }
//            }
//            else if (filter.Value.ToLower().Equals("active") || filter.Value.ToLower().Equals("inactive"))
//            {
//              string text28 = filter.Value.ToLower();
//              if ("active".Contains(text28.ToLower()))
//              {
//                if (text17.ToLower().Equals("true"))
//                {
//                  list4.Add(item3);
//                }
//              }
//              else if ("inactive".Contains(text28.ToLower()) && text17.ToLower().Equals("false"))
//              {
//                list4.Add(item3);
//              }
//            }
//            else if (filter.Value.ToLower().Equals("yes") || filter.Value.ToLower().Equals("no"))
//            {
//              string text29 = filter.Value.ToLower();
//              if ("yes".Contains(text29.ToLower()))
//              {
//                if (text17.ToLower().Equals("true"))
//                {
//                  list4.Add(item3);
//                }
//              }
//              else if ("no".Contains(text29.ToLower()) && text17.ToLower().Equals("false"))
//              {
//                list4.Add(item3);
//              }
//            }
//            else if (filter.Value.ToLower() == text17.ToLower())
//            {
//              list4.Add(item3);
//            }
//          }

//          if (list4.Count > 0)
//          {
//            if (gridEntity.TotalCount > 0 && gridEntity.TotalCount != list.Count)
//            {
//              if (options.Filter.Logic == "or")
//              {
//                for (int j = 0; j < list4.Count; j++)
//                {
//                  int num5 = 0;
//                  PropertyInfo property6 = list4[j].GetType().GetProperty(filter.Field);
//                  if (property6 == null)
//                  {
//                    continue;
//                  }

//                  string text30 = property6.GetValue(list4[j], null).ToString();
//                  foreach (T item4 in list2)
//                  {
//                    PropertyInfo property7 = item4.GetType().GetProperty(filter.Field);
//                    if (!(property7 == null))
//                    {
//                      string text31 = property7.GetValue(item4, null).ToString();
//                      if (text30.ToLower() == text31.ToLower())
//                      {
//                        num5 = 1;
//                      }
//                    }
//                  }

//                  if (num5 == 0)
//                  {
//                    gridEntity.Items.Add(list4[j]);
//                    gridEntity.TotalCount++;
//                    list2.Add(list4[j]);
//                  }
//                }
//              }
//              else
//              {
//                gridEntity.Items = list4.Skip(count).Take(options.PageSize).ToList();
//                gridEntity.TotalCount = list4.Count();
//                list2 = list4;
//              }
//            }
//            else
//            {
//              gridEntity.Items = list4.Skip(count).Take(options.PageSize).ToList();
//              gridEntity.TotalCount = list4.Count();
//              list2 = list4;
//            }
//          }
//          else
//          {
//            gridEntity.Items = null;
//            gridEntity.TotalCount = 0;
//          }
//        }
//        catch (Exception)
//        {
//        }
//      }
//    }
//    else if (source.Any())
//    {
//      IQueryable<T> source2 = source.Skip(count).Take(options.PageSize);
//      gridEntity.Items = source2.ToList();
//      gridEntity.TotalCount = (int)source.LongCount();
//    }

//    return gridEntity;
//  }

//  public static GridEntity<T> Data(List<T> list, GridOptionsWithAdditionalParam options)
//  {
//    GridEntity<T> gridEntity = new GridEntity<T>();
//    int count = (options.Page - 1) * options.PageSize;
//    IQueryable<T> source = list.AsQueryable();
//    if (options.Filter != null)
//    {
//      foreach (CRMFilter.GridFilter filter in options.Filter.Filters)
//      {
//        filter.Field = char.ToUpper(filter.Field.First()) + filter.Field.Substring(1).ToLower();
//        source = source.Where((T p) => p.GetType().GetProperty(filter.Field).GetValue(p, null)
//          .ToString()
//          .Contains(filter.Value.ToString()));
//      }
//    }

//    if (source.Any())
//    {
//      IQueryable<T> source2 = source.Skip(count).Take(options.PageSize);
//      gridEntity.Items = source2.ToList();
//      gridEntity.TotalCount = list.Count();
//    }
//    else
//    {
//      gridEntity.Items = null;
//      gridEntity.TotalCount = 0;
//    }

//    return gridEntity;
//  }
//}

//public class GridEntity<T>
//{
//  public IList<T> Items { get; set; }

//  public int TotalCount { get; set; }
//}

//public class GridOptionsWithAdditionalParam
//{
//  public int Skip { get; set; }

//  public int Take { get; set; }

//  public int Page { get; set; }

//  public int PageSize { get; set; }

//  public List<CRMFilter.GridSort>? Sort { get; set; }

//  public CRMFilter.GridFilters? Filter { get; set; }

//  public int CompanyId { get; set; }

//  public int HrRecordId { get; set; }

//  public string ProcessMonth { get; set; }

//  public int OrgSbuId { get; set; }

//  public int ProductTypeId { get; set; }

//  public int KpiId { get; set; }

//  public int IdentityColumnForEachGridHistory { get; set; }
//}
