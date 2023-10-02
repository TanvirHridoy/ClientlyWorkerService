using System;
using System.Collections.Generic;

namespace ClientlyWorkerService.Data;

public partial class PaymentGateway
{
    public string LiveSecretKey { get; set; } = null!;

    public string LiveCheckoutKey { get; set; } = null!;

    public string TestSecretKey { get; set; } = null!;

    public string TestCheckoutKey { get; set; } = null!;
}
