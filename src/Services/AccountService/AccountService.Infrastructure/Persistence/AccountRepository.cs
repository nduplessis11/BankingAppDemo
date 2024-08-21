using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Utilities;

namespace AccountService.Infrastructure.Persistence;

public class AccountRepository : IAccountRepository
{
    private readonly AccountDbContext _context;

    public AccountRepository(AccountDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
    }

    public async Task<Option<Account>> GetByIdAsync(AccountId accountId)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
        return account == null ? Option<Account>.None() : Option<Account>.Some(account);
    }
}
