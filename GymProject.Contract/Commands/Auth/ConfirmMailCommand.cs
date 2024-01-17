using MediatR;

namespace GymProject.Contract.Commands.Auth;

public sealed record ConfirmMailCommand(string Token, string Email) : IRequest<string>;