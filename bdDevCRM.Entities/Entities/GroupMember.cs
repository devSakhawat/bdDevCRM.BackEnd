using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class GroupMember
{
    public int GroupId { get; set; }

    public int UserId { get; set; }

    public string? GroupOption { get; set; }
}
