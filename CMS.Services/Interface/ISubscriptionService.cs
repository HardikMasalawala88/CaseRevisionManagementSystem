using CMS.Data.ContextModels;
using CMS.Data.ParameterModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.Interface
{
    public interface ISubscriptionService
    {
        Task<List<SubscriptionPackage>> GetNonTrialPackagesAsync();
        Task<SubscriptionPackage> GetTrialPackageAsync();
        Task<UserSubscription> CreateTrialSubscriptionAsync(long userId, SubscriptionPackage selectedPackage);
        Task<UserSubscription> GetUserActiveSubscriptionAsync(long userId);
        Task<SubscriptionPackage> GetPackageByIdAsync(long packageId);
        Task UpdateUserSubscriptionAsync(long userId, SubscriptionPackage selectedPackage);
        bool HasSubscription(long userId);
        UserSubscription GetUserSubscriptionById(long userId);
        Task<string> CreateRazorpayOrderAsync(int amount, int subscriptionPackageId, long userId);
        Task AssignSubscription(UserSubscription subscription);
        Task SavePaymentAsync(Payment payment);
        Task<List<Payment>> GetPaymentsByUserIdAsync(long userId);
        Task<bool> VerifyPayment(VerifyPaymentRequest request);
        Task CloseSubscriptionDialog();
        void RemoveSubscription(long subscriptionId);
    }
}
