using AccountService.Domain.Entities;
using AccountService.Domain.ValueObjects;
using SharedKernel.Application.Utilities;

namespace AccountService.Application.Repositories;

public interface IAccountRepository
{
    Task AddAsync(Account account);
    Task<Option<Account>> GetByIdAsync(AccountId accountId);
}
