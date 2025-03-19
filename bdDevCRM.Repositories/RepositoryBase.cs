using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace bdDevCRM.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
  private readonly CRMContext _context;
  private readonly DbSet<T> _dbSet;

  public RepositoryBase(CRMContext context)
  {
    _context = context;
    _dbSet = _context.Set<T>();
  }

  #region Basic CRUD Operations without async

  public void Create(T entity) => _dbSet.Add(entity);

  public void Update(T entity) => _dbSet.Update(entity);

  public void Delete(T entity) => _dbSet.Remove(entity);

  public T GetById(int id) => _dbSet.Find(id);

  public async Task<T> GetByIdWithNotFoundException(int id)
  {
    var entity = await _dbSet.FindAsync(id);
    if (entity == null) throw new GenericNotFoundException(typeof(T).Name, "Id", id.ToString());
    return entity;
  }

  public IEnumerable<T> GetAll() => _dbSet.ToList();

  public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? _dbSet.AsNoTracking() : _dbSet;

  public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    => !trackChanges ? _dbSet.Where(expression).AsNoTracking() : _dbSet.Where(expression);

  public async Task<bool> HasAnyAsync(Expression<Func<T, bool>> predicate)
  {
    return await _dbSet.AnyAsync(predicate);
  }
  #endregion Basic CRUD Operations without async

  #region Basic Crud Operation with async
  public async Task AddAsync(T entity)
  {
    await _dbSet.AddAsync(entity);
  }

  public void UpdateAsync(T entity)
  {
    //_dbSet.Update(entity);
    _dbSet.Attach(entity);
    _context.Entry(entity).State = EntityState.Modified;
    //await _context.SaveChangesAsync();
  }

  public async Task DeleteAsync(int id)
  {
    var entity = await _dbSet.FindAsync(id);
    if (entity != null)
    {
      _dbSet.Remove(entity);
      //await _context.SaveChangesAsync();
    }
  }

  public async Task<T> GetByIdAsync(int id)
  {
    return await _dbSet.FindAsync(id);
  }

  public async Task<IEnumerable<T>> GetAllAsync()
  {
    return await _dbSet.ToListAsync();
  }

  public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression) => await _dbSet.AsNoTracking().Where(expression).ToListAsync();
  #endregion Basic Crud Operation  with async

  #region Advanced Crud Operation
  public async Task<int> CountAsync()
  {
    return await _dbSet.CountAsync();
  }

  public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
  {
    return await _dbSet.AnyAsync(expression);
  }

  public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
  {
    return await _dbSet.FirstOrDefaultAsync(expression);
  }

  public async Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize)
  {
    return await _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
  }
  #endregion Advanced Crud Operation

  #region Data By Generic
  public T FindById(int id) => _context.Set<T>().Find(id) ?? throw new InvalidOperationException($"Entity of type {typeof(T).Name} with id {id} not found.");

  public async Task<IEnumerable<T>> GetListOfResultByQuery(string query) => await _context.Set<T>().FromSqlRaw(query).ToListAsync();

  /// <summary>
  /// Check if data exists for a specific field and value.
  /// </summary>
  public async Task<bool> CheckIfExists(string fieldName, string fieldValue)
  {
    // Validate the input
    if (string.IsNullOrWhiteSpace(fieldName))
      throw new ArgumentException("Field name cannot be null or empty.", nameof(fieldName));

    // Get the property info for the given field name
    var property = typeof(T).GetProperty(fieldName);
    if (property == null)
      throw new ArgumentException($"Field '{fieldName}' does not exist on type '{typeof(T).Name}'.");

    // Convert the string value to the property's type
    var parameter = Expression.Parameter(typeof(T), "x");
    var propertyAccess = Expression.Property(parameter, property);

    // Parse the field value into the appropriate type
    object convertedValue;
    try
    {
      convertedValue = Convert.ChangeType(fieldValue, property.PropertyType);
    }
    catch (Exception)
    {
      throw new ArgumentException($"The value '{fieldValue}' cannot be converted to type '{property.PropertyType.Name}'.");
    }

    // Build the lambda expression: x => x.FieldName == convertedValue
    var equalsExpression = Expression.Equal(propertyAccess, Expression.Constant(convertedValue));
    var lambda = Expression.Lambda<Func<T, bool>>(equalsExpression, parameter);

    // Query the database
    var exists = await _context.Set<T>().AnyAsync(lambda);
    if (!exists) throw new GenericNotFoundException(typeof(T).Name, fieldName, fieldValue);

    return exists;
  }
  #endregion Data By Generic

  #region Data By Query

  // <summary> If the query column and Entities property different then use this method </summary>
  /// Json Approch : Converting dynamic query result to the generic type result
  public async Task<IEnumerable<TResult>> GetListOfDataByQuery<TResult>(string query) where TResult : class, new()
  {
    var connection = _context.Database.GetDbConnection();
    try
    {
      await connection.OpenAsync();

      using (var command = connection.CreateCommand())
      {
        command.CommandText = query;

        using (var result = await command.ExecuteReaderAsync())
        {
          if (result == null) return new List<TResult>();

          var data = new List<TResult>();
          while (await result.ReadAsync())
          {
            var json = await result.GetFieldValueAsync<string>(0);
            var item = JsonConvert.DeserializeObject<TResult>(json);
            if (item != null) data.Add(item);
          }
          return data;
        }
      }
    }
    finally
    {
      await connection.CloseAsync();
    }
  }

  /// <summary> Retrieves a single record from a query when the query column and Entities property differ </summary>
  /// <remarks> Json Approach: Converting dynamic query result to the generic type result </remarks>
  public async Task<TResult?> GetSingleDataByQuery<TResult>(string query) where TResult : class, new()
  {
    var connection = _context.Database.GetDbConnection();
    try
    {
      await connection.OpenAsync();
      using (var command = connection.CreateCommand())
      {
        command.CommandText = query;
        using (var result = await command.ExecuteReaderAsync())
        {
          if (result == null || !await result.ReadAsync())
            return null;

          var json = await result.GetFieldValueAsync<string>(0);
          return JsonConvert.DeserializeObject<TResult>(json);
        }
      }
    }
    finally
    {
      await connection.CloseAsync();
    }
  }

  /// <summary> .Net Approch : Converting dynamic query result to the generic type result </summary>
  public async Task<IEnumerable<TResult>> GetGenericResultByQuery<TResult>(string query) where TResult : class, new()
  {
    var connection = _context.Database.GetDbConnection();
    try
    {
      await connection.OpenAsync();

      using (var command = connection.CreateCommand())
      {
        command.CommandText = query;

        using (var result = await command.ExecuteReaderAsync())
        {
          var data = new List<TResult>();
          var properties = typeof(TResult).GetProperties();

          // Dynamically map only matching columns
          while (await result.ReadAsync())
          {
            var item = new TResult();

            foreach (var prop in properties)
            {
              // Check if the column exists in the result set
              int ordinal;
              try
              {
                ordinal = result.GetOrdinal(prop.Name);
              }
              catch (IndexOutOfRangeException)
              {
                // Skip this property if the column is missing
                continue;
              }

              object value = null;

              // Check if the column value is DBNull
              if (!await result.IsDBNullAsync(ordinal))
              {
                value = await result.GetFieldValueAsync<object>(ordinal);
              }

              // Assign the value to the property
              prop.SetValue(item, value);
            }

            data.Add(item);
          }

          return data;
        }
      }
    }
    finally
    {
      await connection.CloseAsync();
    }
  }

  /// <summary> .Net Approch : Converting dynamic query result to the generic type result </summary>
  public async Task<TResult?> GetSingleGenericResultByQuery<TResult>(string query) where TResult : class, new()
  {
    var connection = _context.Database.GetDbConnection();
    try
    {
      await connection.OpenAsync();
      using (var command = connection.CreateCommand())
      {
        command.CommandText = query;
        using (var result = await command.ExecuteReaderAsync())
        {
          if (!await result.ReadAsync())
            return null;

          // Create an instance of the result type
          var item = new TResult();

          // Get all properties of the result type
          var properties = typeof(TResult).GetProperties();

          // Get the column names from the query result
          var columnNames = Enumerable.Range(0, result.FieldCount)
                                       .Select(result.GetName)
                                       .ToList();

          // Map query result columns to DTO properties
          foreach (var columnName in columnNames)
          {
            // Find a matching property in the DTO (case-insensitive)
            var matchingProperty = properties.FirstOrDefault(p =>
                string.Equals(p.Name, columnName, StringComparison.OrdinalIgnoreCase));

            if (matchingProperty != null)
            {
              // Get the value from the query result
              var value = await result.GetFieldValueAsync<object>(result.GetOrdinal(columnName));

              // Set the value to the DTO property
              matchingProperty.SetValue(item, value);
            }
          }

          return item;
        }
      }
    }
    finally
    {
      await connection.CloseAsync();
    }
  }

  public async Task<List<TResult>> GetListGenericResultByQuery<TResult>(string query) where TResult : class, new()
  {
    var connection = _context.Database.GetDbConnection();
    var resultList = new List<TResult>();

    try
    {
      await connection.OpenAsync();
      using (var command = connection.CreateCommand())
      {
        command.CommandText = query;
        using (var result = await command.ExecuteReaderAsync())
        {
          // Get all properties of the result type
          var properties = typeof(TResult).GetProperties();

          // Get the column names from the query result
          var columnNames = Enumerable.Range(0, result.FieldCount)
                                       .Select(result.GetName)
                                       .ToList();

          // Iterate through all rows in the query result
          while (await result.ReadAsync())
          {
            // Create an instance of the result type
            var item = new TResult();

            // Map query result columns to DTO properties
            foreach (var columnName in columnNames)
            {
              // Find a matching property in the DTO (case-insensitive)
              var matchingProperty = properties.FirstOrDefault(p =>
                  string.Equals(p.Name, columnName, StringComparison.OrdinalIgnoreCase));

              if (matchingProperty != null)
              {
                // Get the value from the query result
                var value = await result.GetFieldValueAsync<object>(result.GetOrdinal(columnName));

                // Set the value to the DTO property
                matchingProperty.SetValue(item, value);
              }
            }

            // Add the populated DTO object to the result list
            resultList.Add(item);
          }
        }
      }
    }
    finally
    {
      await connection.CloseAsync();
    }

    return resultList;
  }


  #endregion Data By Query




  #region Query Execute by TDT
  public string ExecuteNonQuery(string query)
  {
    var res = "";

    try
    {
      _context.Database.SetCommandTimeout(180);
      _context.Database.ExecuteSqlRaw(query);
      res = "Success";
    }
    catch (Exception ex)
    {
      res = ex.Message;
    }
    return res;

  }

  public async Task<IEnumerable<T>?> ExecuteListSql(string query)
  {
    try
    {
      return await _dbSet.FromSqlRaw(query).ToListAsync();
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<T?> ExecuteSingleSql(string query)
  {
    try
    {
      return await _dbSet.FromSqlRaw(query).FirstOrDefaultAsync();
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  public DataTable DataTable(string sqlQuery, params DbParameter[] parameters)
  {
    try
    {
      DataTable dataTable = new DataTable();
      DbConnection connection = _context.Database.GetDbConnection();
      DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connection);
      using (var cmd = dbFactory.CreateCommand())
      {
        cmd.Connection = connection;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sqlQuery;
        if (parameters != null)
        {
          foreach (var item in parameters)
          {
            cmd.Parameters.Add(item);
          }
        }
        using (DbDataAdapter adapter = dbFactory.CreateDataAdapter())
        {
          adapter.SelectCommand = cmd;
          adapter.Fill(dataTable);
        }
      }
      return dataTable;
    }
    catch (Exception ex)
    {
      throw;
    }
  }
  #endregion Query Execute by TDT


  //public async Task<List<T>> ExecuteQueryAsync<T>(string query) where T : new()
  //{
  //  var dataList = new List<T>();
  //  using (var connection = _context.Database.GetDbConnection())
  //  {
  //    try
  //    {
  //      await connection.OpenAsync();
  //      using (var command = connection.CreateCommand())
  //      {
  //        command.CommandText = query;
  //        using (var reader = await command.ExecuteReaderAsync())
  //        {
  //          while (await reader.ReadAsync())
  //          {
  //            var entity = new T();
  //            var properties = typeof(T).GetProperties();

  //            for (int i = 0; i < reader.FieldCount; i++)
  //            {
  //              if (reader.IsDBNull(i)) continue;

  //              var columnName = reader.GetName(i);
  //              var property = properties.FirstOrDefault(p =>
  //                  p.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));

  //              if (property != null && property.CanWrite)
  //              {
  //                var value = reader.GetValue(i);
  //                try
  //                {
  //                  if (property.PropertyType.IsGenericType &&
  //                      property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
  //                  {
  //                    var underlyingType = Nullable.GetUnderlyingType(property.PropertyType);
  //                    value = Convert.ChangeType(value, underlyingType);
  //                  }
  //                  else
  //                  {
  //                    value = Convert.ChangeType(value, property.PropertyType);
  //                  }

  //                  property.SetValue(entity, value);
  //                }
  //                catch (Exception ex)
  //                {
  //                  Console.Error.WriteLine($"Error setting property {property.Name}: {ex.Message}");
  //                }
  //              }
  //            }

  //            dataList.Add(entity);
  //          }
  //        }
  //      }
  //    }
  //    catch (Exception ex)
  //    {
  //      Console.Error.WriteLine($"Database error: {ex.Message}");
  //      Console.Error.WriteLine($"Stack trace: {ex.StackTrace}");
  //      throw; // রি-থ্রো করা গুরুত্বপূর্ণ যাতে এটি কলিং কোডে প্রচার হয়
  //    }
  //  }

  //  return dataList;
  //}



  public async Task<List<T>> ExecuteQueryAsync<T>(string query)
  {
    var connection = _context.Database.GetDbConnection();
    var dataList = new List<T>();

    try
    {
      await connection.OpenAsync();

      using (var command = connection.CreateCommand())
      {
        command.CommandText = query;

        using (var reader = await command.ExecuteReaderAsync())
        {
          if (!reader.HasRows) return dataList;

          while (await reader.ReadAsync())
          {
            var entity = Activator.CreateInstance<T>();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
              if (!reader.HasColumn(property.Name)) continue;

              var value = reader[property.Name];
              if (value == DBNull.Value) continue;

              property.SetValue(entity, Convert.ChangeType(value, property.PropertyType));
            }

            dataList.Add(entity);
          }
        }
      }
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error in ExecuteQueryAsync: {ex.Message}");
    }
    finally
    {
      if (connection.State == ConnectionState.Open)
      {
        await connection.CloseAsync();
      }
    }

    return dataList;
  }


  #region clde
  public async Task<IEnumerable<TResult>> ExecuteOptimizedQuery<TResult>(string query, SqlParameter[] parameters) where TResult : class, new()
  {
    var connection = _context.Database.GetDbConnection();
    var results = new List<TResult>();

    try
    {
      await connection.OpenAsync();

      using var command = connection.CreateCommand();
      command.CommandText = query;
      command.CommandTimeout = 30; // Set timeout to 30 seconds

      if (parameters != null)
      {
        foreach (var param in parameters)
        {
          var dbParam = command.CreateParameter();
          dbParam.ParameterName = param.ParameterName;
          dbParam.Value = param.Value;
          command.Parameters.Add(dbParam);
        }
      }

      using var reader = await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess);

      // Get property map once outside the loop for performance
      var propertyMap = CreatePropertyMap<TResult>(reader);

      while (await reader.ReadAsync())
      {
        results.Add(MapReaderToObject<TResult>(reader, propertyMap));
      }

      return results;
    }
    finally
    {
      if (connection.State == ConnectionState.Open)
        await connection.CloseAsync();
    }
  }

  private Dictionary<string, PropertyInfo> CreatePropertyMap<T>(DbDataReader reader)
  {
    var properties = typeof(T).GetProperties();
    var columnMap = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);

    for (int i = 0; i < reader.FieldCount; i++)
    {
      string columnName = reader.GetName(i);
      var property = properties.FirstOrDefault(p => p.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
      if (property != null)
      {
        columnMap[columnName] = property;
      }
    }

    return columnMap;
  }

  private T MapReaderToObject<T>(DbDataReader reader, Dictionary<string, PropertyInfo> propertyMap) where T : new()
  {
    var item = new T();

    for (int i = 0; i < reader.FieldCount; i++)
    {
      string columnName = reader.GetName(i);

      if (propertyMap.TryGetValue(columnName, out PropertyInfo property))
      {
        if (!reader.IsDBNull(i))
        {
          object value = reader.GetValue(i);
          if (property.PropertyType != value.GetType() && value is IConvertible)
          {
            value = Convert.ChangeType(value, property.PropertyType);
          }
          property.SetValue(item, value);
        }
      }
    }

    return item;
  }
  #endregion
}


public static class DbDataReaderExtensions
{
  public static bool HasColumn(this DbDataReader reader, string columnName)
  {
    for (int i = 0; i < reader.FieldCount; i++)
    {
      if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
      {
        return true;
      }
    }
    return false;
  }
}

