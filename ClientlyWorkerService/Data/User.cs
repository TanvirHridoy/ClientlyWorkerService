using System;
using System.Collections.Generic;

namespace ClientlyWorkerService.Data;

public partial class User
{
    public string UserAssignId { get; set; } = null!;

    public string ManagerId { get; set; } = null!;

    public string UserId { get; set; } = null!;
}
