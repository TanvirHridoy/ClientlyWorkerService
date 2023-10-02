using System;
using System.Collections.Generic;

namespace ClientlyWorkerService.Data;

public partial class LaborVatMonthlyMaster
{
    public string? TaskType { get; set; }

    public string? FromDate { get; set; }

    public string? TillDate { get; set; }

    public string? CreateDate { get; set; }

    public string? Deadline { get; set; }
}
