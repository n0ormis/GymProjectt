using GymProject.Contract.Dtos.Auth;
using MediatR;

namespace GymProject.Contract.Commands.Auth;

public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<TokenDTO>;
