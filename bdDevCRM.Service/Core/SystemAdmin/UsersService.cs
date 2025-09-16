using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.System;

using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos.Core.HR;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Common;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace bdDevCRM.Services.Core.SystemAdmin;

internal sealed class UsersService : IUsersService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  private const string SELECT_USERS_BY_HRRECORDID = @"Select Users.*,Employment.BRANCHID from Users 
left join Employment on Employment.HRRecordId=Users.EmployeeId where Users.EmployeeId = {0}";

  public UsersService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public IEnumerable<UsersDto> GetUsers(bool trackChanges)
  {
    IEnumerable<Users> users = _repository.Users.GetUsers(trackChanges);
    if (users.Count() == 0) throw new GenericListNotFoundException("Users");

    List<UsersDto> usersDto = MyMapper.JsonCloneIEnumerableToList<Users, UsersDto>(users);
    //IEnumerable<UsersDto> usersDto = users.Select(u => MyMapper.JsonClone<Users, UsersDto>(u)).ToList();
    return usersDto;
  }

  public UsersDto GetUser(int usersId, bool trackChanges)
  {
    var user = _repository.Users.GetUser(usersId, trackChanges);
    //Check if the data is null
    if (user == null) throw new GenericNotFoundException("usersId", "UsersId", usersId.ToString());

    UsersDto usersDto = MyMapper.JsonClone<Users, UsersDto>(user);
    return usersDto;
  }

  public IEnumerable<UsersDto> GetByIds(IEnumerable<int> ids, bool trackChanges)
  {
    if (ids is null) throw new IdParametersBadRequestException();
    var userEntities = _repository.Users.GetByIds(ids, trackChanges);

    if (ids.Count() != userEntities.Count()) throw new CollectionByIdsBadRequestException("Users");

    var usersToReturn = MyMapper.JsonCloneIEnumerableToList<Users, UsersDto>(userEntities);
    return usersToReturn;
  }

  public async Task<IEnumerable<UsersDto>> GetUsersAsync(bool trackChanges)
  {
    IEnumerable<Users> users = await _repository.Users.GetUsersAsync(trackChanges);
    if (users.Count() == 0) throw new GenericListNotFoundException("Users");

    List<UsersDto> usersDto = MyMapper.JsonCloneIEnumerableToList<Users, UsersDto>(users);
    return usersDto;
  }

  public async Task<UsersDto> GetUserAsync(int UsersId, bool trackChanges)
  {
    Users user = await _repository.Users.GetUserAsync(UsersId, trackChanges);
    if (user == null) throw new GenericNotFoundException("Users", "UsersId", UsersId.ToString());

    UsersDto usersDto = MyMapper.JsonClone<Users, UsersDto>(user);
    return usersDto;
  }

  public UsersDto? GetUserByLoginIdAsync(string loginId, bool trackChanges)
  {
    UsersRepositoryDto? user = _repository.Users.GetUserByLoginIdAsync(loginId, trackChanges);
    if (user == null) return null;

    UsersDto usersDto = MyMapper.JsonClone<UsersRepositoryDto, UsersDto>(user);
    return usersDto;
  }

  public void CreateUser(UsersDto model)
  {
    if (model == null) throw new NullModelBadRequestException("Users");

    Users user = MyMapper.JsonClone<UsersDto, Users>(model);
    _repository.Users.CreateUser(user);
    _repository.Save();
  }

  public void UpdateUser(UsersDto model)
  {
    if (model == null) throw new NullModelBadRequestException("Users");

    Users user = MyMapper.JsonClone<UsersDto, Users>(model);
    _repository.Users.UpdateUser(user);
    _repository.Save();
  }

  public void DeleteUser(UsersDto model)
  {
    if (model == null) throw new NullModelBadRequestException("Users");

    Users user = MyMapper.JsonClone<UsersDto, Users>(model);
    _repository.Users.DeleteUser(user);
    _repository.Save();
  }

  public async Task<UsersDto> CreateUserAsync(UsersDto model)
  {
    if (model == null) throw new NullModelBadRequestException("Users");

    Users user = MyMapper.JsonClone<UsersDto, Users>(model);
    _repository.Users.CreateUser(user);
    await _repository.SaveAsync();
    return model;
  }

  public async Task UpdateUserAsync(int userId, UsersDto model, bool trackChanges)
  {
    if (model == null) throw new NullModelBadRequestException("Users");
    if (userId != model.UserId) throw new IdMismatchBadRequestException("Users", "UserId", userId.ToString());

    var user = await _repository.Users.GetUserAsync(userId, trackChanges);
    if (user == null) throw new GenericNotFoundException("Users", "UserId", userId.ToString());
    user = MyMapper.JsonClone<UsersDto, Users>(model);

    // modified state so async don't work.
    _repository.Users.UpdateByState(user);
    await _repository.SaveAsync();
  }

  public async Task DeleteUserAsync(int userId, bool trackChanges)
  {
    if (userId == 0 || userId == null) throw new IdParametersBadRequestException();

    Users user = await _repository.Users.GetUserAsync(userId, trackChanges);
    if (user == null) throw new GenericNotFoundException("Users", "UserId", userId.ToString());

    await _repository.Users.DeleteAsync(x => x.UserId == userId, trackChanges: true);
    await _repository.SaveAsync();
  }

  public async Task<IQueryable<PasswordHistoryDto>> GetPasswordHistory(int userId, int passRestriction)
  {
    IEnumerable<PasswordHistoryRepositoryDto> passwordRepositoryHistory = await _repository.Users.GetPasswordHistory(userId, passRestriction);
    //Check if the result is null
    if (passwordRepositoryHistory == null) throw new GenericNotFoundException("PasswordHistory", "UserId", userId.ToString());
    var passwordHistoryDto = MyMapper.JsonCloneIEnumerableToList<PasswordHistoryRepositoryDto, PasswordHistoryDto>(passwordRepositoryHistory);
    return passwordHistoryDto.AsQueryable();
  }


  // from users settings page
  public async Task<GridEntity<UsersDto>> UsersSummary(int companyId, bool trackChanges, CRMGridOptions options, UsersDto user)
  {
    IEnumerable<GroupsRepositoryDto> objGroups = await _repository.AccessRestrictions.AccessRestrictionGroupsByHrrecordId((int)user.HrRecordId);
    string condition = "";
    var newCondition = "";
    string groupCondition = string.Empty;
    if (objGroups.ToList().Count > 0)
    {
      string groupIds = string.Join(",", objGroups.Select(x => x.GroupId));
      if (!string.IsNullOrEmpty(groupIds)) groupCondition = $" or GroupId in ({groupIds})";
    }

    #region access restriction : no need now.
    //var accessRestrictionRepositoryDto = await _repository.AccessRestrictions.AccessRestrictionByHrRecordId((int)user.HrRecordId, groupCondition);

    //if (accessRestrictionRepositoryDto.Count() > 0)
    //{
    //  IEnumerable<AccessRestrictionRepositoryDto> objAccessRestructionCompany = accessRestrictionRepositoryDto.Where(a => a.ParentReference == 0 && a.ReferenceType == 1).ToList();

    //  if (objAccessRestructionCompany.Count() > 0)
    //  {
    //    foreach (var accessRestrictionEntity in objAccessRestructionCompany)
    //    {
    //      if (newCondition == "")
    //      {
    //        newCondition += string.Format(" (Employment.CompanyId= {0} ", accessRestrictionEntity.ReferenceId);
    //      }
    //      else
    //      {
    //        newCondition += string.Format(" or (Employment.CompanyId={0}", accessRestrictionEntity.ReferenceId);
    //      }

    //      var objAccessRestructionBranch = accessRestrictionRepositoryDto.Where(b => b.ReferenceType == 2 && b.ParentReference == accessRestrictionEntity.ReferenceId).ToList();

    //      #region Branch Count

    //      if (objAccessRestructionBranch.Count > 0)
    //      {
    //        var isFirstConditionForBranch = true;
    //        newCondition += " and (";

    //        foreach (var restrictionEntity in objAccessRestructionBranch)
    //        {
    //          if (isFirstConditionForBranch)
    //          {
    //            newCondition += string.Format(" (Employment.BranchId={0}",
    //                restrictionEntity.ReferenceId);
    //          }
    //          else
    //          {
    //            newCondition += string.Format(" or (Employment.BranchId={0}",
    //                restrictionEntity.ReferenceId);
    //          }
    //          isFirstConditionForBranch = false;

    //          #region Department Count

    //          var objAccessRestructionDep = accessRestrictionRepositoryDto.Where(b =>
    //                      b.ReferenceType == 3 &&
    //                      b.ParentReference == accessRestrictionEntity.ReferenceId &&
    //                      b.ChiledParentReference == restrictionEntity.ReferenceId).ToList();
    //          if (objAccessRestructionDep.Count > 0)
    //          {
    //            newCondition += " and (";
    //            var ids = objAccessRestructionDep.Aggregate("",
    //                (current, entity) =>
    //                    current +
    //                    (current == ""
    //                        ? entity.ReferenceId.ToString()
    //                        : "," + entity.ReferenceId.ToString()));
    //            if (ids != "")
    //            {
    //              newCondition += " Employment.DepartmentId in (" + ids + ")";
    //            }
    //            newCondition += ")";
    //          }

    //          #endregion

    //          newCondition += ")";
    //        }
    //        newCondition += ")";
    //      }

    //      #endregion

    //      newCondition += " )";
    //    }
    //  }

    //  if (user.AccessParentCompany == 1)
    //  {
    //    if (companyId > 0)
    //    {
    //      condition = string.Format(" where Users.CompanyId={0}", companyId);
    //      if (newCondition != "")
    //      {
    //        condition += " and (" + newCondition + ")";
    //      }
    //    }
    //    else
    //    {
    //      if (newCondition != "")
    //      {
    //        condition = " where " + newCondition;
    //      }
    //    }
    //  }
    //  else
    //  {
    //    condition = string.Format(" where Users.CompanyId={0}", companyId == 0 ? companyId : companyId);
    //    if (newCondition != "")
    //    {
    //      condition += " and (" + newCondition + " )";
    //    }
    //  }
    //}

    #endregion access restriction : no need now.


    string query =
      string.Format(@"Select Users.*,Employment.DepartmentId ,BranchId ,Employment.EmployeeId as Employee_Id ,Employee.ShortName ,Department.DepartmentName
--,DESIGNATION.DESIGNATIONNAME 
,Users.EmployeeId as HrRecordId
from Users
inner join Employment on Employment.HrREcordId = Users.EmployeeID 
inner join Employee on Employee.HrRecordId = Employment.HrREcordId
left join Department on Employment.DepartmentId = Department.DepartmentId
--left join DESIGNATION on  Employment.DESIGNATIONID = DESIGNATION.DESIGNATIONID
{0}", condition);
    string orderBy = "UserName asc";
    var gridEntity = await _repository.Users.GridData<UsersDto>(query, options, orderBy, "");

    return gridEntity;
  }

  public async Task<UsersDto> SaveUser(UsersDto usersDto)
  {
    var res = new UsersDto();
    //// Not now. We will work this function letter.
    //UpdateAppUser(usersDto);

    SystemSettings objsystem = await _repository.SystemSettings.GetSystemSettingsDataByCompanyId((int)usersDto.CompanyId);

    if (objsystem == null)
    {
      throw new CommonBadReuqestException("Please First Save System Settings Data");
    }

    var validate = "";
    if (usersDto.UserId == 0)
    {
      validate = ValidateUser(usersDto, objsystem);
    }
    else
    {
      Users userByUserId = await _repository.Users.GetByIdAsync(x => x.UserId == usersDto.UserId, trackChanges: false);
      usersDto.Password = usersDto.Password == "" ? EncryptDecryptHelper.Decrypt(userByUserId.Password) : usersDto.Password;
      validate = ValidateUser(usersDto, objsystem);
    }

    if (validate != "Valid")
    {
      return new UsersDto();
    }

    using var transaction = _repository.Users.TransactionBeginAsync();
    try
    {
      List<GroupMember> groupMembers = new List<GroupMember>();

      #region New User
      if (usersDto.UserId == 0)
      {
        //var objUserNewByLogInId = GetUserByLoginId(usersDto.LoginId);
        Users userByLoginid = await _repository.Users.GetByIdAsync(x => x.LoginId.ToLower().Trim() == usersDto.LoginId.ToLower().Trim(), trackChanges: false);
        if (userByLoginid == null)
        {
          //if (!IsExistsUserByEmployee(usersDto.EmployeeId))
          if (!await _repository.Users.ExistsAsync(x => x.EmployeeId == usersDto.EmployeeId))
          {
            usersDto.CreatedDate = DateTime.Now;
            usersDto.LastUpdatedDate = DateTime.Now;
            usersDto.IsExpired = false;

            var encytpass = EncryptDecryptHelper.Encrypt(usersDto.Password);
            usersDto.Password = encytpass;

            Users objUsers = MyMapper.JsonClone<UsersDto, Users>(usersDto);
            usersDto.UserId = await _repository.Users.CreateAndGetIdAsync(objUsers);

            foreach (var groupMember in usersDto.GroupMembers)
            {
              groupMembers.Add(new GroupMember { GroupId = groupMember.GroupId, UserId = objUsers.UserId });
            }

            if (groupMembers.Count > 0)
            {
              await _repository.GroupMembers.BulkInsertAsync(groupMembers);
            }

            await _repository.GroupMembers.TransactionCommitAsync();

            return usersDto;
          }
          else
          {
            return res;
          }
        }
        else
        {
          return new UsersDto();
        }
      }
      #endregion New User

      #region Update User
      else
      {
        //var objUserNewByLogInId = GetUserByLoginIdAndNotUserId(users.LoginId, users.UserId);
        var objUserNewByLogInId = await _repository.Users.FirstOrDefaultAsync( x=> x.LoginId == usersDto.LoginId && x.UserId != usersDto.UserId);

        if (objUserNewByLogInId == null)
        {
          //var objUserforDb = GetUserById(usersDto.UserId);
          var objUserforDb = await _repository.Users.FirstOrDefaultAsync(x => x.UserId == usersDto.UserId);
          objUserforDb.CompanyId = usersDto.CompanyId;
          objUserforDb.LoginId = usersDto.LoginId;
          var encytpass = EncryptDecryptHelper.Encrypt(usersDto.Password);
          objUserforDb.Password = encytpass;
          objUserforDb.UserName = usersDto.UserName;
          objUserforDb.IsActive = usersDto.IsActive;
          objUserforDb.AccessParentCompany = usersDto.AccessParentCompany;
          objUserforDb.LastUpdatedDate = DateTime.Now;
          objUserforDb.DefaultDashboard = usersDto.DefaultDashboard;

          if (objUserforDb.IsActive == true)
          {
            objUserforDb.FailedLoginNo = 0;
          }

          var lastLoginDate = objUserforDb.LastLoginDate.HasValue && objUserforDb.LastLoginDate.Value != DateTime.MinValue
              ? objUserforDb.LastLoginDate
              : null;

          objUserforDb.LastLoginDate = lastLoginDate;
          _repository.Users.Update(objUserforDb);

          IEnumerable<GroupMember> groupMembersByUserId = await _repository.GroupMembers.GetListByIdsAsync(x => x.UserId == usersDto.UserId);

          if (groupMembersByUserId.Count() > 0)
          {
            _repository.GroupMembers.BulkDelete(groupMembersByUserId);
          }

          foreach (var groupMember in usersDto.GroupMembers)
          {
            groupMembers.Add(new GroupMember { GroupId = groupMember.GroupId, UserId = (int)usersDto.UserId });
          }

          if (groupMembers.Count > 0)
          {
            await _repository.GroupMembers.BulkInsertAsync(groupMembers);
          }
          await _repository.Users.TransactionCommitAsync();

          return usersDto;
        }
        else
        {
          return new UsersDto();
        }
      }
      #endregion Update User
    }
    catch (Exception)
    {
      await _repository.Users.TransactionRollbackAsync();
      throw;
    }
    finally
    {
      await _repository.Users.TransactionDisposeAsync();
    }
  }

  private string ValidateUser(UsersDto users, SystemSettings objsystem)
  {
    string specialChs = @"! ~ @ # $ % ^ & * ( ) _ - + = { } [ ] : ; , . < > ? / | \";
    string[] specialCharacters = specialChs.Split(' ');
    string message = "Valid";
    if (users.LoginId != "")
    {
      if (objsystem.MinLoginLength > users.LoginId.Trim().Length)
      {
        message = "Login ID must have to be minimum " + objsystem.MinLoginLength + " character length!";
        throw new InvalidUpdateOperationException(message);
      }
    }

    if (objsystem.MinPassLength > users.Password.Trim().Length)
    {
      message = "Password must have to be minimum " + objsystem.MinPassLength + " character length!";
      throw new InvalidUpdateOperationException(message);
    }

    if (objsystem.MinLoginLength == 0 && objsystem.MinPassLength == 0 && objsystem.SpecialCharAllowed == false)
      throw new InvalidUpdateOperationException(message);

    int numCount = 0;
    int charCount = 0;
    int specialcharCount = 0;
    char[] pasChars = users.Password.ToCharArray();
    for (int i = 0; i < pasChars.Length; i++)
    {
      if (pasChars[i] == '0' || pasChars[i] == '1' || pasChars[i] == '2' || pasChars[i] == '3' ||
          pasChars[i] == '4' || pasChars[i] == '5' || pasChars[i] == '6' || pasChars[i] == '7' ||
          pasChars[i] == '8' || pasChars[i] == '9')
        numCount++;
      else
      {
        IEnumerable<string> found = specialCharacters.Where(x => x == pasChars[i].ToString());
        if (found.Count() == 0)
          charCount++;
        else
          specialcharCount++;
      }
    }
    //passType 0 = Alpjabetic, 1=Numeric, 2=AlphaNumeric
    if (objsystem.PassType == 0)
    {
      //0 = Alpjabetic

      if (numCount > 0)
      {
        message = "Password must not have any number!";
        throw new InvalidUpdateOperationException(message);
      }

      if (charCount == 0)
      {
        message = "Password must have to be alphabetic characters!";
        throw new InvalidUpdateOperationException(message);
      }
    }
    else if (objsystem.PassType == 1)
    {
      //1=Numeric
      if (numCount == 0)
      {
        message = "Password must have atleast one numeric character!";
        throw new InvalidUpdateOperationException(message);
      }

      if (charCount > 0)
      {
        message = "Password must not have any alphabetic character!";
        throw new InvalidUpdateOperationException(message);
      }
    }
    else
    {
      //2=AlphaNumeric
      if (numCount == 0)
      {
        message = "Password must have atleast one numeric character!";
        throw new InvalidUpdateOperationException(message);
      }

      if (charCount == 0)
      {
        message = "Password must have atleast one alphabetic character!";
        throw new InvalidUpdateOperationException(message);
      }
    }
    if (objsystem.SpecialCharAllowed == true)
    {
      if (specialcharCount == 0)
      {
        message = "Password must have atleast one special character!";
        throw new InvalidUpdateOperationException(message);
      }
    }

    return message;
  }


  // not now when need. Letter we will work this this 
  private void UpdateAppUser(UsersDto usersDto)
  {
    try
    {
      if (usersDto.EmployeeId > 0)
      {
        string sql = string.Format("Update AppUsers set IMEI='{0}' where HrRecordId={1}", usersDto.IMEI, usersDto.EmployeeId);
        _repository.Users.ExecuteNonQuery(sql);
      }
    }
    catch (Exception)
    {
      _logger.LogError(string.Format(@"Error while updating AppUser for IMEI: {0} and HrRecordId: {1}", usersDto.IMEI, usersDto.EmployeeId));
      throw new InvalidUpdateOperationException("Error while updating AppUser");
    }
  }


}
