using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using CMS.Data.ContextModels;
using CMS.Data.ParameterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Services.Interface;
using Payment = CMS.Data.ContextModels.Payment;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CMS.Services
{
    // SubscriptionService.cs
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ApplicationContext _context;
        private readonly IUserService _userService;
        private readonly string _razorpayKey = "rzp_test_FjCNeS1n9Bj9ix";  
        private readonly string _razorpaySecret = "w7QnbMpgFneOOST1Z3bHXZNp";
        private readonly HttpClient _httpClient;
        private HttpClient httpClient;

        public SubscriptionService(ApplicationContext context, IUserService userService, HttpClient httpClient, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _userService = userService;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient = httpClientFactory.CreateClient();
        }

        public SubscriptionService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<SubscriptionPackage>> GetNonTrialPackagesAsync()
        {
            return await _context.SubscriptionPackages.Where(p => !p.IsTrial).ToListAsync();
        }

        public async Task<SubscriptionPackage> GetTrialPackageAsync()
        {
            return await _context.SubscriptionPackages.FirstOrDefaultAsync(p => p.IsTrial && p.PackagePrice == 0);
        }


        public async Task<UserSubscription> CreateTrialSubscriptionAsync(long userId, SubscriptionPackage selectedPackage)
        {
            var trialPackage = await GetTrialPackageAsync();
            if (trialPackage == null) throw new Exception("Trial package not found");

            var subscription = new UserSubscription
            {
                UserId = userId,
                PackageId = trialPackage.Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(selectedPackage.DurationDays),
                IsActive = true
            };

            _context.UserSubscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            return subscription;
        }

        public async Task<UserSubscription> GetUserActiveSubscriptionAsync(long userId)
        {
            return await _context.UserSubscriptions
                                 .Where(s => s.UserId == userId && s.IsActive && s.EndDate >= DateTime.Now)
                                 .Include(s => s.Package)
                                 .FirstOrDefaultAsync();
        }

        public async Task<SubscriptionPackage> GetPackageByIdAsync(long packageId)
        {
            return await _context.SubscriptionPackages
                                 .FirstOrDefaultAsync(p => p.Id == packageId);
        }

        public async Task UpdateUserSubscriptionAsync(long userId, SubscriptionPackage selectedPackage)
        {
            var user = _context.UserData.Any(x => x.Id == userId);
            if (!_context.UserData.Any(x => x.Id == userId)) {
                throw new Exception("User not found");
            }

            var currentSubscription = await GetUserActiveSubscriptionAsync(userId);
            if (currentSubscription != null)
            {
                currentSubscription.IsActive = false;
            }

            // Create a new subscription with the selected package
            var newSubscription = new UserSubscription
            {
                UserId = userId,
                PackageId = selectedPackage.Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(selectedPackage.DurationDays),
                IsActive = true
            };

            _context.UserSubscriptions.Add(newSubscription);
            await _context.SaveChangesAsync();
        }

        public bool HasSubscription(long userId)
        {
            return _context.UserSubscriptions.Any(i => i.UserId == userId);
        }

        public UserSubscription GetUserSubscriptionById(long userId)
        {
            return _context.UserSubscriptions.FirstOrDefault(i => i.UserId == userId);
        }

        public async Task<string> CreateRazorpayOrderAsync(int amount, int subscriptionPackageId, long userId)
        {
            try
            {
                // Initialize Razorpay client
                var razorpayClient = new RazorpayClient(_razorpayKey, _razorpaySecret);

                // Prepare order creation options
                var options = new Dictionary<string, object>
                {
                    { "amount", (amount * 100) }, // Razorpay expects the amount in paise
                    { "currency", "INR" },
                    { "payment_capture", 1 } // Automatically capture the payment
                };

                // Create the order
                var order = razorpayClient.Order.Create(options);
                var orderId = order["id"].ToString();

                // Save order to the database
                var payment = new Payment
                {
                    OrderId = orderId,
                    Amount = amount,
                    Status = "Pending",
                    UserId = userId,
                    SubscriptionPackageId = subscriptionPackageId,
                    PaymentDate = DateTime.UtcNow
                };

                // Save payment to the database (use your DbContext or service layer)
                await _context.Payments.AddAsync(payment);
                await _context.SaveChangesAsync();

                // Return the order ID
                return orderId;
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging mechanism)
                Console.WriteLine($"Error creating Razorpay order: {ex.Message}");
                throw;
            }
        }

        public async Task AssignSubscription(UserSubscription subscription)
        {
            _context.UserSubscriptions.Add(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task SavePaymentAsync(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment), "Payment cannot be null");
            }

            try
            {
                // Add the payment to the Payments DbSet
                await _context.Payments.AddAsync(payment);

                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error saving payment: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Payment>> GetPaymentsByUserIdAsync(long userId)
        {
            try
            {
                // Fetch payment details for the given userId
                return await _context.Payments
                                     .Where(p => p.UserId == userId)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error fetching payment details: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> VerifyPayment(VerifyPaymentRequest request)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:44389")
                };

                var response = await client.PostAsJsonAsync("/api/Payment/verify", request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
                    return result?.Status == "Success"; 
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Error: {errorContent}");
                    throw new Exception($"API call failed with status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error verifying payment: {ex.Message}");
                return false;
            }
        }

        [JSInvokable("CloseSubscriptionDialog")]
        public async Task CloseSubscriptionDialog()
        {
            Console.WriteLine("Closing the subscription dialog...");
        }

        public void RemoveSubscription(long subscriptionId)
        {
            var subscription = _context.UserSubscriptions.Find(subscriptionId);
            if (subscription != null)
            {
                _context.UserSubscriptions.Remove(subscription);
                _context.SaveChanges();
            }
        }


    }

}
