using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ChatUsers
{
    public int ChatUserId { get; set; }

    public string? ConnectionId { get; set; }

    public int? HrRecordId { get; set; }

    public DateTime? ConnectionTime { get; set; }

    public bool? IsOnline { get; set; }

    public DateTime? DisConnectionTime { get; set; }

    public int? ChatStatusId { get; set; }
}
