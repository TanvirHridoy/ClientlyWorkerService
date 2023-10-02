using System;
using System.Collections.Generic;

namespace ClientlyWorkerService.Data;

public partial class PrimeTask
{
    public long PrimeTaskId { get; set; }

    public string PrimeTaskName { get; set; } = null!;

    public string? TaskCreationDate { get; set; }

    public string? Deadline { get; set; }

    public string? InstanceId { get; set; }
}
