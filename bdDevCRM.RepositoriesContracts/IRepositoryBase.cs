using bdDevCRM.Entities.CRMGrid.GRID;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;

namespace bdDevCRM.RepositoriesContracts;



public interface IRepositoryBase<T>
{
  #region Basic CRUD Operations
  void Create(T entity);
  Task CreateAsync(T entity);
  Task<int> CreateAndGetIdAsync(T entity);
  Task BulkInsertAsync(IEnumerable<T> entities);

  #region Transaction Management Methods
  Task TransactionBeginAsync();
  Task TransactionCommitAsync();
  Task TransactionRollbackAsync();
  Task TransactionDisposeAsync();
  #endregion Transaction Management Methods

  void Update(T entity);
  void UpdateByState(T entity);

  void Delete(T entity);
  Task DeleteAsync(Expression<Func<T, bool>> predicate, bool trackChanges);

  void BulkDelete(IEnumerable<T> entities);


  T GetById(Expression<Func<T, bool>> predicate, bool trackChanges = false);
  Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false);
  T FirstOrDefault(Expression<Func<T, bool>>? expression = null, bool trackChanges = false);
  Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool trackChanges = false);

  IEnumerable<T> GetListByIds(Expression<Func<T, bool>> expression, bool trackChanges = false);
  Task<IEnumerable<T>> GetListByIdsAsync(Expression<Func<T, bool>> expression, bool trackChanges = false);

  IEnumerable<T> List(Expression<Func<T, object>>? orderBy = null, bool trackChanges = false);
  Task<IEnumerable<T>> ListAsync(Expression<Func<T, object>>? orderBy = null, bool trackChanges = false);

  IEnumerable<T> ListByCondition(Expression<Func<T, bool>> expression, Expression<Func<T, object>>? orderBy = null, bool trackChanges = false);
  Task<IEnumerable<T>> ListByConditionAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>>? orderBy = null, bool trackChanges = false, bool descending = false);


  IEnumerable<T> ListWithSelect<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, object>>? orderBy = null, bool trackChanges = false);
  Task<IEnumerable<TResult>> ListWithSelectAsync<TResult>(
    Expression<Func<T, TResult>> selector,
    Expression<Func<T, object>>? orderBy = null,
    bool trackChanges = false);


  IEnumerable<TResult> ListByWhereWithSelect<TResult>(
     Expression<Func<T, bool>>? expression = null,
     Expression<Func<T, TResult>>? selector = null,
     Expression<Func<T, object>>? orderBy = null,
     bool trackChanges = false);

  Task<IEnumerable<TResult>> ListByWhereWithSelectAsync<TResult>(
     Expression<Func<T, TResult>>? selector = null,
     Expression<Func<T, bool>>? expression = null,
     Expression<Func<T, object>>? orderBy = null,
     bool trackChanges = false);


  Task<int> CountAsync();
  Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
  #endregion  Basic CRUD Operations

  #region  Query Execute by TDT
  string ExecuteNonQuery(string query);
  Task<IEnumerable<T>?> ExecuteListSql(string query);
  Task<T?> ExecuteSingleSql(string query);

  DataTable DataTable(string sqlQuery, params DbParameter[] parameters);
  #endregion  Query Execute by TDT



  #region Get Data using ado.net
  Task<GridEntity<T>> GridData<T>(string query, CRMGridOptions options, string orderBy, string condition);
  Task<TResult> ExecuteSingleData<TResult>(string query, SqlParameter[] parameters = null) where TResult : class, new();
  TResult ExecuteSingleDataSyncronous<TResult>(string query, SqlParameter[] parameters = null) where TResult : class, new();
  Task<IEnumerable<TResult>> ExecuteListQuery<TResult>(string query, SqlParameter[] parameters = null) where TResult : class, new();
  #endregion Get Data using ado.net

}
