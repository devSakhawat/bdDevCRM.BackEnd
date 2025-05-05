using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoryDtos.Core.SystemAdmin;

public class GroupMemberRepositoryDto
{
  public int GroupId { get; set; }
  public int UserId { get; set; }
  public string GroupOption { get; set; }
}
