using AutoMapper;
using E_Commerce.API.Helper;
using E_Commerce.Core.Entities.Product;
using E_Commerce.Core.Interfaces;
using E_Commerce.Infrastructure.Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // getting all products.
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _unitOfWork.Products
                    .GetAllAsync(p => p.Category, p => p.Photos);

                if(products is null)
                {
                    return BadRequest(new ResponseAPI(400, "Products not found."));
                }

                return Ok(products); 
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }


        // get product by Id.
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await _unitOfWork.Products
                    .GetByIdAsync(id, p => p.Category, p => p.Photos);

                if(product is null)
                {
                    return BadRequest(new ResponseAPI(400, $"Product with Id {id} not found"));
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));                
            }
        }


        // create product endpoint.
        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct(ProductDto dto)
        {
            try
            {
                var product = _mapper.Map<Product>(dto);
                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.CompleteAsync();

                return Ok(new ResponseAPI(201, "product has been created."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }


        // update product endpoint.
        [HttpPut("update")]
        public async Task<IActionResult> Update(ProductDto dto)
        {
            try
            {
                var product = _mapper.Map<Product>(dto);

                _unitOfWork.Products.Update(product);
                await _unitOfWork.CompleteAsync();
                return Ok(new ResponseAPI(200, "product has been updated successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        // delete endpoint.
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _unitOfWork.Products.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();

                return Ok(new ResponseAPI(200, $"Product with Id {id} has been deleted."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
    }
}
