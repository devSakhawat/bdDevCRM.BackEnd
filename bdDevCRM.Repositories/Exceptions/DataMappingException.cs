using System;

namespace bdDevCRM.Repositories.Exceptions
{
  /// <summary>
  /// Thrown when a data reader value cannot be mapped to a DTO/entity property.
  /// </summary>
  public sealed class DataMappingException : Exception
  {
    public string? ColumnName { get; }
    public string? PropertyName { get; }
    public string? PropertyType { get; }
    public string? EntityType { get; }
    public object? RawValue { get; }

    public DataMappingException(
      string message,
      string? columnName = null,
      string? propertyName = null,
      string? propertyType = null,
      string? entityType = null,
      object? rawValue = null,
      Exception? inner = null)
      : base(message, inner)
    {
      ColumnName = columnName;
      PropertyName = propertyName;
      PropertyType = propertyType;
      EntityType = entityType;
      RawValue = rawValue;
    }
  }
}
