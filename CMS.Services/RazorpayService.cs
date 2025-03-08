using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services
{
    public class RazorpayService
    {
        private readonly string _key = "rzp_test_FjCNeS1n9Bj9ix";
        private readonly string _secret = "w7QnbMpgFneOOST1Z3bHXZNp";

        public string CreateOrder(int amount, string currency = "INR")
        {
            if (amount < 1)
            {
                throw new ArgumentException("Amount must be at least ₹1.00.");
            }
            var client = new RazorpayClient(_key, _secret);
            var options = new Dictionary<string, object>
            {
                { "amount", amount * 100 }, // Razorpay expects amount in paise
                { "currency", currency },
                { "payment_capture", 1 }
            };

            Order order = client.Order.Create(options);
            return order["id"].ToString();
        }
    }
}
