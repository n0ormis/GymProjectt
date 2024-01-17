using ThePowerSPAv2.Models.Memberships;

namespace ThePowerSPAv2.ServicesV2.Memberships;

public interface IMembershipService
{
    Task<IEnumerable<MembershipModel>> GetAllAsync();
    Task<MembershipModel> GetByIdAsync(Guid id);
}