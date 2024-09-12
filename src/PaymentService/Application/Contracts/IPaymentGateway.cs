using PaymentService.Application.Models;

namespace PaymentService.Application.Contracts;

/// <summary>
/// Eventually, we will have multiple payment methods like PayPal, Stripe, etc.
/// So, we will create an interface for the payment service to use strategy pattern to switch between payment methods.
/// </summary>
public interface IPaymentGateway
{
    Task<PaystackResponse> MakePayment(double costOfProduct, string productNumber, string customerEmail);

    //Task<VerifyBank> VerifyAccountNumber(string acNumber, string bankCode, decimal amount);

    Task<VerifyBank> VerifyByAccountNumber(string acNumber, string bankCode, decimal amount);

    Task<GenerateRecipientDTO> GenerateRecipients(VerifyBank verifyBank);

    Task<MakeATransfer> SendMoney(string recip, decimal amount);
}