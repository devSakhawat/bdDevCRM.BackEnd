using bdDevCRM.Utilities.CRMGrid.GRID;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;

namespace bdDevCRM.RepositoriesContracts;

/// <summary>
/// Enterprise-level Generic Repository Interface
/// Defines comprehensive data access contract with both synchronous and asynchronous methods
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IRepositoryBaseRefactored<T> where T : class
{
    #region Create Operations

    /// <summary>
    /// Adds an entity to the context (synchronous - no I/O)
    /// </summary>
    void Create(T entity);

    /// <summary>
    /// Adds an entity to the context asynchronously
    /// </summary>
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates an entity and returns its ID (synchronous)
    /// </summary>
    int CreateAndGetId(T entity);

    /// <summary>
    /// Creates an entity and returns its ID asynchronously
    /// </summary>
    Task<int> CreateAndGetIdAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds multiple entities to the context (synchronous)
    /// </summary>
    void BulkInsert(IEnumerable<T> entities);

    /// <summary>
    /// Adds multiple entities to the context asynchronously
    /// </summary>
    Task BulkInsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    #endregion

    #region Update Operations

    /// <summary>
    /// Marks an entity as modified
    /// </summary>
    void Update(T entity);

    /// <summary>
    /// Attaches an entity and marks it as modified
    /// </summary>
    void UpdateByState(T entity);

    #endregion

    #region Delete Operations

    /// <summary>
    /// Removes an entity from the context
    /// </summary>
    void Delete(T entity);

    /// <summary>
    /// Deletes an entity matching the predicate (synchronous)
    /// </summary>
    void DeleteByPredicate(Expression<Func<T, bool>> predicate, bool trackChanges = false);

    /// <summary>
    /// Deletes an entity matching the predicate asynchronously
    /// </summary>
    Task DeleteAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a DELETE statement directly against the database without loading entities into memory.
    /// This bypasses the change tracker and is more efficient for bulk deletes (EF Core 7.0+).
    /// </summary>
    Task<int> ExecuteDeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes multiple entities from the context (synchronous)
    /// </summary>
    void BulkDelete(IEnumerable<T> entities);

    /// <summary>
    /// Removes multiple entities from the context asynchronously
    /// </summary>
    Task BulkDeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    #endregion

    #region Read Operations - Single Entity

    /// <summary>
    /// Gets a single entity by predicate (synchronous)
    /// </summary>
    T? GetById(Expression<Func<T, bool>> predicate, bool trackChanges = false);

    /// <summary>
    /// Gets a single entity by predicate asynchronously
    /// </summary>
    Task<T?> GetByIdAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the first entity matching the expression (synchronous)
    /// </summary>
    T? FirstOrDefault(Expression<Func<T, bool>>? expression = null, bool trackChanges = false);

    /// <summary>
    /// Gets the first entity matching the expression asynchronously
    /// </summary>
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool trackChanges = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the first entity matching the expression with descending order (synchronous)
    /// </summary>
    T? FirstOrDefaultWithOrderByDesc(Expression<Func<T, bool>> expression, Expression<Func<T, object>>? orderBy = null, bool trackChanges = false);

    /// <summary>
    /// Gets the first entity matching the expression with descending order asynchronously
    /// </summary>
    Task<T?> FirstOrDefaultWithOrderByDescAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>>? orderBy = null, bool trackChanges = false, CancellationToken cancellationToken = default);

    #endregion

    #region Read Operations - Multiple Entities

    /// <summary>
    /// Gets entities matching the expression (synchronous)
    /// </summary>
    IEnumerable<T> GetListByIds(Expression<Func<T, bool>> expression, bool trackChanges = false);

    /// <summary>
    /// Gets entities matching the expression asynchronously
    /// </summary>
    Task<IEnumerable<T>> GetListByIdsAsync(Expression<Func<T, bool>> expression, bool trackChanges = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all entities with optional ordering (synchronous)
    /// </summary>
    IEnumerable<T> List(Expression<Func<T, object>>? orderBy = null, bool trackChanges = false);

    /// <summary>
    /// Gets all entities with optional ordering asynchronously
    /// </summary>
    Task<IEnumerable<T>> ListAsync(Expression<Func<T, object>>? orderBy = null, bool trackChanges = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets entities matching condition with optional ordering (synchronous)
    /// </summary>
    IEnumerable<T> ListByCondition(Expression<Func<T, bool>> expression, Expression<Func<T, object>>? orderBy = null, bool trackChanges = false);

    /// <summary>
    /// Gets entities matching condition with optional ordering asynchronously
    /// </summary>
    Task<IEnumerable<T>> ListByConditionAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>>? orderBy = null, bool trackChanges = false, bool descending = false, CancellationToken cancellationToken = default);

    #endregion

    #region Projection Operations

    /// <summary>
    /// Gets entities with projection and optional ordering (synchronous)
    /// </summary>
    IEnumerable<TResult> ListWithSelect<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, object>>? orderBy = null, bool trackChanges = false);

    /// <summary>
    /// Gets entities with projection and optional ordering asynchronously
    /// </summary>
    Task<IEnumerable<TResult>> ListWithSelectAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, object>>? orderBy = null, bool trackChanges = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets entities with filter, projection, and optional ordering (synchronous)
    /// </summary>
    IEnumerable<TResult> ListByWhereWithSelect<TResult>(Expression<Func<T, bool>>? expression = null, Expression<Func<T, TResult>>? selector = null, Expression<Func<T, object>>? orderBy = null, bool trackChanges = false);

    /// <summary>
    /// Gets entities with filter, projection, and optional ordering asynchronously
    /// </summary>
    Task<IEnumerable<TResult>> ListByWhereWithSelectAsync<TResult>(Expression<Func<T, TResult>>? selector = null, Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderBy = null, bool trackChanges = false, CancellationToken cancellationToken = default);

    #endregion

    #region Count and Exists Operations

    /// <summary>
    /// Gets the count of all entities (synchronous)
    /// </summary>
    int Count();

    /// <summary>
    /// Gets the count of entities matching the expression (synchronous)
    /// </summary>
    int Count(Expression<Func<T, bool>> expression);

    /// <summary>
    /// Gets the count of all entities asynchronously
    /// </summary>
    Task<int> CountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the count of entities matching the expression asynchronously
    /// </summary>
    Task<int> CountAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if any entity matching the expression exists (synchronous)
    /// </summary>
    bool Exists(Expression<Func<T, bool>> expression);

    /// <summary>
    /// Checks if any entity matching the expression exists asynchronously
    /// </summary>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

    #endregion

    #region Transaction Management

    /// <summary>
    /// Begins a new database transaction (synchronous)
    /// </summary>
    void TransactionBegin();

    /// <summary>
    /// Begins a new database transaction asynchronously
    /// </summary>
    Task TransactionBeginAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits the current transaction (synchronous)
    /// </summary>
    void TransactionCommit();

    /// <summary>
    /// Commits the current transaction asynchronously
    /// </summary>
    Task TransactionCommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rolls back the current transaction (synchronous)
    /// </summary>
    void TransactionRollback();

    /// <summary>
    /// Rolls back the current transaction asynchronously
    /// </summary>
    Task TransactionRollbackAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Disposes the current transaction if exists asynchronously
    /// </summary>
    Task TransactionDisposeAsync();

    #endregion

    #region Change Tracker Management

    /// <summary>
    /// Clears all tracked entities from the ChangeTracker
    /// </summary>
    void ClearChangeTracker();

    /// <summary>
    /// Clears all tracked entities from the ChangeTracker asynchronously
    /// </summary>
    Task ClearChangeTrackerAsync();

    #endregion

    #region Raw SQL Execution

    /// <summary>
    /// Executes a raw SQL command (synchronous)
    /// </summary>
    string ExecuteNonQuery(string query);

    /// <summary>
    /// Executes a raw SQL command asynchronously
    /// </summary>
    Task<string> ExecuteNonQueryAsync(string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a raw SQL query and returns a list of entities
    /// </summary>
    Task<IEnumerable<T>?> ExecuteListSql(string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a raw SQL query and returns a single entity
    /// </summary>
    Task<T?> ExecuteSingleSql(string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a SQL query and returns results as a DataTable
    /// </summary>
    DataTable DataTable(string sqlQuery, params DbParameter[] parameters);

    #endregion

    #region ADO.NET Query Execution

    /// <summary>
    /// Executes a query and returns grid data with pagination
    /// </summary>
    Task<GridEntity<TGrid>> GridData<TGrid>(string query, CRMGridOptions options, string orderBy, string condition, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a query and returns a single result
    /// </summary>
    Task<TResult?> ExecuteSingleData<TResult>(string query, SqlParameter[]? parameters = null, CancellationToken cancellationToken = default) where TResult : class, new();

    /// <summary>
    /// Executes a query and returns a single result (synchronous)
    /// </summary>
    TResult? ExecuteSingleDataSyncronous<TResult>(string query, SqlParameter[]? parameters = null) where TResult : class, new();

    /// <summary>
    /// Executes a query and returns a list of results
    /// </summary>
    Task<IEnumerable<TResult>> ExecuteListQuery<TResult>(string query, SqlParameter[]? parameters = null, CancellationToken cancellationToken = default) where TResult : class, new();

    #endregion
}
