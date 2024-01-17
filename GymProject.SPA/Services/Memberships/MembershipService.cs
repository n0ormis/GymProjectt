using System.Net.Http.Json;
using ThePowerSPAv2.Models.Memberships;

namespace ThePowerSPAv2.ServicesV2.Memberships;

internal sealed class MembershipService : IMembershipService
{
    private readonly HttpClient _client;

    public MembershipService(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<MembershipModel>> GetAllAsync()
    {
        var getAll = await _client.GetFromJsonAsync<IEnumerable<MembershipModel>>("api/Membership/get-all");
        return getAll;
    }

    public async Task<MembershipModel> GetByIdAsync(Guid id)
    {
        return await _client.GetFromJsonAsync<MembershipModel>($"api/Membership/get?id={id}");
    }
}