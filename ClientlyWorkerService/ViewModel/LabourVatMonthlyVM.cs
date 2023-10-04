using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientlyWorkerService.ViewModel
{
    public class LabourVatMonthlyVM
    {
        public long Id { get; set; }

        public string? TaskType { get; set; }

        public string? FromDate { get; set; }

        public string? TillDate { get; set; }

        public string? CreateDate { get; set; }

        public string? Deadline { get; set; }

        public DateTime DFromDate { get; set; }
        public DateTime DTillDate { get; set; }
        public DateTime DCreateDate { get; set; }
        public DateTime DDeadline { get; set; }

        public string? InstanceId { get; set; }
    }

    
}
