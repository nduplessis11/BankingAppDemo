using AccountService.Domain.Entities;
using AccountService.Domain.ValueObjects;
using SharedKernel.Application.Commands;

namespace AccountService.Application.Features.Accounts.CreateAccount;

public record CreateAccountCommand(AccountNumber AccountNumber, CustomerId CustomerId)
    : ICommand<CreateAccountResult>;
public record CreateAccountResult(AccountId AccountId);

public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, CreateAccountResult>
{
    public Task<CreateAccountResult> HandleAsync(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        var account = new Account(command.AccountNumber, command.CustomerId);
        Task.Delay(1000, cancellationToken); // Simulate long running operation
        return Task.FromResult(new CreateAccountResult(account.Id));
    }
}
