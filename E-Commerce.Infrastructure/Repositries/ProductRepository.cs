using E_Commerce.Core.Entities.Product;
using E_Commerce.Core.Interfaces;
using E_Commerce.Infrastructure.Data;
using E_Commerce.Infrastructure.Data.DTOs;
using AutoMapper;
using E_Commerce.Core.Services;

namespace E_Commerce.Infrastructure.Repositries
{
    public class ProductRepository : GenericRepositry<Product>, IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context,
            IMapper mapper, 
            IImageManagementService imageManagementService) : base(context)
        {
            _mapper = mapper;
            _imageManagementService = imageManagementService;
            _context = context;
        }

        public async Task<bool> AddAsync(AddProductDto dto)
        {
            if(dto is null)
            {
                return false;
            }

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                OldPrice = dto.OldPrice,
                NewPrice = dto.NewPrice
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            var imagePaths = await _imageManagementService
                .UploadImageAsync(dto.Photos, dto.Name);

            var photo = imagePaths.Select(path => new Photo
            {
                Name = path,
                ProductId = product.Id
            }).ToList();

            await _context.Photos.AddRangeAsync(photo);
            return true;

        }
    }
}
