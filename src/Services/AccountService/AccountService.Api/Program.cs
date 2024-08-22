using AccountService.Application.Features.Accounts.CreateAccount;
using AccountService.Application.Repositories;
using AccountService.Application.Services;
using AccountService.Infrastructure.ExternalServices;
using AccountService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Commands;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICommandHandler<CreateAccountCommand, CreateAccountResult>, CreateAccountCommandHandler>();
builder.Services.AddScoped<CommandDispatcher>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddHttpClient<IFiservService, FiservService>(options =>
{
    options.BaseAddress = new Uri(builder.Configuration["FiservApi:FiservUrl"]!);
});

builder.Services.AddDbContext<AccountDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

var DefaultJsonSerializerOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    Converters = { new JsonStringEnumConverter() }
};

builder.Services.AddSingleton(DefaultJsonSerializerOptions);

var app = builder.Build();

app.UseRouting();
app.MapCreateAccountEndpoint();

app.Run();