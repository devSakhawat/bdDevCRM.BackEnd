using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace bdDevCRM.Api.Formatters;

/// <summary>
/// Custom CSV output formatter for content negotiation
/// Converts object collections to CSV format
/// </summary>
public class CsvOutputFormatter : Microsoft.AspNetCore.Mvc.Formatters.TextOutputFormatter
{
    public CsvOutputFormatter()
    {
        SupportedMediaTypes.Add("text/csv");
        SupportedMediaTypes.Add("application/csv");
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    protected override bool CanWriteType(Type type)
    {
        // Support collections and single objects
        if (type == null)
            return false;

        // Check if it's a collection
        if (typeof(System.Collections.IEnumerable).IsAssignableFrom(type))
            return true;

        // Check if it's a single object that can be serialized
        return type.IsClass;
    }

    public override async Task WriteResponseBodyAsync(
        Microsoft.AspNetCore.Mvc.Formatters.OutputFormatterWriteContext context,
        Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;
        var value = context.Object;

        if (value == null)
        {
            await response.WriteAsync(string.Empty);
            return;
        }

        var csv = ConvertToCsv(value);
        await response.WriteAsync(csv, selectedEncoding);
    }

    private string ConvertToCsv(object data)
    {
        if (data == null)
            return string.Empty;

        var sb = new StringBuilder();
        var type = data.GetType();

        // Handle collections
        if (data is System.Collections.IEnumerable enumerable and not string)
        {
            var items = enumerable.Cast<object>().ToList();
            if (!items.Any())
                return string.Empty;

            // Get properties from first item
            var itemType = items.First().GetType();
            var properties = itemType.GetProperties()
                .Where(p => p.CanRead && IsSimpleType(p.PropertyType))
                .ToList();

            // Write header
            sb.AppendLine(string.Join(",", properties.Select(p => EscapeCsv(p.Name))));

            // Write data rows
            foreach (var item in items)
            {
                var values = properties.Select(p =>
                {
                    var value = p.GetValue(item);
                    return EscapeCsv(value?.ToString() ?? string.Empty);
                });
                sb.AppendLine(string.Join(",", values));
            }
        }
        else
        {
            // Handle single object
            var properties = type.GetProperties()
                .Where(p => p.CanRead && IsSimpleType(p.PropertyType))
                .ToList();

            // Write header
            sb.AppendLine(string.Join(",", properties.Select(p => EscapeCsv(p.Name))));

            // Write data row
            var values = properties.Select(p =>
            {
                var value = p.GetValue(data);
                return EscapeCsv(value?.ToString() ?? string.Empty);
            });
            sb.AppendLine(string.Join(",", values));
        }

        return sb.ToString();
    }

    private bool IsSimpleType(Type type)
    {
        return type.IsPrimitive ||
               type == typeof(string) ||
               type == typeof(decimal) ||
               type == typeof(DateTime) ||
               type == typeof(DateTimeOffset) ||
               type == typeof(Guid) ||
               Nullable.GetUnderlyingType(type) != null;
    }

    private string EscapeCsv(string value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        // Escape quotes and wrap in quotes if necessary
        if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
        {
            return $"\"{value.Replace("\"", "\"\"")}\"";
        }

        return value;
    }
}

/// <summary>
/// Enhanced XML formatter with support for StandardApiResponse
/// </summary>
public class EnhancedXmlFormatter : Microsoft.AspNetCore.Mvc.Formatters.XmlSerializerOutputFormatter
{
    public EnhancedXmlFormatter()
    {
        SupportedMediaTypes.Add("application/xml");
        SupportedMediaTypes.Add("text/xml");
    }

    protected override bool CanWriteType(Type type)
    {
        // Support all serializable types
        return base.CanWriteType(type) || type.IsSerializable;
    }

    public override async Task WriteResponseBodyAsync(
        Microsoft.AspNetCore.Mvc.Formatters.OutputFormatterWriteContext context,
        Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;
        var value = context.Object;

        if (value == null)
        {
            await response.WriteAsync(string.Empty, selectedEncoding);
            return;
        }

        var settings = new XmlWriterSettings
        {
            Encoding = selectedEncoding,
            Indent = true,
            IndentChars = "  ",
            OmitXmlDeclaration = false,
            Async = true
        };

        using var writer = XmlWriter.Create(response.Body, settings);
        var serializer = new XmlSerializer(value.GetType());
        serializer.Serialize(writer, value);
        await writer.FlushAsync();
    }
}
