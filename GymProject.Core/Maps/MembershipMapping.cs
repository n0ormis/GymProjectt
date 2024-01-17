using GymProject.Contract.Dtos;
using GymProject.Domain.Models.Memberships;

namespace GymProject.Core.Maps
{
    internal static class MembershipMapping
    {
        internal static MembershipDTO MapToDTO(Subscription membership)
            => new()
            {
                Id = membership.Id,
                Name = membership.Name,
                Description = membership.Description,
                Price = membership.Price,
                Duration = membership.Duration
            };
    }
}