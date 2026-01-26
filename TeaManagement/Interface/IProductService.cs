using TeaManagement.Dtos;
using TeaManagement.Entities;

namespace TeaManagement.Interface;

public interface IProductService
{
    public Task<Product> CreateProductAsync(ProductDto dto);
}

