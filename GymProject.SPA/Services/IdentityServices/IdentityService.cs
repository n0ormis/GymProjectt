using System.Net.Http.Json;
using GymProject.SPA.Models.Auth;

namespace ThePowerSPAv2.Services.IdentityService;

internal sealed class IdentityService : IIdentityService
{
    private readonly HttpClient _client;

    public IdentityService(HttpClient client)
    {
        _client = client;
    }

    public async Task<bool> SignUp(SignUpModel model)
    {
        var request = await _client.PostAsJsonAsync("api/Identity/sign-up",
            new SignUpModel() {Fullname = model.Fullname, Email = model.Email, Password = model.Password, ConfirmPassword = model.ConfirmPassword });
        if (request.RequestMessage is not null && request.RequestMessage.Content is not null)
        {
            var asd = request.RequestMessage.Content.ReadAsStringAsync();
        }

        return request.IsSuccessStatusCode;
    }

    public async Task<bool> SignIn(SignInModel model)
    {
        var request = await _client.PostAsJsonAsync("api/Identity/sign-in",
            new SignInModel() { Email = model.Email, Password = model.Password });
        if (request is not null && request.Content is not null)
        {
            var asd = request.Content.ReadAsStringAsync();
        }

        return request.IsSuccessStatusCode;
    }
}