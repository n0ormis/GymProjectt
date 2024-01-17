using GymProject.Contract.Commands.Payments;
using GymProject.Core.Services.Payments;
using GymProject.Infastructure.Repositories.Memberships;
using MediatR;
using Stripe.Checkout;
using GymProject.Domain.Models.Memberships;

namespace GymProject.Core.Commands.Payments;

internal sealed class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, string>
{
    private readonly IPaymentService _paymentService;
    private readonly IMembershipRepository _membershipRepository;

    public CreatePaymentCommandHandler(IPaymentService paymentService, IMembershipRepository membershipRepository)
    {
        _paymentService = paymentService;
        _membershipRepository = membershipRepository;
    }

    public async Task<string> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var membership = await _membershipRepository.GetAsync(request.SubscriptionId);
        if (membership is null)
            throw new(); //todo CUSTOm exception
        
        return await _paymentService.CreateCheckoutSession(membership);
    }
}