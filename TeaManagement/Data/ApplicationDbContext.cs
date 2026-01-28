using Microsoft.EntityFrameworkCore;
using TeaManagement.Entities;
using Ledger = TeaManagement.Entities.Ledger;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<NewFactory> Factories { get; set; }
    public DbSet<AddBonus> Bonus { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<CoaLedger> ChartOfAccounts { get; set; }
    public DbSet<Ledger> Ledgers { get; set; }
    public DbSet<Receivable> Receivable { get; set; }
    public DbSet<Payable> Payable { get; set; }
    public DbSet<Stakeholder> Stakeholders { get; set; }
    public DbSet<ProductUnit> ProductUnits { get; set; }
    public DbSet<AccountingTransaction> AccTransaction { get; set; }
    public DbSet<TransactionDetails> AccTransactionDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options
            .UseNpgsql(WebApplication.CreateBuilder().Configuration.GetConnectionString("DefaultConnection"))
            .UseSnakeCaseNamingConvention();
    }
}