using CaseTracker.Data.ParameterModels;
using CaseTracker.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace CaseTracker.DESKTOPUI.PayMentInterop
{
    public static class PaymentInterop
    {
        [CascadingParameter]
        public static IMudDialogInstance MudDialog { get; set; }

        [JSInvokable("VerifyPayment")]
        public static async Task<bool> VerifyPayment(VerifyPaymentRequest request)
        {
            try
            {
                using var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:44389") };
                var service = new SubscriptionService(httpClient);

                return await service.VerifyPayment(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in VerifyPayment: {ex.Message}");
                return false;
            }
        }

        [JSInvokable("CloseSubscriptionDialog")]
        public static async Task CloseSubscriptionDialog()
        {
            MudDialog?.Cancel();
        }
    }
}
