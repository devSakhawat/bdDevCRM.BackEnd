using System.Text.RegularExpressions;

namespace bdDevCRM.Repositories.Security;

/// <summary>
/// Provides validation methods to detect and prevent SQL injection attacks
/// </summary>
public static class SqlInjectionValidator
{
	// Compiled regex patterns for better performance
	private static readonly Regex SqlInjectionPattern = new(
		@"(\b(SELECT|INSERT|UPDATE|DELETE|DROP|CREATE|ALTER|EXEC|EXECUTE|UNION|DECLARE|CAST|CONVERT)\b)" +
		@"|(--|;|\/\*|\*\/|xp_|sp_)" +
		@"|(\bOR\b.*=.*|1\s*=\s*1|1\s*=\s*'1')",
		RegexOptions.IgnoreCase | RegexOptions.Compiled
	);

	/// <summary>
	/// Checks if the input string contains SQL injection patterns
	/// </summary>
	/// <param name="input">The string to validate</param>
	/// <returns>True if SQL injection pattern is detected, false otherwise</returns>
	public static bool ContainsSqlInjection(string? input)
	{
		if (string.IsNullOrWhiteSpace(input))
			return false;

		return SqlInjectionPattern.IsMatch(input);
	}

	/// <summary>
	/// Validates ORDER BY clause to ensure it only contains allowed columns
	/// </summary>
	/// <param name="orderBy">The ORDER BY clause to validate</param>
	/// <param name="allowedColumns">Set of allowed column names</param>
	/// <returns>True if ORDER BY is valid, false otherwise</returns>
	public static bool IsValidOrderBy(string? orderBy, HashSet<string> allowedColumns)
	{
		if (string.IsNullOrWhiteSpace(orderBy))
			return true; // Empty is valid

		// Split by comma to handle multiple columns
		var parts = orderBy.Split(',', StringSplitOptions.RemoveEmptyEntries);

		foreach (var part in parts)
		{
			var trimmed = part.Trim();

			// Check for SQL injection patterns in the entire part
			if (ContainsSqlInjection(trimmed))
				return false;

			// Extract column name (first word before ASC/DESC or space)
			var columnName = trimmed.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];

			// Check if column is in allowed list
			if (!allowedColumns.Contains(columnName, StringComparer.OrdinalIgnoreCase))
				return false;

			// Validate direction keyword if present
			var upperTrimmed = trimmed.ToUpperInvariant();
			if (upperTrimmed.Contains(" "))
			{
				var direction = trimmed.Split(' ', StringSplitOptions.RemoveEmptyEntries).LastOrDefault()?.ToUpperInvariant();
				if (direction != null && direction != "ASC" && direction != "DESC")
					return false;
			}
		}

		return true;
	}

	/// <summary>
	/// Gets all property names from a type to use as allowed columns
	/// </summary>
	/// <typeparam name="T">The entity type</typeparam>
	/// <returns>HashSet of property names</returns>
	public static HashSet<string> GetAllowedColumns<T>()
	{
		return typeof(T).GetProperties()
			.Select(p => p.Name)
			.ToHashSet(StringComparer.OrdinalIgnoreCase);
	}

	/// <summary>
	/// Validates a WHERE condition clause
	/// </summary>
	/// <param name="condition">The WHERE condition to validate</param>
	/// <returns>True if condition is safe, false otherwise</returns>
	public static bool IsValidCondition(string? condition)
	{
		if (string.IsNullOrWhiteSpace(condition))
			return true;

		// Check for SQL injection patterns
		return !ContainsSqlInjection(condition);
	}
}
