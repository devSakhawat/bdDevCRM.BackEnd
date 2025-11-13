using Newtonsoft.Json;

namespace bdDevCRM.Utilities.OthersLibrary;

public class MyMapper
{
  public static T Map<T>(object source)
  {
    var sourceType = source.GetType();
    var destinationType = typeof(T);
    var destinationProperties = destinationType.GetProperties();
    var sourceProperties = sourceType.GetProperties();
    var destination = Activator.CreateInstance<T>();
    foreach (var destinationProperty in destinationProperties)
    {
      var sourceProperty = sourceProperties.FirstOrDefault(x => x.Name == destinationProperty.Name);
      if (sourceProperty != null)
      {
        destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
      }
    }
    return destination;
  }

  // how to use
  // var source = new Source { Name = "Name", Age = 10 };
  // var destination = MyMapper.Map<Destination>(source);
  // example : var clonedObjectJson = MyClone.ModelMapping<Module, ModuleHistory>(oldData);
  public static TDestination ModelMapping<TSource, TDestination>(TSource source)
  {
    string text = JsonConvert.SerializeObject((object)source);
    return JsonConvert.DeserializeObject<TDestination>(text);
  }


  public static TTarget JsonClone<TSource, TTarget>(TSource source) where TTarget : new()
  {
    if (source == null) throw new ArgumentNullException(nameof(source));
    var serialized = JsonConvert.SerializeObject(source);
    var target = JsonConvert.DeserializeObject<TTarget>(serialized);

    return target;
  }

  public static TTarget JsonCloneSafe<TSource, TTarget>(TSource source) where TTarget : new()
  {
    if (source == null) throw new ArgumentNullException(nameof(source));

    var settings = new JsonSerializerSettings
    {
      NullValueHandling = NullValueHandling.Ignore,
      MissingMemberHandling = MissingMemberHandling.Ignore,
      DateTimeZoneHandling = DateTimeZoneHandling.Utc,
      Error = (sender, args) =>
      {
        // Handle DateTime conversion errors
        if (args.ErrorContext.Error is InvalidCastException &&
            args.ErrorContext.Path.Contains("Date"))
        {
          // Set default DateTime for null values
          args.ErrorContext.Handled = true;
        }
      }
    };

    var serialized = JsonConvert.SerializeObject(source, settings);
    var target = JsonConvert.DeserializeObject<TTarget>(serialized, settings) ?? new TTarget();

    ProcessDateTimeFields(target);

    return target;
  }

  /// <summary>
  /// Checke DateTime properties. If there are  DateTime.MinValue then set null.
  /// </summary>
  /// <typeparam name="T">Target object type</typeparam>
  /// <param name="obj">check Object DateTime properties</param>
  private static void ProcessDateTimeFields<T>(T obj)
  {
    if (obj == null) return;

    var type = typeof(T);
    var properties = type.GetProperties();

    foreach (var property in properties)
    {
      if (property.PropertyType == typeof(DateTime) ||
          property.PropertyType == typeof(DateTime?))
      {
        try
        {
          var value = property.GetValue(obj);

          if (value is DateTime dateTimeValue && dateTimeValue == DateTime.MinValue)
          {
            if (property.PropertyType == typeof(DateTime?))
            {
              property.SetValue(obj, null);
            }
          }
        }
        catch (Exception)
        {
          continue;
        }
      }
    }
  }


  // IEnumerable<Country> countries = await _repository.Countries.GetCountriesAsync(trackChanges);
  //List<CountryDto> countryDtos = MyMapper.JsonCloneIEnumerableToList<Country, CountryDto>(countries);
  public static List<TTarget> JsonCloneIEnumerableToList<TSource, TTarget>(IEnumerable<TSource> sourceList)
  {
    if (sourceList == null) throw new ArgumentNullException(nameof(sourceList));

    var serialized = JsonConvert.SerializeObject(sourceList);
    var targetList = JsonConvert.DeserializeObject<List<TTarget>>(serialized);

    return targetList;
  }

  public static IEnumerable<TTarget> JsonCloneIEnumerableToIEnumerable<TSource, TTarget>(List<TSource> sourceList)
  {
    if (sourceList == null) throw new ArgumentNullException(nameof(sourceList));

    var serialized = JsonConvert.SerializeObject(sourceList);
    var targetList = JsonConvert.DeserializeObject<IEnumerable<TTarget>>(serialized);

    return targetList;
  }

  public static List<TTarget> JsonCloneListToList<TSource, TTarget>(List<TSource> sourceList)
  {
    if (sourceList == null) throw new ArgumentNullException(nameof(sourceList));

    var serialized = JsonConvert.SerializeObject(sourceList);
    var targetList = JsonConvert.DeserializeObject<List<TTarget>>(serialized);

    return targetList;
  }

  public static IEnumerable<TTarget> JsonCloneIEnumerableToIEnumerable<TSource, TTarget>(IEnumerable<TSource> sourceList)
  {
    if (sourceList == null) throw new ArgumentNullException(nameof(sourceList));

    var serialized = JsonConvert.SerializeObject(sourceList);
    var targetList = JsonConvert.DeserializeObject<IEnumerable<TTarget>>(serialized);

    return targetList;
  }
}


public static class JsonSafeDeserializer
{
  public static T SafeDeserialize<T>(string json) where T : new()
  {
    var settings = new JsonSerializerSettings
    {
      NullValueHandling = NullValueHandling.Ignore,
      MissingMemberHandling = MissingMemberHandling.Ignore,
      Error = (sender, args) =>
      {
        Console.WriteLine("JSON Error at: " + args.ErrorContext.Path);
        args.ErrorContext.Handled = true;
      }
    };

    return JsonConvert.DeserializeObject<T>(json, settings) ?? new T();
  }
}




