using System;
using System.Collections.Generic;

namespace ClientlyWorkerService.Data;

public partial class MasterPrimeTask
{
    public long PrimeTaskId { get; set; }

    public string PrimeTaskName { get; set; } = null!;

    public string? TaskCreationDate { get; set; }

    public string? Deadline { get; set; }
}
