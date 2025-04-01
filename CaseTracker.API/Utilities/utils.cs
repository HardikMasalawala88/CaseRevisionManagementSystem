using Razorpay.Api;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System;
using Razorpay.Api.Errors;

namespace CaseTracker.API.Utilities
{
    public static class Utils
    {
        private static readonly string _razorpayKey = "rzp_test_FjCNeS1n9Bj9ix";
        private static readonly string _razorpaySecret = "w7QnbMpgFneOOST1Z3bHXZNp";
        public static void verifyPaymentSignature(Dictionary<string, string> attributes)
        {
            if (!attributes.ContainsKey("razorpay_order_id") ||
                !attributes.ContainsKey("razorpay_payment_id") ||
                !attributes.ContainsKey("razorpay_signature"))
            {
                throw new ArgumentException("Invalid attributes for signature verification.");
            }

            string keySecret = _razorpaySecret; 
            string payload = $"{attributes["razorpay_order_id"]}|{attributes["razorpay_payment_id"]}";
            string actualSignature = attributes["razorpay_signature"];

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(keySecret)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                string computedSignature = BitConverter.ToString(hash).Replace("-", "").ToLower(); // Use lowercase hex string
                Console.WriteLine($"Computed Signature: {computedSignature}");

                if (computedSignature != actualSignature)
                    throw new InvalidOperationException("Invalid payment signature.");
            }
        } 
    }
}
