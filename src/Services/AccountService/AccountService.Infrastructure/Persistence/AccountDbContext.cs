using AccountService.Domain.Entities;
using AccountService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Infrastructure.Persistence;

public class AccountDbContext : DbContext
{
    public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options) { }

    public DbSet<Account> Accounts { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Apply snake_case naming convention
        optionsBuilder.UseSnakeCaseNamingConvention();

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Account>().ToTable("account");

        modelBuilder.Entity<Account>().HasKey(a => a.Id);

        modelBuilder.Entity<Account>()
            .Property(a => a.Id)
            .HasConversion(id => id.Value, value => new(value));

        modelBuilder.Entity<Account>()
            .Property(a => a.AccountNumber)
            .HasConversion(
            an => an.Value,
            value => AccountNumber.From(value));

        modelBuilder.Entity<Account>()
            .Property(a => a.CustomerId)
            .HasConversion(
            cid => cid.Value,
            value => CustomerId.From(value));
    }
}
