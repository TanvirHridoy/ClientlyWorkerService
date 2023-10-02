using System;
using System.Collections.Generic;

namespace ClientlyWorkerService.Data;

public partial class ClientsAddOn
{
    public long Id { get; set; }

    public long? NewClientNumber { get; set; }

    public double? ClientPrice { get; set; }
}
