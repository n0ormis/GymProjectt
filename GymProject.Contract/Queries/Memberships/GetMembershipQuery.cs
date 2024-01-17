using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymProject.Contract.Dtos;

namespace GymProject.Contract.Queries.Memberships
{
    public sealed record GetMembershipQuery(Guid id) : IRequest<MembershipDTO>;
}