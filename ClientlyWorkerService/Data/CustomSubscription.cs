using System;
using System.Collections.Generic;

namespace ClientlyWorkerService.Data;

public partial class CustomSubscription
{
    public string CustomSubId { get; set; } = null!;

    public string InstanceId { get; set; } = null!;

    public int UserNumber { get; set; }

    public int ClientNumber { get; set; }

    public string Frequency { get; set; } = null!;

    public double Amount { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime ExpiryDate { get; set; }
}
