using System.Text.Json;
using System.Text;

namespace AccountService.Infrastructure.ExternalServices;

public class FiservClient
{
    private readonly HttpClient _httpClient;

    public FiservClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

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
        return JsonSerializer.Deserialize<FiservAccountResponse>(responseContent);
    }
}

public readonly record struct FiservAccountRequest(
    PartyAcctRelInfo PartyAcctRelInfo,
    DepositAcctInfo DepositAcctInfo,
    OVRDEXCEPTIONDATA OvrExceptionData);

public readonly record struct PartyAcctRelInfo(
    PartyRef PartyRef,
    PartyAcctRelData[] PartyAcctRelData,
    bool TaxReportingOwnerInd,
    bool TaxReportingSignerInd);

public readonly record struct PartyRef(
    PartyKeys PartyKeys);

public readonly record struct PartyKeys(
    string PartyIdentType,
    string PartyIdent);

public readonly record struct PartyAcctRelData(
    string PartyAcctRelType);

public readonly record struct DepositAcctInfo(
    string AcctType,
    string ProductIdent,
    string OpenDt,
    string MaturityDt,
    string Nickname,
    string HandlingCode,
    string SourceOfFunds);

public readonly record struct OVRDEXCEPTIONDATA(
    OVRDEXCEPTION[] OVERRIDEEXCEPTION);

public readonly record struct OVRDEXCEPTION(
    string SUBJECTROLE);

public readonly record struct FiservAccountResponse(
    Status Status,
    AcctStatusRec AcctStatusRec);

public readonly record struct Status(
    string StatusCode,
    string StatusDesc,
    string Severity,
    string SvcProviderName);

public readonly record struct AcctStatusRec(
    AcctKeys AcctKeys,
    AcctStatus AcctStatus);

public readonly record struct AcctKeys(
    string AcctId,
    string AcctType);

public readonly record struct AcctStatus(
    string AcctStatusCode,
    string EffDt);