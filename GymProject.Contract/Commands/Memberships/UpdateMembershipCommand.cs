using GymProject.Contract.Dtos;
using MediatR;

namespace GymProject.Contract.Commands.Memberships
{
	public sealed record UpdateMembershipCommand(MembershipDTO membershipDTO) : IRequest;

}
