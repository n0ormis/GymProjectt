using System.Runtime.CompilerServices;
using GymProject.Core.Auth;
using GymProject.Domain.Models.Payments;
using GymProject.Infastructure.Repositories.Memberships;
using GymProject.Infastructure.Repositories.Payments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using GymProject.Domain.Models.Auth;
using GymProject.Domain.Models.Memberships;
using PaymentSession = GymProject.Domain.Models.Payments.PaymentSession;
using Subscription = Stripe.Subscription;
namespace GymProject.Core.Services.Payments;

internal sealed class PaymentService : IPaymentService
{
    private readonly IIdentityService _identityService;
    private readonly IConfigurationSection _stripeSettings;
    private readonly IConfigurationSection _webSettings;
    private readonly IConfiguration _configuration;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMembershipRepository _membershipRepository;

    public PaymentService(
        IConfiguration configuration, 
        IIdentityService identityService, 
        IPaymentRepository paymentRepository, 
        IMembershipRepository membershipRepository)
    {
        _configuration = configuration; //TODO configuracje wrzucić do jednego miejsca
        _identityService = identityService;
        _paymentRepository = paymentRepository;
        _membershipRepository = membershipRepository;
        _stripeSettings = _configuration.GetSection("StripeSettings");
        _webSettings = _configuration.GetSection("SPA");
        StripeConfiguration.ApiKey = _stripeSettings["apiKey"]; //TODO zmienić to na jakiś lepszy sposób
    }

    public async Task VerifyPayment(string sessionId)
    {
        var sessionService = new SessionService();
        var chagre = await sessionService.GetAsync(sessionId);
        if (chagre.Status.Equals("complete"))
        {
            var session = await _paymentRepository.GetSession(sessionId);
            var membersip = await _membershipRepository.GetAsync(session.ProductId);
            await _identityService.VerifyPayment(session.UserId, membersip.Duration);
            await _paymentRepository.ApproveSession(sessionId);
        }
    }

    public async Task<string> CreateCheckoutSession(Domain.Models.Memberships.Subscription subscription)
    {
        var userId = Guid.Parse(_identityService.GetUserId());
        var session = await GenerateSession(subscription);
        if (session is null)
            return ""; //throw new
       
        await _paymentRepository.CreateSession(new Domain.Models.Payments.PaymentSession() { SessionUrl = session.Id, ProductId = subscription.Id, UserId = userId});
        return session.Url;
    }

    private async Task<SessionData> GenerateSession(Domain.Models.Memberships.Subscription subscription)
    {
        var oprions = new SessionCreateOptions()
        {
            PaymentMethodTypes = new()
            {
                "card",
            },
            LineItems = new()
            {
                new()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        UnitAmount = Convert.ToInt64(subscription.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = subscription.Name,
                            Description = subscription.Description,
                        },
                    },
                    Quantity = 1,
                },
            },
            Mode = "payment",
            SuccessUrl = $"{_webSettings["http"]}/order-success/{{CHECKOUT_SESSION_ID}}",
            CancelUrl = $"{_webSettings["http"]}/order-cancelled",
        };

        var service = new SessionService();
        var session = service.Create(oprions);
        return new()
        {
            Url = session.Url,
            Id = session.Id,
        };
    }
}