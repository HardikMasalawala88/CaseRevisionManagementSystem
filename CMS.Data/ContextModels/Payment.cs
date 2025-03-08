using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.ContextModels
{
    public class Payment : BaseEntity
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public int SubscriptionPackageId { get; set; }
        public long UserId { get; set; }
        public DateTime PaymentDate { get; set; }
    }

}
