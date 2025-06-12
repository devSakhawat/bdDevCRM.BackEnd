using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.Sql.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

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

  public async Task<IEnumerable<T>> ListByConditionAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>>? orderBy = null, bool trackChanges = false, bool descending = false)
  {

    IQueryable<T> query = !trackChanges ? _dbSet.AsNoTracking() : _dbSet;
    query = query.Where(expression);
    //query = (orderBy != null) ? query.Where(expression).OrderBy(orderBy) : query.Where(expression);
    if (orderBy != null)
    {
      query = descending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
    }
    return await query.ToListAsync(); ;
  }

  public IEnumerable<T> ListWithSelect<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, object>>? orderBy = null, bool trackChanges = false)
  {
    IQueryable<T> query = !trackChanges ? _dbSet.AsNoTracking() : _dbSet;

    if (orderBy != null)
    {
      query = query.OrderBy(orderBy);
    }

    return (IEnumerable<T>)query.Select(selector).ToList();
  }

  public async Task<IEnumerable<TResult>> ListWithSelectAsync<TResult>(
    Expression<Func<T, TResult>> selector,
    Expression<Func<T, object>>? orderBy = null,
    bool trackChanges = false)
  {
    IQueryable<T> query = !trackChanges ? _dbSet.AsNoTracking() : _dbSet;

    if (orderBy != null)
    {
      query = query.OrderBy(orderBy);
    }

    return await query.Select(selector).ToListAsync();
  }


  public IEnumerable<TResult> ListByWhereWithSelect<TResult>(
     Expression<Func<T, bool>>? expression = null,
     Expression<Func<T, TResult>>? selector = null,
     Expression<Func<T, object>>? orderBy = null,
     bool trackChanges = false)
  {
    IQueryable<T> query = !trackChanges ? _dbSet.AsNoTracking() : _dbSet;

    if (expression != null)
    {
      query = query.Where(expression);
    }

    if (orderBy != null)
    {
      query = query.OrderBy(orderBy);
    }

    if (selector != null)
    {
      return query.Select(selector).ToList();
    }
    else
    {
      // Check: T must be assignable to TResult
      if (typeof(TResult) != typeof(T))
      {
        throw new InvalidOperationException("Selector is null but TResult is not same as T.");
      }

      // Safe cast, since TResult == T
      return query.Cast<TResult>().ToList();
    }
  }

  public async Task<IEnumerable<TResult>> ListByWhereWithSelectAsync<TResult>(
    Expression<Func<T, TResult>>? selector = null,
    Expression<Func<T, bool>>? expression = null,
    Expression<Func<T, object>>? orderBy = null,
    bool trackChanges = false)
  {
    IQueryable<T> query = !trackChanges ? _dbSet.AsNoTracking() : _dbSet;

    if (expression is not null)
    {
      query = query.Where(expression);
    }

    if (orderBy is not null)
    {
      query = query.OrderBy(orderBy);
    }

    if (selector is not null)
    {
      return await query.Select(selector).ToListAsync();
    }
    else
    {
      if (typeof(TResult) != typeof(T))
      {
        throw new InvalidOperationException("Selector is null but TResult is not same as T.");
      }

      // Since TResult == T, safe to cast
      return (IEnumerable<TResult>)await query.Cast<T>().ToListAsync();
    }
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

  public async Task<T> ExecuteSingleSql(string query)
  {
    try
    {
      var result = await _dbSet.FromSqlRaw(query).FirstOrDefaultAsync();

      if (result == null)
      {
        return Activator.CreateInstance<T>()!;
      }
      return result;
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


  #region Grid execution old mechanism
  //public async Task<GridEntity<T>> GridData<T>(string query, CRMGridOptions options, string orderBy, string condition)
  //{
  //  var connection = _context.Database.GetDbConnection();
  //  var sqlCount = "SELECT COUNT(*) FROM (" + query + " ) As tbl ";
  //  query = CRMGridDataSource<T>.DataSourceQuery(options, query, orderBy, "");
  //  var dataList = new List<T>();
  //  int totalCount = 0;
  //  try
  //  {
  //    await connection.OpenAsync();

  //    using (var countCommand = connection.CreateCommand())
  //    {
  //      countCommand.CommandText = sqlCount;
  //      totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync());
  //    }

  //    using (var command = connection.CreateCommand())
  //    {
  //      command.CommandText = query;
  //      using (var reader = await command.ExecuteReaderAsync())
  //      {
  //        if (!reader.HasRows) return new GridEntity<T> { Items = dataList, TotalCount = 0 };

  //        var columnMap = new Dictionary<string, int>();
  //        for (int i = 0; i < reader.FieldCount; i++)
  //        {
  //          columnMap[reader.GetName(i)] = i;
  //        }

  //        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

  //        while (await reader.ReadAsync())
  //        {
  //          var entity = Activator.CreateInstance<T>();

  //          foreach (var property in properties)
  //          {
  //            if (!columnMap.ContainsKey(property.Name)) continue;

  //            var columnIndex = columnMap[property.Name];
  //            if (reader.IsDBNull(columnIndex)) continue;

  //            var value = reader.GetValue(columnIndex);

  //            try
  //            {
  //              Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

  //              if (propertyType == typeof(Guid) && value is string)
  //              {
  //                property.SetValue(entity, Guid.Parse((string)value));
  //              }
  //              else if (propertyType.IsEnum && value is string)
  //              {
  //                property.SetValue(entity, Enum.Parse(propertyType, (string)value));
  //              }
  //              else
  //              {
  //                property.SetValue(entity, Convert.ChangeType(value, propertyType));
  //              }
  //            }
  //            catch (Exception ex)
  //            {
  //              Console.Error.WriteLine($"Error converting value '{value}' to type {property.PropertyType.Name} for property {property.Name}: {ex.Message}");
  //            }
  //          }

  //          dataList.Add(entity);
  //        }
  //      }
  //    }
  //  }
  //  catch (Exception ex)
  //  {
  //    Console.Error.WriteLine($"Error in ExecuteQueryAsync: {ex.Message}");
  //  }
  //  finally
  //  {
  //    if (connection.State == ConnectionState.Open)
  //    {
  //      await connection.CloseAsync();
  //    }
  //  }

  //  var dbEntity = new GridEntity<T>();
  //  dbEntity.Items = dataList ?? new List<T>();
  //  dbEntity.TotalCount = totalCount;
  //  dbEntity.Columnses = new List<GridColumns>();

  //  return dbEntity;
  //}
  #endregion Grid execution old mechanism

  #region grid with duplicate column name and insensative column and property name
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
      // Total Count Query
      using (var countCommand = connection.CreateCommand())
      {
        countCommand.CommandText = sqlCount;
        totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync());
      }
      // Main Data Query
      using (var command = connection.CreateCommand())
      {
        command.CommandText = query;
        using (var reader = await command.ExecuteReaderAsync())
        {
          if (!reader.HasRows) return new GridEntity<T> { Items = dataList, TotalCount = 0 };

          // Create a case-insensitive dictionary for column mapping
          var columnMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

          // Check for duplicate column names and report them
          var duplicateColumns = new List<string>();
          var columnNames = new List<string>();

          for (int i = 0; i < reader.FieldCount; i++)
          {
            var columnName = reader.GetName(i);
            columnNames.Add(columnName);

            // Store in lowercase for case-insensitive comparison
            if (columnMap.ContainsKey(columnName))
            {
              duplicateColumns.Add(columnName);
            }
            columnMap[columnName] = i;
          }

          // Report duplicate columns if any found
          if (duplicateColumns.Any())
          {
            throw new InvalidOperationException($"WARNING: Query returned duplicate column names: {string.Join(", ", duplicateColumns)}. This may cause mapping issues.");
          }

          var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
          while (await reader.ReadAsync())
          {
            var entity = Activator.CreateInstance<T>();
            foreach (var property in properties)
            {
              // Case-insensitive property matching
              if (!columnMap.ContainsKey(property.Name))
              {
                // Try additional checks for name variations
                var propertySnakeCase = ToSnakeCase(property.Name);
                var propertyCamelCase = ToCamelCase(property.Name);

                if (columnMap.ContainsKey(propertySnakeCase))
                {
                  ProcessProperty(reader, entity, property, columnMap[propertySnakeCase]);
                }
                else if (columnMap.ContainsKey(propertyCamelCase))
                {
                  ProcessProperty(reader, entity, property, columnMap[propertyCamelCase]);
                }
                // Skip properties that don't match any column
                continue;
              }

              var columnIndex = columnMap[property.Name];
              ProcessProperty(reader, entity, property, columnIndex);
            }
            dataList.Add(entity);
          }

          // Log unmapped properties for debugging
          var propertyNames = properties.Select(p => p.Name).ToList();
          var unmappedColumns = columnNames.Where(c => !propertyNames.Contains(c, StringComparer.OrdinalIgnoreCase)).ToList();
          
          // add log message
          //if (unmappedColumns.Any())
          //{
            
          //  throw new InvalidOperationException($"WARNING: Some columns were not mapped to properties: {string.Join(", ", unmappedColumns)}");
          //}
        }
      }
    }
    catch (Exception ex)
    {
      throw new InvalidOperationException($"Error in ExecuteQueryAsync: {ex.Message}");
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

  // Update Version with auto generated columns
  public async Task<GridEntity<T>> GridDataUpdated<T>(string query, CRMGridOptions options, string orderBy, string condition)
  {
    var connection = _context.Database.GetDbConnection();
    var sqlCount = "SELECT COUNT(*) FROM (" + query + " ) AS tbl";
    query = CRMGridDataSource<T>.DataSourceQuery(options, query, orderBy, condition);

    var dataList = new List<T>();
    var gridColumns = new List<GridColumns>();
    int totalCount = 0;

    try
    {
      await connection.OpenAsync();

      // Total Count Query
      using (var countCommand = connection.CreateCommand())
      {
        countCommand.CommandText = sqlCount;
        totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync());
      }

      // Main Data Query
      using (var command = connection.CreateCommand())
      {
        command.CommandText = query;
        using (var reader = await command.ExecuteReaderAsync())
        {
          if (!reader.HasRows)
            return new GridEntity<T> { Items = dataList, TotalCount = 0, Columnses = gridColumns };

          // Mapping column index
          var columnMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
          var duplicateColumns = new List<string>();
          var columnNames = new List<string>();

          for (int i = 0; i < reader.FieldCount; i++)
          {
            var columnName = reader.GetName(i);
            columnNames.Add(columnName);

            if (columnMap.ContainsKey(columnName))
              duplicateColumns.Add(columnName);

            columnMap[columnName] = i;
          }

          if (duplicateColumns.Any())
          {
            throw new InvalidOperationException($"WARNING: Query returned duplicate column names: {string.Join(", ", duplicateColumns)}.");
          }

          // Generate dynamic GridColumns based on query columns
          foreach (var columnName in columnNames)
          {
            gridColumns.Add(new GridColumns
            {
              field = columnName,
              title = columnName,
              width = "150px", // Default width, can adjust if needed
              filterable = true,
              sortable = true,
              hidden = false
            });
          }

          // Map Data
          var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
          while (await reader.ReadAsync())
          {
            var entity = Activator.CreateInstance<T>();
            foreach (var property in properties)
            {
              if (!columnMap.ContainsKey(property.Name))
              {
                var propertySnakeCase = ToSnakeCase(property.Name);
                var propertyCamelCase = ToCamelCase(property.Name);

                if (columnMap.ContainsKey(propertySnakeCase))
                  ProcessProperty(reader, entity, property, columnMap[propertySnakeCase]);
                else if (columnMap.ContainsKey(propertyCamelCase))
                  ProcessProperty(reader, entity, property, columnMap[propertyCamelCase]);

                continue;
              }

              var columnIndex = columnMap[property.Name];
              ProcessProperty(reader, entity, property, columnIndex);
            }
            dataList.Add(entity);
          }
        }
      }
    }
    catch (Exception ex)
    {
      throw new InvalidOperationException($"Error in GridData: {ex.Message}");
    }
    finally
    {
      if (connection.State == ConnectionState.Open)
        await connection.CloseAsync();
    }

    return new GridEntity<T>
    {
      Items = dataList,
      TotalCount = totalCount,
      Columnses = gridColumns
    };
  }



  // Helper method to process a property
  private void ProcessProperty<T>(DbDataReader reader, T entity, PropertyInfo property, int columnIndex)
  {
    if (reader.IsDBNull(columnIndex)) return;

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

  // Converts PascalCase to snake_case
  private string ToSnakeCase(string name)
  {
    if (string.IsNullOrEmpty(name)) return name;

    var result = new StringBuilder();
    result.Append(char.ToLowerInvariant(name[0]));

    for (int i = 1; i < name.Length; i++)
    {
      if (char.IsUpper(name[i]))
      {
        result.Append('_');
        result.Append(char.ToLowerInvariant(name[i]));
      }
      else
      {
        result.Append(name[i]);
      }
    }

    return result.ToString();
  }

  // Converts PascalCase to camelCase
  private string ToCamelCase(string name)
  {
    if (string.IsNullOrEmpty(name)) return name;
    return char.ToLowerInvariant(name[0]) + name.Substring(1);
  }
  #endregion grid with duplicate column name and insensative column and property name




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
        command.CommandTimeout = 120;

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

          // Create case-insensitive column mapping
          var columnMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
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
              // Try to find column by exact name or case-insensitive match
              var columnFound = columnMap.TryGetValue(property.Name, out int columnIndex);
              if (!columnFound)
              {
                // Try alternate common cases (e.g., HRRecordId -> HrRecordId)
                var alternateNames = new[]
                {
                  property.Name,
                  property.Name.ToUpper(),
                  property.Name.ToLower(),
                  string.Concat(property.Name[0].ToString().ToUpper(), property.Name.Substring(1))
                };

                foreach (var alternateName in alternateNames)
                {
                  if (columnMap.TryGetValue(alternateName, out columnIndex))
                  {
                    columnFound = true;
                    break;
                  }
                }
              }

              if (!columnFound) continue;
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
        // Add DateOnly? conversion
        else if (property.PropertyType == typeof(DateOnly?) && value is DateTime dateTime)
        {
          property.SetValue(obj, DateOnly.FromDateTime(dateTime));
        }
        // Add non-nullable DateOnly conversion if needed
        else if (property.PropertyType == typeof(DateOnly) && value is DateTime dateTime2)
        {
          property.SetValue(obj, DateOnly.FromDateTime(dateTime2));
        }
        else
        {
          try
          {
            // Use default type conversion for other types
            property.SetValue(obj, Convert.ChangeType(value, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType));
          }
          catch (InvalidCastException)
          {
            // Log error or handle specific type conversion failures
            // You might want to add more robust error handling here
          }
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


  #region DMS Module

  #endregion DMS Module
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

