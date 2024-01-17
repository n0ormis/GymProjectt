using MediatR;

namespace GymProject.Contract.Commands.Payments;

public sealed record ConfirmPaymentCommand(string SessionId) : IRequest;
