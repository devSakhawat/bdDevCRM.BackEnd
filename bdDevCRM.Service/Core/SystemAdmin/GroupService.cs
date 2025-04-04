using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace bdDevCRM.Services.Core.SystemAdmin;


internal sealed class GroupService : IGroupService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public GroupService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<GroupDto> CreateAsync(GroupDto modelDto)
  {
    if (modelDto == null) throw new NullModelBadRequestException(new MenuDto().GetType().Name.ToString());
    bool isModuleExists = await _repository.Groups.ExistsAsync(m => m.GroupName == modelDto.GroupName);
    if (isModuleExists) throw new DuplicateRecordException("Group", "GroupName");

    if (modelDto.IsDefault == 1)
    {
      string updateIsDefaultZero = string.Format("Update Groups set IsDefault = 0");
      string res = _repository.Groups.ExecuteNonQuery(updateIsDefaultZero);
      if (res != "Success") throw new Exception("Error in update IsDefault");
    }

    try
    {
      using var transaction = _repository.GroupPermission.TransactionBeginAsync();
      
      Groups entity = MyMapper.JsonClone<GroupDto, Groups>(modelDto);
      int groupId = await _repository.Groups.CreateAndGetIdAsync(entity);

      // insert modulesPermission to GroupPermission table with currently inserted GroupId
      if (groupId > 0 && modelDto.ModuleList.Count > 0)
      {
        IEnumerable<GroupPermission> modulesPermission = MyMapper.JsonCloneListToIEnumerable<GroupPermissionDto, GroupPermission>(modelDto.ModuleList);
        foreach (var item in modulesPermission) item.Groupid = groupId;
        await _repository.GroupPermission.BulkInsertAsync(modulesPermission);
      }

      // insert menusPermission to GroupPermission table with currently inserted GroupId
      if (groupId > 0 && modelDto.MenuList.Count > 0)
      {
        IEnumerable<GroupPermission> menusPermission = MyMapper.JsonCloneListToIEnumerable<GroupPermissionDto, GroupPermission>(modelDto.MenuList);
        foreach (var item in menusPermission) item.Groupid = groupId;
        await _repository.GroupPermission.BulkInsertAsync(menusPermission);
      }

      // insert accessesPermission to GroupPermission table with currently inserted GroupId
      if (groupId > 0 && modelDto.AccessList.Count > 0)
      {
        IEnumerable<GroupPermission> accessesPermission = MyMapper.JsonCloneListToIEnumerable<GroupPermissionDto, GroupPermission>(modelDto.AccessList);
        foreach (var item in accessesPermission) item.Groupid = groupId;
        await _repository.GroupPermission.BulkInsertAsync(accessesPermission);
      }

      // insert statusPermission to GroupPermission table with currently inserted GroupId
      if (groupId > 0 && modelDto.StatusList.Count > 0)
      {
        IEnumerable<GroupPermission> statusPermission = MyMapper.JsonCloneListToIEnumerable<GroupPermissionDto, GroupPermission>(modelDto.StatusList);
        foreach (var item in statusPermission) item.Groupid = groupId;
        await _repository.GroupPermission.BulkInsertAsync(statusPermission);
      }

      // insert actionsPermission to GroupPermission table with currently inserted GroupId
      if (groupId > 0 && modelDto.ActionList.Count > 0)
      {
        IEnumerable<GroupPermission> actionsPermission = MyMapper.JsonCloneListToIEnumerable<GroupPermissionDto, GroupPermission>(modelDto.ActionList);
        foreach (var item in actionsPermission) item.Groupid = groupId;
        await _repository.GroupPermission.BulkInsertAsync(actionsPermission);
      }

      // insert reportListPermission to GroupPermission table with currently inserted GroupId
      if (groupId > 0 && modelDto.ActionList.Count > 0)
      {
        IEnumerable<GroupPermission> reportListPermission = MyMapper.JsonCloneListToIEnumerable<GroupPermissionDto, GroupPermission>(modelDto.ReportList);
        foreach (var item in reportListPermission) item.Groupid = groupId;
        await _repository.GroupPermission.BulkInsertAsync(reportListPermission);
      }

      await _repository.GroupPermission.TransactionCommitAsync();
      //await _repository.SaveAsync();
      return modelDto;

    }
    catch (Exception)
    {
      await _repository.GroupPermission.TransactionRollbackAsync();
      throw;
    }
    finally
    {
      await _repository.GroupPermission.TransactionDisposeAsync();
    }
  }


  public async Task<GroupDto> UpdateAsync(int key, GroupDto modelDto)
  {
    if (modelDto == null) throw new NullModelBadRequestException(new GroupDto().GetType().Name.ToString());
    if (key != modelDto.GroupId) throw new IdMismatchBadRequestException(key.ToString(), new GroupDto().GetType().Name.ToString());
    bool isModuleExists = await _repository.Groups.ExistsAsync(m => m.GroupName == modelDto.GroupName);
    //if (isModuleExists) throw new DuplicateRecordException("Group", "GroupName");

    if (modelDto.IsDefault == 1)
    {
      string updateIsDefaultZero = string.Format("Update Groups set IsDefault = 0");
      string res = _repository.Groups.ExecuteNonQuery(updateIsDefaultZero);
      if (res != "Success") throw new Exception("Error in update IsDefault");
    }

    try
    {
      using var transaction = _repository.GroupPermission.TransactionBeginAsync();

      if (!isModuleExists)
      {
        Groups entity = await _repository.Groups.GetByIdAsync(m => m.GroupId == modelDto.GroupId, trackChanges: false);
        entity = MyMapper.JsonClone<GroupDto, Groups>(modelDto);
        _repository.Groups.UpdateByState(entity);
      }

      // delete all permissions of this group using ef core not sql query
      IEnumerable<GroupPermission> groupPermissions = await _repository.GroupPermission.ListByConditionAsync(x => x.Groupid.Equals(modelDto.GroupId));
      if(groupPermissions.Count() > 0)
      {
        _repository.GroupPermission.BulkDelete(groupPermissions);
      }


      // insert modulesPermission to GroupPermission table with currently inserted GroupId
      if (key > 0 && modelDto.ModuleList.Count > 0)
      {
        IEnumerable<GroupPermission> modulesPermission = MyMapper.JsonCloneListToIEnumerable<GroupPermissionDto, GroupPermission>(modelDto.ModuleList);
        foreach (var item in modulesPermission) item.Groupid = key;
        await _repository.GroupPermission.BulkInsertAsync(modulesPermission);
      }

      // insert menusPermission to GroupPermission table with currently inserted GroupId
      if (key > 0 && modelDto.MenuList.Count > 0)
      {
        IEnumerable<GroupPermission> menusPermission = MyMapper.JsonCloneListToIEnumerable<GroupPermissionDto, GroupPermission>(modelDto.MenuList);
        foreach (var item in menusPermission) item.Groupid = key;
        await _repository.GroupPermission.BulkInsertAsync(menusPermission);
      }

      // insert accessesPermission to GroupPermission table with currently inserted GroupId
      if (key > 0 && modelDto.AccessList.Count > 0)
      {
        IEnumerable<GroupPermission> accessesPermission = MyMapper.JsonCloneListToIEnumerable<GroupPermissionDto, GroupPermission>(modelDto.AccessList);
        foreach (var item in accessesPermission) item.Groupid = key;
        await _repository.GroupPermission.BulkInsertAsync(accessesPermission);
      }

      // insert statusPermission to GroupPermission table with currently inserted GroupId
      if (key > 0 && modelDto.StatusList.Count > 0)
      {
        IEnumerable<GroupPermission> statusPermission = MyMapper.JsonCloneListToIEnumerable<GroupPermissionDto, GroupPermission>(modelDto.StatusList);
        foreach (var item in statusPermission) item.Groupid = key;
        await _repository.GroupPermission.BulkInsertAsync(statusPermission);
      }

      // insert actionsPermission to GroupPermission table with currently inserted GroupId
      if (key > 0 && modelDto.ActionList.Count > 0)
      {
        IEnumerable<GroupPermission> actionsPermission = MyMapper.JsonCloneListToIEnumerable<GroupPermissionDto, GroupPermission>(modelDto.ActionList);
        foreach (var item in actionsPermission) item.Groupid = key;
        await _repository.GroupPermission.BulkInsertAsync(actionsPermission);
      }

      // insert reportListPermission to GroupPermission table with currently inserted GroupId
      if (key > 0 && modelDto.ReportList.Count > 0)
      {
        IEnumerable<GroupPermission> reportListPermission = MyMapper.JsonCloneListToIEnumerable<GroupPermissionDto, GroupPermission>(modelDto.ReportList);
        foreach (var item in reportListPermission) item.Groupid = key;
        await _repository.GroupPermission.BulkInsertAsync(reportListPermission);
      }

      await _repository.GroupPermission.TransactionCommitAsync();
      return modelDto;

    }
    catch (Exception)
    {
      await _repository.GroupPermission.TransactionRollbackAsync();
      throw;
    }
    finally
    {
      await _repository.GroupPermission.TransactionDisposeAsync();
    }
  }

  /// <summary>
  /// Menu crud
  /// </summary>
  /// <param name="trackChanges"></param>
  /// <param name="options"></param>
  /// <returns></returns>
  public async Task<GridEntity<GroupSummaryDto>> GroupSummary(bool trackChanges, CRMGridOptions options)
  {
    string query = "Select * from Groups";
    string orderBy = "GroupId asc";
    var gridEntity = await _repository.Groups.GridData<GroupSummaryDto>(query, options, orderBy, "");

    return gridEntity;
  }

  public async Task<IEnumerable<GroupPermissionDto>> GroupPermisionsbyGroupId(int groupId)
  {
    if (groupId == null) throw new IdParametersBadRequestException();
    IEnumerable<GroupPermissionRepositoryDto> groupPermissions = await _repository.Groups.GroupPermisionsbyGroupId(groupId);
    IEnumerable<GroupPermissionDto> groupPermissionsDto = MyMapper.JsonCloneIEnumerableToList<GroupPermissionRepositoryDto, GroupPermissionDto>(groupPermissions);

    return groupPermissionsDto;
  }

  public async Task<IEnumerable<AccessControlDto>> GetAccesses()
  {
    IEnumerable<AccessControlRepositoryDto> groupPermissions = await _repository.Groups.GetAccesses();
    IEnumerable<AccessControlDto> groupPermissionsDto = MyMapper.JsonCloneIEnumerableToList<AccessControlRepositoryDto, AccessControlDto>(groupPermissions);
    return groupPermissionsDto;
  }




}
