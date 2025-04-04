using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.EntityFrameworkCore;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class UsersRepository : RepositoryBase<Users>, IUsersRepository
{
  public UsersRepository(CRMContext context) : base(context) { }

  private const string UPDATE_USER =
            "Update Users set CompanyID = {0}, LoginId='{1}', UserName='{2}', Password='{3}',EmployeeId={4},CreatedDate={5},LastUpdateDate={6},LastLoginDate={7},FailedLoginNo={8},IsActive={9},IsExpired={10},AccessParentCompany={11} where UserId={12}";
  private const string SAVE_PASSWORD_HISTORY =
            "insert into PasswordHistory (USERID, OLDPASSWORD, PASSWORDCHANGEDATE) values ({0}, '{1}', '{2}')";

  private const string SELECT_USERS_BY_LOGINID_SQL =
           "Select Users.UserId, Users.CompanyID, Users.LoginId, Users.UserName, Users.Password, Users.EmployeeId, Users.CreatedDate, Users.LastUpdateDate, Users.LastLoginDate, Users.FailedLoginNo, Users.IsActive, Users.IsExpired, Users.THEME  ,Users.AccessParentCompany ,Users.DefaultDashboard ,Employee.PROFILEPICTURE as ProfilePicture \r\nfrom Users \r\ninner join Employee on Users.EmployeeId = Employee.HRRecordId\r\nwhere rtrim(ltrim(Lower(LoginId))) = '{0}'";


  public IEnumerable<Users> GetUsers(bool trackChanges) => List(u => u.UserId ,trackChanges);

  public Users GetUser(int UsersId, bool trackChanges)
  {
    return FirstOrDefault(c => c.UserId.Equals(UsersId), trackChanges);
  }

  public IEnumerable<Users> GetByIds(IEnumerable<int> ids, bool trackChanges) 
    => GetListByIds(x => ids.Contains(x.UserId), trackChanges);

  // Get all Users
  public async Task<IEnumerable<Users>> GetUsersAsync(bool trackChanges) => await ListAsync(c => c.UserId ,trackChanges);

  // Get a single Users by ID
  public async Task<Users> GetUserAsync(int usersId, bool trackChanges) => await FirstOrDefaultAsync(c => c.UserId.Equals(usersId), trackChanges);


  // Get a single Users by LoginId
  public UsersRepositoryDto? GetUserByLoginIdAsync(string loginId, bool trackChanges) 
  {
    string quary = string.Format(SELECT_USERS_BY_LOGINID_SQL, loginId);
    UsersRepositoryDto userRepositoryDto = ExecuteSingleDataSyncronous<UsersRepositoryDto>(quary);

    return userRepositoryDto;
  }

  // Get a single Users by LoginId
  public Users? GetUserByLoginId(string loginId, bool trackChanges) => FirstOrDefault(c => c.LoginId.Trim().Equals(loginId.Trim()), trackChanges);

  // Add a new Users
  public void CreateUser(Users Users) => Create(Users);

  // Update an existing Users
  public void UpdateUser(Users Users) => UpdateByState(Users);

  // Delete a Users by Object
  public void DeleteUser(Users Users) => Delete(Users);

  ////////////////
  public async Task<IQueryable<PasswordHistoryRepositoryDto>> GetPasswordHistory(int userId, int passRestriction)
  {
    //var passwordHistory = new List<PasswordHistoryRepositoryDto>();
    var quary = string.Format(@"SELECT TOP {1} [HistoryId],[UserId],[OldPassword],[PasswordChangeDate] FROM [dbo].[PasswordHistory] WHERE [UserId] = {0} ORDER BY [PasswordChangeDate] DESC", userId, passRestriction);

    IEnumerable<PasswordHistoryRepositoryDto> passwordHistory = await ExecuteListQuery<PasswordHistoryRepositoryDto>(quary);

    return passwordHistory.AsQueryable();
  }

  public string UpdateUser(Users users, PasswordHistory passwordHistory)
  {
    string res = string.Empty;
    UpdateUser(users);

    if (passwordHistory != null)
    {
      passwordHistory.PasswordChangeDate = DateTime.Now;
      
    }
    if (passwordHistory != null)
    {
      var passHisQuary = string.Format(SAVE_PASSWORD_HISTORY, passwordHistory.UserId, passwordHistory.OldPassword, DateTime.Now);
      res = ExecuteNonQuery(passHisQuary);
    }
    return res;
  }
}