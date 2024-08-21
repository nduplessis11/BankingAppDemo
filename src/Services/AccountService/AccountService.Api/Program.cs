using AccountService.Application.Features.Accounts.CreateAccount;
using SharedKernel.Application.Commands;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICommandHandler<CreateAccountCommand, CreateAccountResult>, CreateAccountCommandHandler>();
builder.Services.AddScoped<CommandDispatcher>();

var app = builder.Build();

app.UseRouting();
app.MapCreateAccountEndpoint();

app.Run();