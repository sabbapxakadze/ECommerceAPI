using Microsoft.Extensions.Configuration;
using PayPal.Api;

namespace AppLibrary.Service
{
    public class PayPalService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public PayPalService(IConfiguration configuration)
        {
            _clientId = configuration["PayPal:ClientId"];
            _clientSecret = configuration["PayPal:ClientSecret"];
        }

        private APIContext GetAPIContext()
        {
            var config = new Dictionary<string, string>
        {
            { "mode", "sandbox" } // Change to "live" for production
        };

            var accessToken = new OAuthTokenCredential(_clientId, _clientSecret, config).GetAccessToken();
            return new APIContext(accessToken) { Config = config };
        }

        public Payment CreatePayment(decimal amount, string returnUrl, string cancelUrl)
        {
            var apiContext = GetAPIContext();

            var payment = new Payment
            {
                intent = "sale",
                payer = new Payer { payment_method = "paypal" },
                transactions = new List<Transaction>
            {
                new Transaction
                {
                    amount = new Amount
                    {
                        currency = "USD",
                        total = amount.ToString("F2") // Format to 2 decimal places
                    },
                    description = "Purchase from MyStore"
                }
            },
                redirect_urls = new RedirectUrls
                {
                    return_url = returnUrl,
                    cancel_url = cancelUrl
                }
            };

            return payment.Create(apiContext);
        }
    }
}