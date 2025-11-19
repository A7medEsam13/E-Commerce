using E_Commerce.Core.Interfaces;
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
                throw e;
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

                throw e;
            }
        }
    }
}
