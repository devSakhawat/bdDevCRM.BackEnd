using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Shift25032023
{
    public int Shiftid { get; set; }

    public string Shiftcode { get; set; } = null!;

    public int Isdefault { get; set; }

    public int Shifttype { get; set; }

    public string Shiftname { get; set; } = null!;

    public string? Shiftdescription { get; set; }

    public string? Officehourdescription { get; set; }

    public int Gracetimein { get; set; }

    public int Gracetimeout { get; set; }

    public int Allowedlate { get; set; }

    public int Dayoffenable { get; set; }

    public int Shiftstatus { get; set; }

    public int Movealltothisshift { get; set; }

    public decimal? Lateallowed { get; set; }

    public decimal? Graceloggofftime { get; set; }

    public decimal? Nextdaylogingracetime { get; set; }

    public int? UserId { get; set; }

    public int? IsSpecial { get; set; }

    public decimal? BreakupDuration { get; set; }

    public int? SameDayNextShift { get; set; }

    public string? WeakName { get; set; }

    public int? IsGlobal { get; set; }

    public int? IsNightShift { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? IsOtShift { get; set; }
}
