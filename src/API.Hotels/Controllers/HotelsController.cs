using Microsoft.AspNetCore.Mvc;

namespace AuthManagement.Hotels.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class HotelsController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok("Hotels");
        }
    }
}
