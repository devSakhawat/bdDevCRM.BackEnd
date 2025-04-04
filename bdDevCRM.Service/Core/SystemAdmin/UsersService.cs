using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.OthersLibrary;
using System.Threading.Tasks.Dataflow;

namespace bdDevCRM.Services.Core.SystemAdmin;


internal sealed class UsersService : IUsersService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;

  public UsersService(IRepositoryManager repository, ILoggerManager logger)
  {
    _repository = repository;
    _logger = logger;
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
    UsersRepositoryDto user = _repository.Users.GetUserByLoginIdAsync(loginId, trackChanges);
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

    await _repository.Users.DeleteAsync(x => x.UserId == userId, trackChanges:true);
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




  public async Task<GridEntity<UsersDto>> UsersSummary(bool trackChanges, CRMGridOptions options, int hrrecordId)
  {
    IEnumerable<GroupsRepositoryDto> accessGroupsRepositoryDto = await _repository.AccessRestriction.AccessRestrictionGroupsByHrrecordId(hrrecordId);
    var groupCondition = string.Empty;
    if (accessGroupsRepositoryDto.ToList().Count > 0)
    {
      string groupIds = string.Join(",", accessGroupsRepositoryDto.Select(x => x.GroupId));
      if (!string.IsNullOrEmpty(groupIds))
      {
        groupCondition = $" or GroupId in ({groupIds})";
      }
    }

    string query = "Select * from Groups";
    string orderBy = "GroupId asc";
    var gridEntity = await _repository.Users.GridData<UsersDto>(query, options, orderBy, "");

    return gridEntity;
  }


}
