using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using TeaManagement.Data;
using TeaManagement.Interface;
using TeaManagement.Manager;
using TeaManagement.Providers;
using TeaManagement.Repository;
using TeaManagement.Repository.Interface;
using TeaManagement.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFactoryService, FactoryService>();
builder.Services.AddScoped<IAccountingTransactionService, AccountingTransactionService>();
builder.Services.AddScoped<IBonusService, BonusService>();
builder.Services.AddScoped<ISalesService, SalesService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ILedgerService, LedgerService>();
builder.Services.AddScoped<IStakeholderService, StakeholderService>();
builder.Services.AddScoped<IReceivableService, ReceivableService>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<DropdownProvider>();
builder.Services.AddScoped<FactoryManager>();
builder.Services.AddScoped<BonusManager>();
builder.Services.AddScoped<IdProvider>();
builder.Services.AddScoped<SalesTransactionManager>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention());
builder.Services.AddControllersWithViews()
    .AddNToastNotifyToastr(new ToastrOptions
    {
        // ProgressBar = true,
        PositionClass = ToastPositions.BottomRight,
        CloseButton = true,
        TimeOut = 5000
    });


var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

db.Database.Migrate();

app.Services.EnsurePostgresDatabaseCreated();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();