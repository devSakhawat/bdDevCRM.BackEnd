using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.System;

public partial class TokenBlacklist
{
    public string Token { get; set; } = null!;

    public DateTime ExpiryDate { get; set; }

    public Guid TokenId { get; set; }
}
