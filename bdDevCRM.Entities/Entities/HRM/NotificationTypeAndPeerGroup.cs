using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class NotificationTypeAndPeerGroup
{
    public int PeerGroupId { get; set; }

    public int? HrRecordId { get; set; }

    public int? NotificationTypeId { get; set; }

    public int? ForHrRecordId { get; set; }
}
