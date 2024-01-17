using GymProject.Contract.Commands.Payments;
using GymProject.Core.Services.Payments;
using MediatR;
using GymProject.Core.Auth;

namespace GymProject.Core.Commands.Payments;

internal sealed class ConfirmPaymentCommandHandler : IRequestHandler<ConfirmPaymentCommand>
{
    private readonly IPaymentService _paymentService;

    public ConfirmPaymentCommandHandler(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public async Task Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        => await _paymentService.VerifyPayment(request.SessionId);
}
