var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/acctmgmt/accounts", async (HttpRequest request) =>
{
    // Mock headers validation (not mandatory for the mock)
    if (!request.Headers.ContainsKey("EfxHeader") ||
        !request.Headers.ContainsKey("ConsumerAuthToken") ||
        !request.Headers.ContainsKey("X-OVERRIDETOKEN"))
    {
        return Results.BadRequest(new
        {
            status = new
            {
                statusCode = "400",
                statusDesc = "Missing headers",
                severity = "Error"
            }
        });
    }

    // Read and log the request body (for debugging purposes)
    using var reader = new StreamReader(request.Body);
    var requestBody = await reader.ReadToEndAsync();

    // Mock response
    var response = new
    {
        status = new
        {
            statusCode = "0",
            statusDesc = "Success",
            severity = "Info",
            svcProviderName = "DNA"
        },
        acctStatusRec = new
        {
            acctKeys = new
            {
                acctId = "10183903051613",
                acctType = "DDA"
            },
            acctStatus = new
            {
                acctStatusCode = "Valid",
                effDt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
            }
        }
    };

    return Results.Created($"/acctmgmt/accounts/10183903051613", response);
})
.WithName("AddAccountDDA");

app.Run();
