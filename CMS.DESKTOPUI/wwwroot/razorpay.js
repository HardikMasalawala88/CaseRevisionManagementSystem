function openRazorpay(orderId, amount, userId) {
    var options = {
        key: "rzp_test_FjCNeS1n9Bj9ix", // Replace with your Razorpay key
        amount: amount * 100, // Amount in paise
        currency: "INR",
        order_id: orderId, // Razorpay Order ID
        handler: function (response) {
            var request = {
                PaymentId: response.razorpay_payment_id,
                OrderId: orderId,
                Signature: response.razorpay_signature
            };

            // Call the Blazor method to verify payment
            DotNet.invokeMethodAsync('CMS.DESKTOPUI', 'VerifyPayment', request)
                .then(success => {
                    if (success) {
                        debugger
                        //DotNet.invokeMethodAsync('CMS.DESKTOPUI', 'OnPaymentVerification', success);

                        // Close the dialog by invoking Blazor method
                        DotNet.invokeMethodAsync('CMS.DESKTOPUI', 'CloseSubscriptionDialog');
                    } else {
                        DotNet.invokeMethodAsync('CMS.DESKTOPUI', 'OnPaymentVerification', error);
                    }
                })
                .catch(error => {
                    console.error("Error calling VerifyPayment:", error);
                    alert("An error occurred during payment verification. Please try again.");
                });
        },
        prefill: {
            email: "user@example.com", // Replace with user email
            contact: "9999999999" // Replace with user contact
        }
    };

    var razorpay = new Razorpay(options);
    razorpay.open();
}

function closeSubscriptionDialog() {
    // Assuming your subscription dialog has an ID like 'subscriptionDialog'
    const dialog = document.getElementById('subscriptionDialog');
    if (dialog) {
        dialog.style.display = 'none'; // Hide the dialog
        console.log('Subscription dialog closed.');
    } else {
        console.warn('Subscription dialog not found.');
    }
}

