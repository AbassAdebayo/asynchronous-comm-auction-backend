using PaymentService.Application.Contracts;
using PaymentService.Application.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PaymentService.Infrastructure.Services
{
    /// <summary>
    /// Provides services to interact with the Paystack payment gateway.
    /// </summary>
    public class PaystackServices : IPaymentGateway
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaystackServices> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaystackServices"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client used for making requests.</param>
        /// <param name="configuration">The application configuration for retrieving settings like API keys.</param>
        /// <param name="logger">The logger used for logging information and errors.</param>
        public PaystackServices(HttpClient httpClient, IConfiguration configuration, ILogger<PaystackServices> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Initiates a payment via Paystack.
        /// </summary>
        /// <param name="costOfProduct">The cost of the product in Naira.</param>
        /// <param name="productNumber">A unique identifier for the product.</param>
        /// <param name="customerEmail">The customer's email address.</param>
        /// <returns>A <see cref="PaystackResponse"/> object containing the result of the payment initiation.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the API key is not found in the configuration.</exception>
        public async Task<PaystackResponse> MakePayment(double costOfProduct, string productNumber, string customerEmail)
        {
            var apiKey = _configuration["Paystack:APIKey"];
            if (string.IsNullOrEmpty(apiKey)) throw new InvalidOperationException("API Key is missing from the configuration.");

            costOfProduct *= 100; // Convert to kobo
            productNumber = string.IsNullOrWhiteSpace(productNumber)
                            ? Guid.NewGuid().ToString().Replace('-', 'y')
                            : productNumber;

            var request = new
            {
                amount = costOfProduct,
                email = customerEmail,
                reference = productNumber,
                currency = "NGN",
            };

            var response = await SendPostRequestAsync<PaystackResponse>("https://api.paystack.co/transaction/initialize", request, apiKey);
            return response;
        }

        /// <summary>
        /// Verifies a bank account number using Paystack.
        /// </summary>
        /// <param name="acNumber">The account number to verify.</param>
        /// <param name="bankCode">The bank code associated with the account number.</param>
        /// <param name="amount">The amount associated with the verification.</param>
        /// <returns>A <see cref="VerifyBank"/> object containing the result of the account verification.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the API key is not found in the configuration.</exception>
        public async Task<VerifyBank> VerifyByAccountNumber(string acNumber, string bankCode, decimal amount)
        {
            var apiKey = _configuration["Paystack:APIKey"];
            if (string.IsNullOrEmpty(apiKey)) throw new InvalidOperationException("API Key is missing from the configuration.");

            var uri = $"https://api.paystack.co/bank/resolve?account_number={acNumber}&bank_code={bankCode}";
            var response = await SendGetRequestAsync<VerifyBank>(uri, apiKey);
            return response;
        }

        /// <summary>
        /// Generates a recipient for a bank transfer via Paystack.
        /// </summary>
        /// <param name="verifyBank">The bank verification details containing account information.</param>
        /// <returns>A <see cref="GenerateRecipientDTO"/> object containing the recipient generation result.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the API key is not found in the configuration.</exception>
        public async Task<GenerateRecipientDTO> GenerateRecipients(VerifyBank verifyBank)
        {
            var apiKey = _configuration["Paystack:APIKey"];
            if (string.IsNullOrEmpty(apiKey)) throw new InvalidOperationException("API Key is missing from the configuration.");

            var request = new
            {
                type = "nuban",
                name = verifyBank?.Data?.AccountNumber ?? "Unknown Name",
                account_number = verifyBank?.Data?.AccountNumber ?? "Unknown Account Number",
                bank_code = verifyBank?.Data?.BankId.ToString() ?? "Unknown Bank Code",
                currency = "NGN",
            };

            var response = await SendPostRequestAsync<GenerateRecipientDTO>("https://api.paystack.co/transferrecipient", request, apiKey);
            return response;
        }

        /// <summary>
        /// Sends money to a recipient via Paystack.
        /// </summary>
        /// <param name="recip">The recipient code to send money to.</param>
        /// <param name="amount">The amount to send in Naira.</param>
        /// <returns>A <see cref="MakeATransfer"/> object containing the result of the transfer.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the API key is not found in the configuration.</exception>
        public async Task<MakeATransfer> SendMoney(string recip, decimal amount)
        {
            var apiKey = _configuration["Paystack:APIKey"];
            if (string.IsNullOrEmpty(apiKey)) throw new InvalidOperationException("API Key is missing from the configuration.");

            var request = new
            {
                recipient = recip,
                amount = amount * 100, // Convert to kobo
                currency = "NGN",
                source = "balance"
            };

            var response = await SendPostRequestAsync<MakeATransfer>("https://api.paystack.co/transfer", request, apiKey);
            return response;
        }

        /// <summary>
        /// Sends a POST request to the specified URL and deserializes the response.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response into.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="request">The request payload to send.</param>
        /// <param name="apiKey">The API key for authentication.</param>
        /// <returns>The deserialized response object of type <typeparamref name="T"/>.</returns>
        /// <exception cref="Exception">Thrown if an error occurs while making the request.</exception>
        private async Task<T> SendPostRequestAsync<T>(string url, object request, string apiKey)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                var response = await _httpClient.PostAsync(url, content);

                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while making a POST request.");
                throw;
            }
        }

        /// <summary>
        /// Sends a GET request to the specified URL and deserializes the response.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response into.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="apiKey">The API key for authentication.</param>
        /// <returns>The deserialized response object of type <typeparamref name="T"/>.</returns>
        /// <exception cref="Exception">Thrown if an error occurs while making the request.</exception>
        private async Task<T> SendGetRequestAsync<T>(string url, string apiKey)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                var response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while making a GET request.");
                throw;
            }
        }
    }
}
