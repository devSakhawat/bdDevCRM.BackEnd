using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class GreetingsOrWishNotificationLog
{
    public int GreetingsOrWishNotifyId { get; set; }

    public int? HrRecordId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? NotificationType { get; set; }

    public int? IsNotify { get; set; }
}
