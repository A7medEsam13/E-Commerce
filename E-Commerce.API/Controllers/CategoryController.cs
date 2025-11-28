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
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // getting the categories from db.
                var categories = await _unitOfWork.Categories.GetAllAsync();

                // check if the categories are null?
                if(categories is null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                return Ok(categories);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseAPI(400, e.Message));
            }
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                // getting the category from db.
                var category = await _unitOfWork.Categories.GetByIdAsync(id);

                // check the nullability.
                if (category is null)
                {
                    return BadRequest(new ResponseAPI(400, $"Category with id {id} not found."));
                }

                return Ok(category);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseAPI(400, e.Message));
            }
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create(CategoryDto dto)
        {
            try
            {
                var category = _mapper.Map<Category>(dto);

                await _unitOfWork.Categories.AddAsync(category);
                await _unitOfWork.CompleteAsync();

                return Ok(new ResponseAPI(200,"new category has been added."));
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseAPI(400, e.Message));
            }
        }


        [HttpPut("update-category")]
        public async Task<IActionResult> Update(UpdateCategoryDto dto)
        {
            try
            {
                var category = _mapper.Map<Category>(dto);
                _unitOfWork.Categories.Update(category);
                await _unitOfWork.CompleteAsync();

                return Ok(new ResponseAPI(200, "Item has been updated"));
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseAPI(400, e.Message));
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _unitOfWork.Categories.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                return Ok(new ResponseAPI(200, "Item has been deleted"));
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseAPI(400, e.Message));
            }
        }
    }
}
