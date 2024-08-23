using AccountService.Application.Repositories;
using AccountService.Application.Services;
using AccountService.Domain.Entities;
using AccountService.Domain.ValueObjects;
using SharedKernel.Application.Commands;
using System.Globalization;

namespace AccountService.Application.Features.Accounts.CreateAccount;

public record CreateAccountCommand(AccountNumber AccountNumber, CustomerId CustomerId)
    : ICommand<CreateAccountResult>;
public record CreateAccountResult(AccountId AccountId);

public class CreateAccountCommandHandler(IAccountRepository accountRepository, IFiservService fiservService)
    : ICommandHandler<CreateAccountCommand, CreateAccountResult>
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IFiservService _fiservService = fiservService;

    public async Task<CreateAccountResult> HandleAsync(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        // TODO: Automapper to map DTO between domain and external service models
        var fiservAccountRequest = new FiservAccountRequest
        {
            PartyAcctRelInfo = new PartyAcctRelInfo
            {
                PartyRef = new PartyRef
                {
                    PartyKeys = new PartyKeys
                    {
                        PartyIdentType = "PersonNum",
                        PartyIdent = command.CustomerId.ToString()
                    }
                },
                PartyAcctRelData =
                [
                    new PartyAcctRelData
                    {
                        PartyAcctRelType = "OWNR"
                    }
                ],
                TaxReportingOwnerInd = true,
                TaxReportingSignerInd = true
            },
            DepositAcctInfo = new DepositAcctInfo
            {
                AcctType = "DDA",
                ProductIdent = "Basic Checking",
                OpenDt = DateTime.UtcNow.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Nickname = "My Checking",
                SourceOfFunds = "NMCS"
            },
        };

        var response = await _fiservService.AddAccountAsync(fiservAccountRequest);
        var fiservAcctId = FiservAcctId.From(response.AcctStatusRec.AcctKeys.AcctId);

        // TODO: Let database handle GUID generation for sequential IDs for better performance
        var account = new Account(command.AccountNumber, command.CustomerId, fiservAcctId);

        await _accountRepository.AddAsync(account);

        return new CreateAccountResult(account.Id);
    }
}
