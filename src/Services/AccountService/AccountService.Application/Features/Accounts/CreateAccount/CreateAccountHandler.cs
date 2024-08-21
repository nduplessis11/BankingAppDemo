using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Domain.ValueObjects;
using SharedKernel.Application.Commands;

namespace AccountService.Application.Features.Accounts.CreateAccount;

public record CreateAccountCommand(AccountNumber AccountNumber, CustomerId CustomerId)
    : ICommand<CreateAccountResult>;
public record CreateAccountResult(AccountId AccountId);

public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, CreateAccountResult>
{
    private readonly IAccountRepository _accountRepository;

    public CreateAccountCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<CreateAccountResult> HandleAsync(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        // TODO: Need to let database handle creating the GUID
        var account = new Account(command.AccountNumber, command.CustomerId);

        await _accountRepository.AddAsync(account);

        return new CreateAccountResult(account.Id);
    }
}
