using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;

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
        if (modelDto == null) throw new NullModelBadRequestException(new GroupDto().GetType().Name);

        bool isGroupExists = await _repository.Groups.ExistsAsync(m => m.GroupName == modelDto.GroupName);
        if (isGroupExists) throw new DuplicateRecordException("Group", "GroupName");

        if (modelDto.IsDefault == 1)
        {
            string updateIsDefaultZero = "UPDATE Groups SET IsDefault = 0";
            string res = _repository.Groups.ExecuteNonQuery(updateIsDefaultZero);
            if (res != "Success") throw new Exception("Error in update IsDefault");
        }

        try
        {
            await _repository.GroupPermissiones.TransactionBeginAsync();

            Groups entity = MyMapper.JsonClone<GroupDto, Groups>(modelDto);
            int groupId = await _repository.Groups.CreateAndGetIdAsync(entity);

            // Safety check: Delete any existing permissions for this newly created group
            await DeleteExistingPermissionsForGroup(groupId);

            // Insert all permission types using helper method
            await InsertPermissionsIfNotEmpty(groupId, modelDto.ModuleList);
            await InsertPermissionsIfNotEmpty(groupId, modelDto.MenuList);
            await InsertPermissionsIfNotEmpty(groupId, modelDto.AccessList);
            await InsertPermissionsIfNotEmpty(groupId, modelDto.StatusList);
            await InsertPermissionsIfNotEmpty(groupId, modelDto.ActionList);
            await InsertPermissionsIfNotEmpty(groupId, modelDto.ReportList);

            await _repository.GroupPermissiones.TransactionCommitAsync();
            return modelDto;
        }
        catch (Exception)
        {
            await _repository.GroupPermissiones.TransactionRollbackAsync();
            throw;
        }
        finally
        {
            await _repository.GroupPermissiones.TransactionDisposeAsync();
        }
    }


    public async Task<GroupDto> UpdateAsync(int key, GroupDto modelDto)
    {
        if (modelDto == null) throw new NullModelBadRequestException(new GroupDto().GetType().Name);
        if (key != modelDto.GroupId) throw new IdMismatchBadRequestException(key.ToString(), new GroupDto().GetType().Name);

        bool isGroupExists = await _repository.Groups.ExistsAsync(m => m.GroupId != key);
        // Uncomment if you want to check for duplicate group names during update
        // if (isGroupExists) throw new DuplicateRecordException("Group", "GroupName");

        if (modelDto.IsDefault == 1)
        {
            string updateIsDefaultZero = "UPDATE Groups SET IsDefault = 0";
            string res = _repository.Groups.ExecuteNonQuery(updateIsDefaultZero);
            if (res != "Success") throw new Exception("Error in update IsDefault");
        }

        try
        {
            await _repository.GroupPermissiones.TransactionBeginAsync();

            // Update group entity if name doesn't exist for other groups
            if (!isGroupExists)
            {
                Groups entity = await _repository.Groups.GetByIdAsync(m => m.GroupId == modelDto.GroupId, trackChanges: false);
                entity = MyMapper.JsonClone<GroupDto, Groups>(modelDto);
                _repository.Groups.UpdateByState(entity);
            }

            // Delete all existing permissions before inserting new ones
            await DeleteExistingPermissionsForGroup(modelDto.GroupId);

            // Insert all permission types using helper method
            await InsertPermissionsIfNotEmpty(key, modelDto.ModuleList);
            await InsertPermissionsIfNotEmpty(key, modelDto.MenuList);
            await InsertPermissionsIfNotEmpty(key, modelDto.AccessList);
            await InsertPermissionsIfNotEmpty(key, modelDto.StatusList);
            await InsertPermissionsIfNotEmpty(key, modelDto.ActionList);
            await InsertPermissionsIfNotEmpty(key, modelDto.ReportList);

            // Commit transaction (this saves all changes)
            await _repository.GroupPermissiones.TransactionCommitAsync();
            return modelDto;
        }
        catch (Exception)
        {
            await _repository.GroupPermissiones.TransactionRollbackAsync();
            throw;
        }
        finally
        {
            await _repository.GroupPermissiones.TransactionDisposeAsync();
        }
    }

    // Helper method to avoid code duplication
    private async Task InsertPermissionsIfNotEmpty(int groupId, List<GroupPermissionDto>? permissionList)
    {
        if (groupId > 0 && permissionList != null && permissionList.Count > 0)
        {
            IEnumerable<GroupPermission> permissions = MyMapper.JsonCloneIEnumerableToIEnumerable<GroupPermissionDto, GroupPermission>(permissionList);

            // Remove duplicates based on Referenceid and Parentpermission
            var uniquePermissions = permissions
              .GroupBy(x => new { x.Referenceid, x.Parentpermission })
              .Select(g => g.First())
              .ToList();

            // Set GroupId for all permissions
            foreach (var item in uniquePermissions)
            {
                item.Groupid = groupId;
            }

            await _repository.GroupPermissiones.BulkInsertAsync(uniquePermissions);
        }
    }

    // Helper method to delete existing permissions
    private async Task DeleteExistingPermissionsForGroup(int groupId)
    {
        IEnumerable<GroupPermission> existingPermissions = await _repository.GroupPermissiones
          .ListByConditionAsync(x => x.Groupid == groupId);

        if (existingPermissions.Any())
        {
            _repository.GroupPermissiones.BulkDelete(existingPermissions);
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

    public async Task<IEnumerable<GroupPermissionDto>> GetAccessPermisionForCurrentUser(int moduleId, int userId)
    {
        IEnumerable<GroupPermissionRepositoryDto> groupMemeberRepositoriesDto = await _repository.GroupPermissiones.GetAccessPermisionForCurrentUser(moduleId, userId);
        if (!groupMemeberRepositoriesDto.Any())
            groupMemeberRepositoriesDto = new List<GroupPermissionRepositoryDto> { new GroupPermissionRepositoryDto() };

        IEnumerable<GroupPermissionDto> groupMemeberDtos = MyMapper.JsonCloneIEnumerableToList<GroupPermissionRepositoryDto, GroupPermissionDto>(groupMemeberRepositoriesDto);
        return groupMemeberDtos;
    }

    // from user settings
    public async Task<IEnumerable<GroupForUserSettings>> GetGroups(bool trackChanges)
    {
        IEnumerable<Groups> groups =
          await _repository.Groups.ListWithSelectAsync(selector: g => new Groups { GroupId = g.GroupId, GroupName = g.GroupName }, trackChanges: false);

        IEnumerable<GroupForUserSettings> groupForUserSettings = MyMapper.JsonCloneIEnumerableToList<Groups, GroupForUserSettings>(groups);
        return groupForUserSettings;
    }

    public async Task<IEnumerable<GroupForUserSettings>> GetGroupsByUserId(int userId, bool trackChanges)
    {
        // get user to check IsSystemUser flag
        Users? objUser = await _repository.Users.GetUserAsync(userId, trackChanges: false);

        IEnumerable<Groups> groups;

        // If system user -> return all groups (no joins)
        if (objUser != null && objUser.IsSystemUser == true)
        {
            groups = await _repository.Groups.ListWithSelectAsync(
              selector: g => new Groups { GroupId = g.GroupId, GroupName = g.GroupName },
              trackChanges: false);
        }
        else
        {
            // normal user -> get groups by GroupMember relation
            IEnumerable<GroupMember> groupMembers =
              await _repository.GroupMembers.ListByConditionAsync(x => x.UserId == userId, trackChanges: false);

            var groupIds = groupMembers?.Select(gm => gm.GroupId).Distinct().ToList();

            if (groupIds == null || !groupIds.Any())
                return Enumerable.Empty<GroupForUserSettings>();

            groups = await _repository.Groups.ListByConditionAsync(g => groupIds.Contains(g.GroupId), trackChanges: false);
        }

        IEnumerable<GroupForUserSettings> groupForUserSettings = MyMapper.JsonCloneIEnumerableToList<Groups, GroupForUserSettings>(groups);
        return groupForUserSettings;
    }

    public async Task<IEnumerable<GroupMemberDto>> GroupMemberByUserId(int userId, bool trackChanges)
    {
        // ListByConditionAsync must be call by only enitiy name not dto. 
        IEnumerable<GroupMember> groupMembers = await _repository.GroupMembers.ListByConditionAsync(expression: x => x.UserId == userId, trackChanges: false);
        IEnumerable<GroupMemberDto> resultData = MyMapper.JsonCloneIEnumerableToList<GroupMember, GroupMemberDto>(groupMembers);
        return resultData;
    }

    //public async Task<IEnumerable<GroupDto>> GroupsByModuleId(int moduleId, bool trackChanges)
    //{
    //  IEnumerable<GroupDto> groupsByModule = await _repository.Groups.ListByConditionAsync(x => x.mod, trackChanges:false);
    //}

    // (For MenuManagement Only) Check menu permission by path and user
    public async Task<MenuDto> CheckMenuPermission(string rawPath, UsersDto objUser)
    {
        if (string.IsNullOrWhiteSpace(rawPath)) throw new GenericBadRequestException("Invalid URL.");
        Users userEntity = MyMapper.JsonClone<UsersDto, Users>(objUser);
        MenuRepositoryDto data = await _repository.Groups.CheckMenuPermission(rawPath, userEntity);
        if (data == null)
            return new MenuDto();
        else
            return MyMapper.JsonClone<MenuRepositoryDto, MenuDto>(data);
    }

}
