using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.ContextModels
{
    public class UserSubscription : BaseEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long PackageId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public SubscriptionPackage Package { get; set; } 
    }
}
