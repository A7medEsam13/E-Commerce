using E_Commerce.API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("errors/{statuscode}")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error(int statuscode)
        {
            return new ObjectResult(new ResponseAPI(statuscode));
        }
    }
}
