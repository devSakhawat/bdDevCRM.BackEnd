using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Message
{
    public int MessageId { get; set; }

    public int FromHrRecordId { get; set; }

    public DateTime MessagingDate { get; set; }

    public string? MessageDetails { get; set; }

    public bool? IsArchive { get; set; }

    public string MessageSubject { get; set; } = null!;

    public int? ReferenceId { get; set; }

    public DateTime? ArchiveTime { get; set; }

    public int? ReferenceType { get; set; }

    public int? ModuleId { get; set; }

    public bool? IsDelivered { get; set; }

    public int? EmailTitleId { get; set; }

    public DateTime? DeliveredTime { get; set; }

    public DateOnly? ReferenceDate { get; set; }

    public string? RedirectLink { get; set; }

    public int? EmailNotificationTypeId { get; set; }

    public bool? IsSentWebNotification { get; set; }

    public bool? IsProcessed { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? NotificationOrder { get; set; }

    public int? ToHrRecordId { get; set; }

    public int? IsRead { get; set; }
}
