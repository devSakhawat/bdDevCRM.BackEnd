using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class GroupRepository : RepositoryBase<Groups>, IGroupsRepository
{
  public GroupRepository(CRMContext context) : base(context) { }

  public async Task<List<GroupsRepositoryDto>> GroupSummary(bool trackChanges)
  {
    string menuSummaryQuery = $"Select GroupId,Group.ModuleId, GroupName, GroupPath, ISNULL(ParentGroup, 0) as ParentGroup ,ModuleName,ToDo,SORORDER\r\n,(Select GroupName from Group mn where mn.GroupId = menu.ParentGroup) as ParentGroupName \r\nfrom Group \r\nleft outer join Module on module.ModuleId = menu.ModuleId\r\norder by ModuleName asc,ParentGroup asc, GroupName";
    List<GroupsRepositoryDto> menusDto = await ExecuteQueryAsync<GroupsRepositoryDto>(menuSummaryQuery);
    return menusDto;
  }

  public async Task<IEnumerable<Groups>> GroupsAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.GroupId).ToListAsync();

  public async Task<Groups> GroupAsync(int GroupId, bool trackChanges) => await FindByCondition(c => c.GroupId.Equals(GroupId), trackChanges).FirstOrDefaultAsync();

  public void CreateGroup(Groups Group) => Create(Group);

  public void UpdateGroup(Groups Group) => UpdateAsync(Group);

  public void DeleteGroup(Groups Group) => Delete(Group);
}
