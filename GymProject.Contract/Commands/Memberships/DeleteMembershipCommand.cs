using MediatR;

namespace GymProject.Contract.Commands.Memberships
{
	public sealed record DeleteMembershipCommand(Guid Id) : IRequest;
}
