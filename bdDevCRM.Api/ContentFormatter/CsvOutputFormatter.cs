using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Collections;
using System.Reflection;
using System.Text;

namespace bdDevCRM.Api.ContentFormatter;

public class CsvOutputFormatter : TextOutputFormatter
{
  public CsvOutputFormatter()
  {
    SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
    SupportedEncodings.Add(Encoding.UTF8);
    SupportedEncodings.Add(Encoding.Unicode);
  }

  protected override bool CanWriteType(Type? type)
  {
    if (type == null)
      return false;

    if (type.IsClass && type != typeof(string))
    {
      return true;
    }

    if (type.IsGenericType && typeof(IEnumerable<>).IsAssignableFrom(type.GetGenericTypeDefinition()))
    {
      return true;
    }

    return false;
  }

  public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
  {
    var response = context.HttpContext.Response;
    var buffer = new StringBuilder();
    if (context.Object == null)
    {
      await response.WriteAsync(string.Empty);
      return;
    }

    Type objectType = context.Object.GetType();

    // Check if the object is an enumerable (not the generic definition)
    if (typeof(IEnumerable).IsAssignableFrom(objectType) && objectType != typeof(string))
    {
      // Get the element type
      Type itemType;
      if (objectType.IsGenericType)
      {
        itemType = objectType.GetGenericArguments()[0];
      }
      else
      {
        // For non-generic collections, use first item's type
        var enumerableItems = ((IEnumerable)context.Object).Cast<object>().ToList();
        if (enumerableItems.Count > 0)
        {
          itemType = enumerableItems.First().GetType();
          WriteCsv(buffer, enumerableItems, itemType);
        }
        else
        {
          // Empty collection
          await response.WriteAsync(string.Empty);
          return;
        }
      }

      if (objectType.IsGenericType)
      {
        WriteCsv(buffer, ((IEnumerable)context.Object).Cast<object>(), itemType);
      }
    }
    else
    {
      WriteCsv(buffer, new List<object> { context.Object }, objectType);
    }

    await response.WriteAsync(buffer.ToString());
  }

  private static void WriteCsv(StringBuilder buffer, IEnumerable<object> items, Type type)
  {
    var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                         .Where(p => p.GetIndexParameters().Length == 0)
                         .Where(p => p.DeclaringType == type)
                         .ToArray();

    if (!properties.Any())
      return;


    buffer.AppendLine(string.Join(",", properties.Select(p => p.Name)));

    foreach (var item in items)
    {
      var values = properties.Select(p => GetValueAsString(p, item));
      buffer.AppendLine(string.Join(",", values));
    }
  }


  private static string GetValueAsString(PropertyInfo property, object item)
  {
    try
    {
      object? value = property.GetValue(item, null);
      if (value == null) return "";
      if (value is string strValue) return $"\"{strValue}\""; 
      return value.ToString() ?? "";
    }
    catch (Exception)
    {
      return "";
    }
  }
}

