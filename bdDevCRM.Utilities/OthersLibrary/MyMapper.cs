using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

  public static TTarget JsonClone<TSource, TTarget>(TSource source) where TTarget : new()
  {
    if (source == null) throw new ArgumentNullException(nameof(source));
    var serialized = JsonConvert.SerializeObject(source);
    var target = JsonConvert.DeserializeObject<TTarget>(serialized);

    return target;
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

  public static IEnumerable<TTarget> JsonCloneListToIEnumerable<TSource, TTarget>(List<TSource> sourceList)
  {
    if (sourceList == null) throw new ArgumentNullException(nameof(sourceList));

    var serialized = JsonConvert.SerializeObject(sourceList);
    var targetList = JsonConvert.DeserializeObject<List<TTarget>>(serialized);

    return targetList;
  }
}