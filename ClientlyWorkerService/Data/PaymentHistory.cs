using System;
using System.Collections.Generic;

namespace ClientlyWorkerService.Data;

public partial class PaymentHistory
{
    public long Id { get; set; }

    public string PaymentId { get; set; } = null!;

    public string SubscriptionId { get; set; } = null!;

    public string PackageName { get; set; } = null!;

    public int MinimumUser { get; set; }

    public int MaximumUser { get; set; }

    public double FirstUserPrice { get; set; }

    public double NextUserPrice { get; set; }

    public int AdditionalUserNumber { get; set; }

    public double AdditionalUserAmount { get; set; }

    public int ClientNumber { get; set; }

    public int AddOnsClientNumber { get; set; }

    public double AddonClientPrice { get; set; }

    public double PackagePrice { get; set; }

    public int TotalAmount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string InstanceId { get; set; } = null!;

    public bool IsComplete { get; set; }

    public virtual Subscriber Instance { get; set; } = null!;
}
