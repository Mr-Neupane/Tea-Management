using TeaManagement.Dtos;

namespace TeaManagement.Repository.Interface;

public interface IReportRepository
{
    public Task<List<ProductReportDto>> GetProductReportAsync(int? status);
    public Task<List<FactoryReportDto>> GetFactoryReportAsync(int? status);
    public Task<List<LedgerReportDto>> LedgerReportAsync();
    public Task<List<StakeholderReportDto>> GetStakeholderReportAsync(bool isSupplier, int? status);
}