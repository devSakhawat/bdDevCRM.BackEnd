using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ChatMessage
{
    public int ChatingId { get; set; }

    public string? ChatMessage1 { get; set; }

    public DateTime? SentTime { get; set; }

    public string? ChatLocation { get; set; }

    public int? HrRecordId { get; set; }

    public string? ConnectionId { get; set; }

    public int? ToHrRecordId { get; set; }

    public DateTime? SeenTime { get; set; }

    public DateTime? ChatTime { get; set; }
}
