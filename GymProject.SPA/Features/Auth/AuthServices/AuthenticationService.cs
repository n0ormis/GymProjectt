using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;
using GymProject.Contract.Dtos.Auth;
using GymProject.SPA.Models.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using TokenDTO = GymProject.Contract.Dtos.Auth.TokenDTO;

namespace GymProject.SPA.Features.Auth.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _options;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorage;
    

    public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider,
        ILocalStorageService localStorage)
    {
        _client = client;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _authStateProvider = authStateProvider;
        _localStorage = localStorage;
    }

    public async Task<bool> UserAuthenticated()
    {
        var state = await _authStateProvider.GetAuthenticationStateAsync();
        if (state.User is null || state.User.Identity is null || state.User.Identity.IsAuthenticated == false)
            return false;
        var s = state.User.Claims;
        return true;
    }

    public async Task<string> SignIn(SignInModel model)
    {
        var content = JsonSerializer.Serialize(model);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        var authResult = await _client.PostAsync("api/identity/sign-in", bodyContent);
        var authContent = await authResult.Content.ReadAsStringAsync();
        try
        {
            TokenDTO result = null;
            try
            {
                result = JsonSerializer.Deserialize<TokenDTO>(authContent, _options);
            }
            catch
            {
                
            }
            if (!authResult.IsSuccessStatusCode)
                return "Invalid user credentials";
            await _localStorage.SetItemAsync("authToken", result.Token);
            await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
            return "";
        }
        catch(Exception ex)
        {
            return authContent;
        }
        
    }
    
    public async Task<string> RefreshToken()
    {
        try
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");
            var tokenDto = JsonSerializer.Serialize(new TokenDTO() { Token = token, RefreshToken = refreshToken });
            var bodyContent = new StringContent(tokenDto, Encoding.UTF8, "application/json");
            var refreshResult = await _client.PostAsync("api/identity/refresh-token", bodyContent);
            var refreshContent = await refreshResult.Content.ReadAsStringAsync();
            if (!refreshResult.IsSuccessStatusCode)
            {
                ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
                return "";
            }
            var result = JsonSerializer.Deserialize<TokenDTO>(refreshContent, _options);
            await _localStorage.SetItemAsync("authToken", result.Token);
            await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
            SetCurrentUser(result.Token);
            return result.Token;
        }
        catch
        {
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            return "";
        }
    }

    private void SetCurrentUser(string token)
    {
        ((AuthStateProvider)_authStateProvider).User = new LoginUser(token);
    }
    public async Task<ProfileDto> GetProfile()
    {
        var authResult = await _client.GetAsync("api/identity/get");
        var authContent = await authResult.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ProfileDto>(authContent, _options);

        return result;
    }

    public string GetCurrentToken()
    {
        return ((AuthStateProvider)_authStateProvider).User.Token;
    }
}
