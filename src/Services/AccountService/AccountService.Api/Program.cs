using AccountService.Application.Features.Accounts.CreateAccount;
using AccountService.Application.Repositories;
using AccountService.Application.Services;
using AccountService.Infrastructure.ExternalServices;
using AccountService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Commands;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICommandHandler<CreateAccountCommand, CreateAccountResult>, CreateAccountCommandHandler>();
builder.Services.AddScoped<CommandDispatcher>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddHttpClient<IFiservService, FiservService>();

builder.Services.AddDbContext<AccountDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

var app = builder.Build();

app.UseRouting();
app.MapCreateAccountEndpoint();

app.Run();