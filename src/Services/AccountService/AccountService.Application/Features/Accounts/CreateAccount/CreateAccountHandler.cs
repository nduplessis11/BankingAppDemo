using AccountService.Domain.Entities;
using AccountService.Domain.ValueObjects;
using SharedKernel.Application.Commands;

namespace AccountService.Application.Features.Accounts.CreateAccount;

public record CreateAccountCommand(AccountNumber accountNumber, CustomerId customerId)
    : ICommand<CreateAccountResult>;
public record CreateAccountResult(AccountId accountId);

internal class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, CreateAccountResult>
{
    public Task<CreateAccountResult> HandleAsync(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        var account = new Account(command.accountNumber, command.customerId);
        Task.Delay(1000, cancellationToken); // Simulate long running operation
        return Task.FromResult(new CreateAccountResult(account.Id));
    }
}
