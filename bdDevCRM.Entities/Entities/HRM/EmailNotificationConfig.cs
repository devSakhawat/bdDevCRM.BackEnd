using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmailNotificationConfig
{
    public int EmailNotificationConfigId { get; set; }

    public int? NotificationTypeId { get; set; }

    public string? NotificationTitle { get; set; }

    public string? NotificationKey { get; set; }

    public bool? IsEnable { get; set; }

    public bool? IsFirstReminder { get; set; }

    public int? FirstReminderBeforeDays { get; set; }

    public bool? IsSecondReminder { get; set; }

    public int? SecondReminderSendBeforeDays { get; set; }

    public bool? IsThirdReminder { get; set; }

    public int? ThirdReminderSendBeforeDays { get; set; }

    public string? ScheduleType { get; set; }

    public TimeOnly? ServiceRunningTimeSchedule { get; set; }

    public int? EmailProcessAfterSendingHour { get; set; }

    public bool? IsManualRunning { get; set; }

    public DateOnly? ManualRunningDate { get; set; }

    public bool? IsActive { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? NotificationSendBeforeDays { get; set; }

    public int? NotificationSendBeforeMonths { get; set; }

    public int? NotificationSendBeforeYears { get; set; }

    public int? EmailSendingIntervalHoure { get; set; }

    public int? NotificationSourceId { get; set; }
}
