using AutoMapper;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Services;
using E_Commerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;

        public ICategoryRepository Categories { get; }

        public IProductRepository Products { get; }

        public IPhotoRepository Photos { get; }

        public UnitOfWork(AppDbContext context, 
            IMapper mapper, 
            IImageManagementService imageManagementService)
        {
            _context = context;
            _mapper = mapper;
            _imageManagementService = imageManagementService;
            Categories = new CategoryRepository(_context);
            Products = new ProductRepository(_context, _mapper, _imageManagementService);
            Photos = new PhotoRepository(_context);
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
