using System;
using System.Collections.Generic;

namespace ClientlyWorkerService.Data;

public partial class SubscriptionType
{
    public string SubscriptionId { get; set; } = null!;

    public string PackageName { get; set; } = null!;

    public int MinimumUser { get; set; }

    public int? MaximumUser { get; set; }

    public int ClientNumber { get; set; }

    public double? FirstUserPrice { get; set; }

    public double? NextUserPrice { get; set; }

    public int Priority { get; set; }
}
