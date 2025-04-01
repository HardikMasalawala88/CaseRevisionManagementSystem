using CaseTracker.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using CaseTracker.Data.ParameterModels;
using CaseTracker.Data.ContextModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System.Linq;
using StackExchange.Redis;
using System.Security.Cryptography.Xml;
using CaseTracker.API.Utilities;
using Razorpay.Api;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;
    private readonly ApplicationContext _context;
    private readonly RazorpayClient _razorpayClient;
    private readonly string _razorpayKey = "rzp_test_FjCNeS1n9Bj9ix";
    private readonly string _razorpaySecret = "w7QnbMpgFneOOST1Z3bHXZNp";

    public PaymentController(ISubscriptionService subscriptionService, ApplicationContext context, IConfiguration configuration)
    {
        _subscriptionService = subscriptionService;
        _context = context;
        var razorpayKey = configuration["Razorpay:Key"];
        var razorpaySecret = configuration["Razorpay:Secret"];
        _razorpayClient = new RazorpayClient(razorpayKey, razorpaySecret);
    }

    [HttpPost("create-order")]
    public async Task<IActionResult> CreateOrder([FromBody] PaymentRequest paymentRequest)
    {
        if (paymentRequest.Amount < 1)
        {
            return BadRequest(new { IsSuccess = false, Message = "Amount must be at least ₹1.00." });
        }

        // Create an order with Razorpay API
        var razorpayClient = new RazorpayClient("rzp_test_FjCNeS1n9Bj9ix", "w7QnbMpgFneOOST1Z3bHXZNp");

        var options = new Dictionary<string, object>
        {
            { "amount", paymentRequest.Amount * 100 },  // Convert amount to paise
            { "currency", "INR" },
            { "receipt", Guid.NewGuid().ToString() },
            { "payment_capture", 1 }
        };

        var order = razorpayClient.Order.Create(options);

        var payment = new CaseTracker.Data.ContextModels.Payment
        {
            OrderId = order["id"].ToString(),
            Amount = paymentRequest.Amount,
            UserId = paymentRequest.UserId,
            SubscriptionPackageId = paymentRequest.SubscriptionPackageId,
            Status = "Pending",
            PaymentDate = DateTime.UtcNow
        };

        await _subscriptionService.SavePaymentAsync(payment);

        // Return order details to frontend
        return Ok(new
        {
            IsSuccess = true,
            OrderData = new
            {
                OrderId = order["id"].ToString(),
                Amount = paymentRequest.Amount,
                Currency = "INR"
            }
        });
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyPayment([FromBody] VerifyPaymentRequest request)
    {
        try
        {
            string secret = _razorpaySecret;

            // Validate the signature
            var attributes = new Dictionary<string, string>
            {
                { "razorpay_payment_id", request.PaymentId },
                { "razorpay_order_id", request.OrderId},
                { "razorpay_signature", request.Signature }
            };

            CaseTracker.API.Utilities.Utils.verifyPaymentSignature(attributes);

            // Fetch the payment record from the database
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.OrderId == request.OrderId);
            if (payment == null)
            {
                return NotFound(new { status = "Failed", message = "Payment record not found." });
            }

            // Update the payment status and details
            payment.Status = "Success";
            payment.PaymentDate = DateTime.UtcNow; 

            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();

            var package = await _context.SubscriptionPackages.FirstOrDefaultAsync(p => p.Id == payment.SubscriptionPackageId);
            if (package == null)
            {
                return NotFound(new { status = "Failed", message = "Subscription package not found." });
            }

            // Define subscription start and end dates
            var startDate = DateTime.UtcNow;
            var endDate = startDate.AddMonths(package.DurationDays);

            var userSubscription = new UserSubscription
            {
                UserId = payment.UserId,
                PackageId = payment.SubscriptionPackageId,
                StartDate = startDate,
                EndDate = endDate,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            // Insert into UserSubscription table
            _context.UserSubscriptions.Add(userSubscription);
            await _context.SaveChangesAsync();

            return Ok(new { status = "Success", message = "Payment verified successfully." });
        }
        catch (Exception ex)
        {
            // Log the exception and return an error response
            Console.WriteLine($"Payment verification failed: {ex.Message}");
            return BadRequest(new { status = "Failed", message = "Payment verification failed." });
        }
    }
}
