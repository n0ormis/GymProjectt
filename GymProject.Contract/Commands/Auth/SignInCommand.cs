using MediatR;
using GymProject.Contract.Dtos.Auth;

namespace GymProject.Contract.Commands.Auth;

public sealed record SignInCommand(string Email, string Password) : IRequest<TokenDTO>;