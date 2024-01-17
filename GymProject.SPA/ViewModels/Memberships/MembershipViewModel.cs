using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using ThePowerSPAv2.Models.Memberships;
using ThePowerSPAv2.ServicesV2.Memberships;
using ThePowerSPAv2.ViewModels.Base;

namespace ThePowerSPAv2.ViewModels.Memberships;

public class MembershipViewModel : BaseViewModel
{
    private readonly IMembershipService _membershipService;
    private readonly NavigationManager _navigationManager;
    public List<MembershipModel> Memberships = new List<MembershipModel>();
   
   
	public MembershipViewModel(IMembershipService membershipService,NavigationManager navigationManager)
    {
        _membershipService = membershipService;
        _navigationManager = navigationManager;
    }
    
    public override async Task OnInitializedAsync()
    {
        try
        {
            var memeberships = await _membershipService.GetAllAsync();
            Memberships = memeberships.ToList();
        }
        catch
        {
        }
    }

    public async Task<MembershipModel> GetByIdAsync(Guid Id)
    {
        return await _membershipService.GetByIdAsync(Id);
    }
}
