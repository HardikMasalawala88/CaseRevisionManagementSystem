using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTracker.Data.ContextModels
{
    public class SubscriptionPackage : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int DurationDays { get; set; }
        public bool IsTrial { get; set; }
        public int PackagePrice { get; set; }
    }
}
