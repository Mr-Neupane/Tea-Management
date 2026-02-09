using Microsoft.EntityFrameworkCore;
using NToastNotify;
using TeaManagement.Interface;
using TeaManagement.Manager;
using TeaManagement.Providers;
using TeaManagement.Repository;
using TeaManagement.Repository.Interface;
using TeaManagement.Services;

namespace TeaManagement;

public static class DiConfig
{
    public static void UseApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention());

        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<IFactoryService, FactoryService>();
        builder.Services.AddScoped<IAccountingTransactionService, AccountingTransactionService>();
        builder.Services.AddScoped<IBonusService, BonusService>();
        builder.Services.AddScoped<ISalesService, SalesService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ILedgerService, LedgerService>();
        builder.Services.AddScoped<IStakeholderService, StakeholderService>();
        builder.Services.AddScoped<IReceivableService, ReceivableService>();

        builder.UseManagers();
        builder.UseRepository();
        builder.UseProviders();
        builder.UseNotificationService();
    }

    private static void UseRepository(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IReportRepository, ReportRepository>();
    }

    private static void UseProviders(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IdProvider>();
        builder.Services.AddScoped<DropdownProvider>();
    }

    private static void UseManagers(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<FactoryManager>();
        builder.Services.AddScoped<BonusManager>();
        builder.Services.AddScoped<SalesTransactionManager>();
    }

    private static void UseNotificationService(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews()
            .AddNToastNotifyToastr(new ToastrOptions
            {
                // ProgressBar = true,
                PositionClass = ToastPositions.BottomRight,
                CloseButton = true,
                TimeOut = 5000
            });
    }
}