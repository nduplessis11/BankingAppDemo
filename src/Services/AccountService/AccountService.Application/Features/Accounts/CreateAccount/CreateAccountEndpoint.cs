using AccountService.Domain.ValueObjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharedKernel.Application.Commands;

namespace AccountService.Application.Features.Accounts.CreateAccount;

public record struct CreateAccountRequest
{
    public string AccountNumber { get; set; }
    public Guid CustomerId { get; set; }
}

public record struct CreateAccountResponse
{
    public Guid AccountId { get; set; }
}

public static class CreateAccountEndpoint
{
    public static void MapCreateAccountEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/accounts", async (HttpContext context, CommandDispatcher commandDispatcher) =>
        {
            var request = await context.Request.ReadFromJsonAsync<CreateAccountRequest>();

            var command = new CreateAccountCommand(
                AccountNumber.From(request.AccountNumber),
                CustomerId.From(request.CustomerId)
            );

            var result = await commandDispatcher.DispatchAsync<CreateAccountCommand, CreateAccountResult>(command);

            context.Response.StatusCode = StatusCodes.Status201Created;
            await context.Response.WriteAsJsonAsync<CreateAccountResponse>(new CreateAccountResponse
            {
                AccountId = result.AccountId.Value
            });
        })
        .WithName("CreateAccount");
    }
}