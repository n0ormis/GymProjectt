using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using GymProject.SPA.Features.Auth.Services;
using GymProject.SPA.Models.Auth;
using ThePowerSPAv2.Defenisions;
using ThePowerSPAv2.Services.IdentityService;
using ThePowerSPAv2.ViewModels.Base;

namespace ThePowerSPAv2.ViewModels.Identity;

public class IdentityViewModel : BaseViewModel
{
    private readonly IIdentityService _identityService;
    private readonly IAuthenticationService _authenticationService;
    private readonly NavigationManager _navigationManager;
    private readonly ISnackbar _snackbar;
    
    public SignUpModel SignUpModel = new();
    public SignInModel SignInModel = new();

    public IdentityViewModel(IIdentityService identityService, HttpClient client, IAuthenticationService authenticationService, ISnackbar snackbar, NavigationManager navigationManager)
    {
        _identityService = identityService;
        _authenticationService = authenticationService;
        _snackbar = snackbar;
        _navigationManager = navigationManager;
    }

    public override async Task OnInitializedAsync()
    {
    }

    public async Task SignUpAsync()
    {
        var result = await _identityService.SignUp(SignUpModel);
        if (result)
        {
            _navigationManager.NavigateTo(PageUri.CONFIRM_MAIL);
        }
    }
    
    public async Task SignInAsync()
    {
        var result = await _authenticationService.SignIn(SignInModel);
        if (string.IsNullOrEmpty(result))
        {
            _snackbar.Add("Successfuly authorized", Severity.Success);
            _navigationManager.NavigateTo("/");
        }
        else
        {
            _snackbar.Add(result, Severity.Error);
        }
    }
}