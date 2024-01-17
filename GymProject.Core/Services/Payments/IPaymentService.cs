using GymProject.Domain.Models.Memberships;
using Stripe.Checkout;

namespace GymProject.Core.Services.Payments;

public interface IPaymentService
{
    Task<string> CreateCheckoutSession(Subscription subscription);
    Task VerifyPayment(string sessionId);
}