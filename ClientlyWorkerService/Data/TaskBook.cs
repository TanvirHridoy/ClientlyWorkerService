using System;
using System.Collections.Generic;

namespace ClientlyWorkerService.Data;

public partial class TaskBook
{
    public long TaskId { get; set; }

    public string TaskName { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Notes { get; set; } = null!;

    public DateTime DeadLine { get; set; }

    public string? InstanceId { get; set; }

    public string? AtchTitle { get; set; }

    public string? FileUrl { get; set; }
}
