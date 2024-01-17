using MediatR;

namespace GymProject.Contract.Commands.Auth;

public sealed record SignUpCommand(string Fullname, string Email, string Password, string ConfirmPassword) : IRequest;
