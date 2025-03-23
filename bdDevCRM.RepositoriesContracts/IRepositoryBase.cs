using System.Data.Common;
using System.Data;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using bdDevCRM.Entities.CRMGrid;
using bdDevCRM.Entities.CRMGrid.GRID;

namespace bdDevCRM.RepositoriesContracts;



public interface IRepositoryBase<T>
{
  #region Basic CRUD Operations without async
  // Generic Crud Operation
  void Create(T entity);

  void Update(T entity);

  void Delete(T entity);

  T GetById(int id);

  Task<T> GetByIdWithNotFoundException(int id);

  IEnumerable<T> GetAll();

  IQueryable<T> FindAll(bool trackChanges);

  IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
  #endregion Basic CRUD Operations without async

  #region Basic Generic Crud
  // Generic Crud Operation
  Task CreateAsync(T entity);
  void UpdateAsync(T entity);
  Task DeleteAsync(Expression<Func<T, bool>> predicate, bool trackChanges);

  Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate, bool trackChanges);
  Task<IEnumerable<T>> GetAllAsync();

  Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
  #endregion Basic Generic Crud

  #region Advanced Crud Operation
  Task<int> CountAsync();
  Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
  Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
  Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);
  #endregion Advanced Crud Operation

  #region Data By Generic
  public T FindById(int id);
  Task<IEnumerable<T>> GetListOfResultByQuery(string query);
  Task<bool> CheckIfExists(string fieldName, string fieldValue);
  #endregion Data By Generic

  #region Data By Query
  /// <summary>
  /// Retrieves multiple records from a query when the query column and Entities property differ
  /// </summary>
  /// <typeparam name="TResult">The type to deserialize the results to</typeparam>
  /// <param name="query">The SQL query to execute</param>
  /// <returns>A collection of the specified type</returns>
  Task<IEnumerable<TResult>> GetListOfDataByQuery<TResult>(string query) where TResult : class, new();

  /// <summary>
  /// Retrieves a single record from a query when the query column and Entities property differ
  /// </summary>
  /// <typeparam name="TResult">The type to deserialize the result to</typeparam>
  /// <param name="query">The SQL query to execute</param>
  /// <returns>A single object of the specified type or null if not found</returns>
  Task<TResult?> GetSingleDataByQuery<TResult>(string query) where TResult : class, new();


  Task<IEnumerable<TResult>> GetGenericResultByQuery<TResult>(string query) where TResult : class, new();

  Task<TResult?> GetSingleGenericResultByQuery<TResult>(string query) where TResult : class, new();
  Task<List<TResult>> GetListGenericResultByQuery<TResult>(string query) where TResult : class, new();

  Task<bool> HasAnyAsync(Expression<Func<T, bool>> predicate);
  #endregion  Data By Query

  #region  Query Execute by TDT
  string ExecuteNonQuery(string query);
  Task<IEnumerable<T>?> ExecuteListSql(string query);
  Task<T?> ExecuteSingleSql(string query);
  DataTable DataTable(string sqlQuery, params DbParameter[] parameters);
  #endregion  Query Execute by TDT


  Task<GridEntity<T>> GridData<T>(string query, CRMGridOptions options, string orderBy, string condition);
  Task<List<T>> ExecuteQueryAsync<T>(string query);

  #region clde
  Task<IEnumerable<TResult>> ExecuteOptimizedQuery<TResult>(string query, SqlParameter[] parameters) where TResult : class, new();
  #endregion clde

}
