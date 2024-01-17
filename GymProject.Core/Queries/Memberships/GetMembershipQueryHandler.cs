using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymProject.Contract.Dtos;
using GymProject.Contract.Queries.Memberships;
using GymProject.Core.Exceptions.Memberships;
using GymProject.Core.Maps;
using GymProject.Infastructure.Repositories.Memberships;
using GymProject.Core.UserExceptions;

namespace GymProject.Core.Queries.Memberships
{
    internal class GetMembershipQueryHandler : IRequestHandler<GetMembershipQuery, MembershipDTO>
	{
		private readonly IMembershipRepository _membershipRepository;
        public GetMembershipQueryHandler(IMembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }
        public async Task<MembershipDTO> Handle(GetMembershipQuery request, CancellationToken cancellationToken)
		{
			var membership = await _membershipRepository.GetAsync(request.id);
			if (membership is null)
				throw new MembershipNullException();
			return MembershipMapping.MapToDTO(membership);
		}
	}
}
