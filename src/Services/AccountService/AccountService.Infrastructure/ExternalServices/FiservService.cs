using System.Text.Json;
using System.Text;
using AccountService.Application.Services;
using System.Text.Json.Serialization;

namespace AccountService.Infrastructure.ExternalServices;

public class FiservService(HttpClient httpClient, JsonSerializerOptions options) : IFiservService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions = options;

    public async Task<FiservAccountResponse> AddAccountAsync(FiservAccountRequest request)
    {
        var jsonContent = JsonSerializer.Serialize(request);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/acctmgmt/accounts")
        {
            Content = content
        };

        httpRequest.Headers.Add("EfxHeader", "efx_header_value");
        httpRequest.Headers.Add("ConsumerAuthToken", "very_secure_plaintext_token");
        httpRequest.Headers.Add("X-OVERRIDETOKEN", "override_token_value");

        var response = await _httpClient.SendAsync(httpRequest);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<FiservAccountResponse>(responseContent, _jsonSerializerOptions);
    }
}

