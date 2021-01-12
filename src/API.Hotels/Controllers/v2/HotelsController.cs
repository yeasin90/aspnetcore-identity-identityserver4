using Microsoft.AspNetCore.Mvc;

namespace Api.Hotels.Controllers.v2
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HotelsController : ControllerBase
    {

    }
}
