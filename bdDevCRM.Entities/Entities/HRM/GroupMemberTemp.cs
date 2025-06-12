using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class GroupMemberTemp
{
    public int GroupId { get; set; }

    public int UserId { get; set; }

    public string? GroupOption { get; set; }
}
