using System;
using System.Collections.Generic;

namespace ClientlyWorkerService.Data;

public partial class UserRefreshToken
{
    public string UserRefreshTokenId { get; set; } = null!;

    public string? UserId { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime ExpiryDate { get; set; }

    public virtual AspNetUser? User { get; set; }
}
