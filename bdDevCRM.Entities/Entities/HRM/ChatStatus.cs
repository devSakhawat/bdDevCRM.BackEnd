using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ChatStatus
{
    public int ChatStatusId { get; set; }

    public string? ChatStatusTitle { get; set; }

    public string? IconPath { get; set; }
}
