using System;
using System.Collections.Generic;

namespace ClientlyWorkerService.Data;

public partial class Payroll
{
    public long Id { get; set; }

    public string? TaskType { get; set; }

    public string? CreateDate { get; set; }

    public string? Deadline { get; set; }

    public string? InstanceId { get; set; }

    public virtual Subscriber? Instance { get; set; }
}
