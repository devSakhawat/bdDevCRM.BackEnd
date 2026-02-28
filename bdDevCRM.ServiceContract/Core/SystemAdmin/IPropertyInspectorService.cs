using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.ServiceContract.Core.SystemAdmin;

public interface IPropertyInspectorService
{
  PropertyMetadata GetPropertyMetadata(string className);
  T CreateInstance<T>() where T : class;
  Dictionary<string, object> GetFlattenedProperties(object instance);
}


// Supporting classes
public class PropertyMetadata
{
  public string ClassName { get; set; }
  public string FullName { get; set; }
  public List<PropertyDetail> Properties { get; set; }
}

public class PropertyDetail
{
  public string Name { get; set; }
  public string TypeName { get; set; }
  public string FullTypeName { get; set; }
  public bool IsNullable { get; set; }
  public bool IsComplex { get; set; }
  public bool CanRead { get; set; }
  public bool CanWrite { get; set; }
}
