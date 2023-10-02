using System;
using System.Collections.Generic;

namespace ClientlyWorkerService.Data;

public partial class PrimeTaskBook
{
    public long TaskBookId { get; set; }

    public string? TaskType { get; set; }

    public string? TaskName { get; set; }

    public string ClientId { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public DateTime? Deadline { get; set; }

    public string? TaskStatus { get; set; }

    public string? Assignee { get; set; }

    public string? InstanceId { get; set; }

    public bool? IsPrimeTask { get; set; }

    public string? AtchTitle { get; set; }

    public string? FileUrl { get; set; }

    public string? Notes { get; set; }

    public virtual Client Client { get; set; } = null!;
}
