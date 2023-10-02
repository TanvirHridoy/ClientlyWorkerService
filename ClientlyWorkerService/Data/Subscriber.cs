using System;
using System.Collections.Generic;

namespace ClientlyWorkerService.Data;

public partial class Subscriber
{
    public string InstanceId { get; set; } = null!;

    public string Cvr { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ExpireDate { get; set; }

    public bool? IsSubscriber { get; set; }

    public int? IsActive { get; set; }

    public string? SubscriptionId { get; set; }

    public virtual ICollection<AspNetUser> AspNetUsers { get; set; } = new List<AspNetUser>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<LaborVatMonthly> LaborVatMonthlies { get; set; } = new List<LaborVatMonthly>();

    public virtual ICollection<PaymentHistory> PaymentHistories { get; set; } = new List<PaymentHistory>();

    public virtual ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
}
