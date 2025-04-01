using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTracker.Data.ParameterModels
{
    public class PaymentRequest
    {
        public decimal Amount { get; set; } 
        public long UserId { get; set; } 
        public int SubscriptionPackageId { get; set; } 
    }

    public class VerifyPaymentRequest
    {
        public string PaymentId { get; set; }
        public string OrderId { get; set; }
        public string Signature { get; set; }
    }

    public class ApiResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    //public class RazorpayCallback
    //{
    //    public string OrderId { get; set; }
    //    public string Status { get; set; } // e.g., "success", "failed", etc.
    //}

}
