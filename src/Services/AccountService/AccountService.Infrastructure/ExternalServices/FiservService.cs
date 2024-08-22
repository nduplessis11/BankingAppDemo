using System.Text.Json;
using System.Text;
using AccountService.Application.Services;

namespace AccountService.Infrastructure.ExternalServices;

public class FiservService : IFiservService
{
    private readonly HttpClient _httpClient;

    public FiservService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<FiservAccountResponse> AddAccountAsync(FiservAccountRequest request)
    {
        var jsonContent = JsonSerializer.Serialize(request);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        const string tempUrlBase = "http://localhost:6100";
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{tempUrlBase}/acctmgmt/accounts")
        {
            Content = content
        };

        httpRequest.Headers.Add("EfxHeader", "efx_header_value");
        httpRequest.Headers.Add("ConsumerAuthToken", "very_secure_plaintext_token");
        httpRequest.Headers.Add("X-OVERRIDETOKEN", "override_token_value");

        var response = await _httpClient.SendAsync(httpRequest);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<FiservAccountResponse>(responseContent);
    }
}

