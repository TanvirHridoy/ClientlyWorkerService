using System;
using System.Collections.Generic;

namespace ClientlyWorkerService.Data;

public partial class Client
{
    public string ClientId { get; set; } = null!;

    public string Cvr { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public string? ReelOwner { get; set; }

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string ClientType { get; set; } = null!;

    public string FinancialYearStartDate { get; set; } = null!;

    public string FinancialYear { get; set; } = null!;

    public string LaborVatType { get; set; } = null!;

    public string AnnualReportType { get; set; } = null!;

    public string TaxStatementType { get; set; } = null!;

    public string PayrollType { get; set; } = null!;

    public string Notes { get; set; } = null!;

    public string? InstanceId { get; set; }

    public string? EntryBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Subscriber? Instance { get; set; }

    public virtual ICollection<PrimeTaskBook> PrimeTaskBooks { get; set; } = new List<PrimeTaskBook>();
}
