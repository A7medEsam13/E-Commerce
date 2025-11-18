using E_Commerce.Core.Entities.Product;
using E_Commerce.Core.Interfaces;
using E_Commerce.Infrastructure.Data;

namespace E_Commerce.Infrastructure.Repositries
{
    public class ProductRepository : GenericRepositry<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
