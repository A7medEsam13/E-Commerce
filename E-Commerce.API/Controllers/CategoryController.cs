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

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                    return BadRequest();
                }
                return Ok(categories);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
                    return BadRequest();
                }

                return Ok(category);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create(CategoryDto dto)
        {
            try
            {
                var category = new Category
                {
                    Name = dto.Name,
                    Description = dto.Description
                };

                await _unitOfWork.Categories.AddAsync(category);
                await _unitOfWork.CompleteAsync();

                return Ok(new { message = "Item has been added" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut("update-category")]
        public async Task<IActionResult> Update(UpdateCategoryDto dto)
        {
            try
            {
                var category = new Category
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Description
                };
                _unitOfWork.Categories.Update(category);
                await _unitOfWork.CompleteAsync();

                return Ok(new { message = "Item has been updated" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _unitOfWork.Categories.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                return Ok(new { message = "Item has been deleted" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
