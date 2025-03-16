using System.Reflection;

namespace bdDevCRM.Utilities.Common;

public class CommonHelper
{
  public static string ReplaceMultipleSpecificSpecialCharacters(string input, Dictionary<char, char> replacements)
  {
    // Iterate through the dictionary and replace each character
    foreach (var replacement in replacements)
    {
      input = input.Replace(replacement.Key, replacement.Value);
    }

    return input;
  }

  public static bool IsEncrypted(string value)
  {
    return value.StartsWith("enc_");
  }
}
