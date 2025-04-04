using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.Sql.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
  private readonly CRMContext _context;
  private readonly DbSet<T> _dbSet;
  private IDbContextTransaction _currentTransaction;

  public RepositoryBase(CRMContext context)
  {
    _context = context;
    _dbSet = _context.Set<T>();
  }

  #region Basic CRUD Operations without async

  public void Create(T entity) => _dbSet.Add(entity);

  public async Task CreateAsync(T entity) => await _dbSet.AddAsync(entity);

  public async Task<int> CreateAndGetIdAsync(T entity)
  {
    await _dbSet.AddAsync(entity);
    await _context.SaveChangesAsync();
    // Get the primary key property
    var keyProperty = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties[0];

    // Return the primary key value
    return (int)keyProperty.GetGetter().GetClrValue(entity);
  }


  public async Task BulkInsertAsync(IEnumerable<T> entities)
  {
    if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities), "Entities list cannot be null or empty.");

    await _dbSet.AddRangeAsync(entities);
  }

  #region Transaction Management Methods

  public async Task TransactionBeginAsync()
  {
    if (_currentTransaction != null)
      throw new InvalidOperationException("A transaction is already in progress.");

    _currentTransaction = await _context.Database.BeginTransactionAsync();
  }

  public async Task TransactionCommitAsync()
  {
    if (_currentTransaction == null)
      throw new InvalidOperationException("No active transaction to commit.");

    try
    {
      await _context.SaveChangesAsync();
      await _currentTransaction.CommitAsync();
    }
    finally
    {
      _currentTransaction.Dispose();
      _currentTransaction = null;
    }
  }

  public async Task TransactionRollbackAsync()
  {
    if (_currentTransaction == null)
      throw new InvalidOperationException("No active transaction to rollback.");

    try
    {
      await _currentTransaction.RollbackAsync();
    }
    finally
    {
      _currentTransaction.Dispose();
      _currentTransaction = null;
    }
  }

  public async Task TransactionDisposeAsync()
  {
    if (_currentTransaction != null)
    {
      try
      {
        await _currentTransaction.RollbackAsync();
      }
      finally
      {
        _currentTransaction.Dispose();
        _currentTransaction = null;
      }
    }
  }

  #endregion transaction end




  public void Update(T entity) => _dbSet.Update(entity);

  public void UpdateByState(T entity)
  {
    _dbSet.Attach(entity);
    _context.Entry(entity).State = EntityState.Modified;
  }

  public void Delete(T entity) => _dbSet.Remove(entity);

  public void BulkDelete(IEnumerable<T> entities)
  {
    if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities), "Entities list cannot be null or empty.");

     _dbSet.RemoveRange(entities);
  }




  public async Task DeleteAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false)
  {
    var enitytData = (trackChanges) ? await _dbSet.Where(predicate).AsNoTracking().FirstOrDefaultAsync() : await _dbSet.Where(predicate).FirstOrDefaultAsync();
    if (enitytData != null)
    {
      _dbSet.Remove(enitytData);
    }
  }

  public T GetById(Expression<Func<T, bool>> predicate, bool trackChanges = false)
    => !trackChanges ? _dbSet.Where(predicate).AsNoTracking().FirstOrDefault() : _dbSet.Where(predicate).FirstOrDefault();

  public async Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false)
    => !trackChanges ? await _dbSet.Where(predicate).AsNoTracking().FirstOrDefaultAsync() : await _dbSet.Where(predicate).FirstOrDefaultAsync();


  public T FirstOrDefault(Expression<Func<T, bool>>? expression = null, bool trackChanges = false)
    => !trackChanges ? _dbSet.AsNoTracking().FirstOrDefault(expression) : _dbSet.FirstOrDefault(expression);
  public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool trackChanges = false)
    => !trackChanges ? await _dbSet.AsNoTracking().FirstOrDefaultAsync(expression) : await _dbSet.FirstOrDefaultAsync(expression);


  public IEnumerable<T> GetListByIds(Expression<Func<T, bool>> expression, bool trackChanges = false)
  {
    IQueryable<T> query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
    query = query.Where(expression);
    return query.ToList();
  }

  public async Task<IEnumerable<T>> GetListByIdsAsync(Expression<Func<T, bool>> expression, bool trackChanges = false)
  {
    IQueryable<T> query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
    query = query.Where(expression);
    return await query.ToListAsync();
  }




  public IEnumerable<T> List(Expression<Func<T, object>>? orderBy = null, bool trackChanges = false)
  {
    IQueryable<T> query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
    if (orderBy != null)
    {
      query = query.OrderBy(orderBy);
    }
    return query.ToList();
  }

  public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, object>>? orderBy = null, bool trackChanges = false)
  {
    IQueryable<T> query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
    if (orderBy != null)
    {
      query = query.OrderBy(orderBy);
    }
    return await query.ToListAsync();
  }
  
  public IEnumerable<T> ListByCondition(Expression<Func<T, bool>> expression, Expression<Func<T, object>>? orderBy = null, bool trackChanges = false)
  {

    IQueryable<T> query = !trackChanges ? _dbSet.AsNoTracking() : _dbSet;
    query = (orderBy != null) ? query.Where(expression).OrderBy(orderBy) : query.Where(expression);
    return query.ToList(); ;
  }

  public async Task<IEnumerable<T>> ListByConditionAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>>? orderBy = null, bool trackChanges = false)
  {

    IQueryable<T> query = !trackChanges ? _dbSet.AsNoTracking() : _dbSet;
    query = (orderBy != null) ? query.Where(expression).OrderBy(orderBy) : query.Where(expression);
    return await query.ToListAsync(); ;
  }


  public async Task<int> CountAsync() => await _dbSet.AsNoTracking().CountAsync();

  public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression) => await _dbSet.AsNoTracking().AnyAsync(expression);
  #endregion Advanced Crud Operation


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


  #region Get Data using ado.net


  public async Task<GridEntity<T>> GridData<T>(string query, CRMGridOptions options, string orderBy, string condition)
  {
    var connection = _context.Database.GetDbConnection();
    var sqlCount = "SELECT COUNT(*) FROM (" + query + " ) As tbl ";
    query = CRMGridDataSource<T>.DataSourceQuery(options, query, orderBy, "");
    var dataList = new List<T>();
    int totalCount = 0;
    try
    {
      await connection.OpenAsync();

      using (var countCommand = connection.CreateCommand())
      {
        countCommand.CommandText = sqlCount;
        totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync());
      }

      using (var command = connection.CreateCommand())
      {
        command.CommandText = query;
        using (var reader = await command.ExecuteReaderAsync())
        {
          if (!reader.HasRows) return new GridEntity<T> { Items = dataList, TotalCount = 0 };

          var columnMap = new Dictionary<string, int>();
          for (int i = 0; i < reader.FieldCount; i++)
          {
            columnMap[reader.GetName(i)] = i;
          }

          var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

          while (await reader.ReadAsync())
          {
            var entity = Activator.CreateInstance<T>();

            foreach (var property in properties)
            {
              if (!columnMap.ContainsKey(property.Name)) continue;

              var columnIndex = columnMap[property.Name];
              if (reader.IsDBNull(columnIndex)) continue;

              var value = reader.GetValue(columnIndex);

              try
              {
                Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                if (propertyType == typeof(Guid) && value is string)
                {
                  property.SetValue(entity, Guid.Parse((string)value));
                }
                else if (propertyType.IsEnum && value is string)
                {
                  property.SetValue(entity, Enum.Parse(propertyType, (string)value));
                }
                else
                {
                  property.SetValue(entity, Convert.ChangeType(value, propertyType));
                }
              }
              catch (Exception ex)
              {
                Console.Error.WriteLine($"Error converting value '{value}' to type {property.PropertyType.Name} for property {property.Name}: {ex.Message}");
              }
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

    var dbEntity = new GridEntity<T>();
    dbEntity.Items = dataList ?? new List<T>();
    dbEntity.TotalCount = totalCount;
    dbEntity.Columnses = new List<GridColumns>();

    return dbEntity;
  }

  public async Task<TResult> ExecuteSingleData<TResult>(string query, SqlParameter[] parameters = null) where TResult : class, new()
  {
    var connection = _context.Database.GetDbConnection();
    TResult result = null;

    try
    {
      await connection.OpenAsync();

      using var command = connection.CreateCommand();
      command.CommandText = query;
      command.CommandTimeout = 120; // Set timeout to 120 seconds

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

      using var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow);

      if (await reader.ReadAsync())
      {
        // Get property map for performance
        var propertyMap = CreatePropertyMap<TResult>(reader);

        // Map the single record to an object
        result = MapReaderToObject<TResult>(reader, propertyMap);
      }
    }
    finally
    {
      if (connection.State == ConnectionState.Open)
        await connection.CloseAsync();
    }

    return result;
  }


  public TResult ExecuteSingleDataSyncronous<TResult>(string query, SqlParameter[] parameters = null) where TResult : class, new()
  {
    var connection = _context.Database.GetDbConnection();
    TResult result = null;

    try
    {
      connection.Open();

      using (var command = connection.CreateCommand())
      {
        command.CommandText = query;
        command.CommandTimeout = 120; // Set timeout to 120 seconds

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

        using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
        {
          if (!reader.HasRows) return null;

          var columnMap = new Dictionary<string, int>();
          for (int i = 0; i < reader.FieldCount; i++)
          {
            columnMap[reader.GetName(i)] = i;
          }

          var properties = typeof(TResult).GetProperties(BindingFlags.Public | BindingFlags.Instance);

          if (reader.Read())
          {
            var entity = Activator.CreateInstance<TResult>();

            foreach (var property in properties)
            {
              if (!columnMap.ContainsKey(property.Name)) continue;

              var columnIndex = columnMap[property.Name];
              if (reader.IsDBNull(columnIndex)) continue;

              var value = reader.GetValue(columnIndex);

              try
              {
                Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                if (propertyType == typeof(Guid) && value is string)
                {
                  property.SetValue(entity, Guid.Parse((string)value));
                }
                else if (propertyType.IsEnum && value is string)
                {
                  property.SetValue(entity, Enum.Parse(propertyType, (string)value));
                }
                else
                {
                  property.SetValue(entity, Convert.ChangeType(value, propertyType));
                }
              }
              catch (Exception ex)
              {
                Console.Error.WriteLine($"Error converting value '{value}' to type {property.PropertyType.Name} for property {property.Name}: {ex.Message}");
              }
            }

            result = entity;
          }
        }
      }
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error in ExecuteSingleDataSyncronous: {ex.Message}");
    }
    finally
    {
      if (connection.State == ConnectionState.Open)
      {
        connection.Close();
      }
    }

    return result;
  }


  public async Task<IEnumerable<TResult>> ExecuteListQuery<TResult>(string query, SqlParameter[] parameters = null) where TResult : class, new()
  {
    var connection = _context.Database.GetDbConnection();
    var results = new List<TResult>();

    try
    {
      await connection.OpenAsync();

      using var command = connection.CreateCommand();
      command.CommandText = query;
      command.CommandTimeout = 120; // Set timeout to 30 seconds

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


  // Helper methods for mapping data reader to object
  // These methods are generic and can be used in any repository
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

  private T MapReaderToObject<T>(DbDataReader reader, Dictionary<string, PropertyInfo> propertyMap) where T : class, new()
  {
    var obj = new T();
    foreach (var entry in propertyMap)
    {
      string columnName = entry.Key;
      PropertyInfo property = entry.Value;

      if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
      {
        object value = reader[columnName];

        // Handle type conversion explicitly
        if (property.PropertyType == typeof(bool?) && value is bool)
        {
          property.SetValue(obj, (bool?)value); // Directly cast bool to bool?
        }
        else if (property.PropertyType == typeof(int?) && value is int)
        {
          property.SetValue(obj, (int?)value);
        }
        else if (property.PropertyType == typeof(string))
        {
          property.SetValue(obj, value?.ToString());
        }
        else
        {
          // Use default type conversion for other types
          property.SetValue(obj, Convert.ChangeType(value, property.PropertyType));
        }
      }
      else
      {
        // Handle NULL values explicitly
        if (property.PropertyType == typeof(bool?))
        {
          property.SetValue(obj, null); // Set nullable bool to null
        }
        else if (property.PropertyType == typeof(int?))
        {
          property.SetValue(obj, null); // Set nullable int to null
        }
      }
    }

    return obj;
  }
  #endregion Get Data using ado.net

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

