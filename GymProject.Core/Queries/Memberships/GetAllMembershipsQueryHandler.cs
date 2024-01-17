using GymProject.Contract.Dtos;
using GymProject.Contract.Queries.Memberships;
using GymProject.Core.Maps;
using GymProject.Infastructure.Repositories.Memberships;
using MediatR;
using GymProject.Domain.Models.Memberships;

namespace GymProject.Core.Queries.Memberships
{
	internal class GetAllMembershipQueryHandler : IRequestHandler<GetAllMembershipsQuery, IEnumerable<MembershipDTO>>
	{
		private readonly IMembershipRepository _membershipRepository;
		public GetAllMembershipQueryHandler(IMembershipRepository membershipRepository)
		{
			_membershipRepository = membershipRepository;
		}

		public async Task<IEnumerable<MembershipDTO>> Handle(GetAllMembershipsQuery request, CancellationToken cancellationToken)
		{
			var asd = await _membershipRepository.GetAllAsync();
			var list = asd.Select(o => MembershipMapping.MapToDTO(o));
			return list;
		}
	}
}
