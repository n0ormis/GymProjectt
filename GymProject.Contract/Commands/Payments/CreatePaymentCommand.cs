using MediatR;

namespace GymProject.Contract.Commands.Payments;

public sealed record CreatePaymentCommand(Guid SubscriptionId) : IRequest<string>;