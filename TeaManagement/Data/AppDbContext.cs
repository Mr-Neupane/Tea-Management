using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TeaManagement.Entities;
using Ledger = TeaManagement.Entities.Ledger;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<NewFactory> Factories { get; set; }
    public DbSet<AddBonus> Bonus { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<CoaLedger> ChartOfAccounts { get; set; }
    public DbSet<Ledger> Ledgers { get; set; }
    public DbSet<AccountingTransaction> AccTransaction { get; set; }
    public DbSet<TransactionDetails> AccTransactionDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options
            .UseNpgsql(WebApplication.CreateBuilder().Configuration.GetConnectionString("DefaultConnection"))
            .UseSnakeCaseNamingConvention();
    }
}