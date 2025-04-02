using CaseTracker.Data.ContextModels;
using CaseTracker.Data.ParameterModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTracker.Services.Interface
{
    public interface ISubscriptionService
    {
        Task<List<SubscriptionPackage>> GetNonTrialPackagesAsync();
        Task<SubscriptionPackage> GetTrialPackageAsync();
        Task<UserSubscription> CreateTrialSubscriptionAsync(string userId, SubscriptionPackage selectedPackage);
        Task<UserSubscription> GetUserActiveSubscriptionAsync(string userId);
        Task<SubscriptionPackage> GetPackageByIdAsync(Guid packageId);
        Task UpdateUserSubscriptionAsync(string userId, SubscriptionPackage selectedPackage);
        bool HasSubscription(string userId);
        UserSubscription GetUserSubscriptionById(string userId);
        Task<string> CreateRazorpayOrderAsync(int amount, Guid subscriptionPackageId, string userId);
        Task AssignSubscription(UserSubscription subscription);
        Task SavePaymentAsync(Payment payment);
        Task<List<Payment>> GetPaymentsByUserIdAsync(string userId);
        Task<bool> VerifyPayment(VerifyPaymentRequest request);
        Task CloseSubscriptionDialog();
        void RemoveSubscription(Guid subscriptionId);
    }
}
