using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface IGroupMemberRepository : IRepositoryBase<GroupMember>
{
  Task<IEnumerable<GroupMemberRepositoryDto>> GroupMemberByUserId(int userId, bool trackChanges);
}
