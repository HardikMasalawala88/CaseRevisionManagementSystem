using CaseTracker.Data.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTracker.Data.FormModels
{
    public class PaymentFM : BaseEntityFM
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public Guid SubscriptionPackageId { get; set; }
        public string UserId { get; set; } //Lawyer
        public DateTime PaymentDate { get; set; }
    }
}
