using E_Commerce.Core.Entities.Product;
using E_Commerce.Infrastructure.Data.DTOs;

namespace E_Commerce.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<bool> AddAsync(AddProductDto dto);
    }
}
