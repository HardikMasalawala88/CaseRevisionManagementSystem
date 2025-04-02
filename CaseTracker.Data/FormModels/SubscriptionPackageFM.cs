using CaseTracker.Data.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTracker.Data.FormModels
{
    public class SubscriptionPackageFM : BaseEntityFM
    {
        public string Name { get; set; }
        public int DurationDays { get; set; }
        public bool IsTrial { get; set; }
        public int PackagePrice { get; set; }
    }
}
