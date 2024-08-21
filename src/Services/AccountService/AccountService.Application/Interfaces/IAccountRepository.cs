using AccountService.Domain.Entities;
using AccountService.Domain.ValueObjects;

namespace AccountService.Application.Interfaces;

public interface IAccountRepository
{
    Task AddAsync(Account account);
    Task<Account> GetByIdAsync(AccountId accountId);
}
