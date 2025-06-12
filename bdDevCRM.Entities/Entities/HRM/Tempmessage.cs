using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Tempmessage
{
    public int Fromuserid { get; set; }

    public int Touserid { get; set; }

    public DateTime Messagingdate { get; set; }

    public string Messagedetails { get; set; } = null!;

    public int Isarchive { get; set; }

    public int Isread { get; set; }
}
