using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface IGroupsRepository : IRepositoryBase<Groups>
{
  Task<List<GroupsRepositoryDto>> GroupSummary(bool trackChanges);

  Task<IEnumerable<Groups>> GroupsAsync(bool trackChanges);
  Task<Groups> GroupAsync(int GroupId, bool trackChanges);

  public void CreateGroup(Groups Group);

  public void UpdateGroup(Groups Group);

  public void DeleteGroup(Groups Group);
}
