using CaseTracker.Data.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTracker.Data.FormModels
{
    public class UserSubscriptionFM : BaseEntityFM
    {
        public string UserId { get; set; } //LawyerId
        public Guid SubscriptionPackageId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public SubscriptionPackageFM Package { get; set; }
    }
}
