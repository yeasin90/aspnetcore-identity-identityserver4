﻿using Microsoft.AspNetCore.Mvc;

namespace Api.Hotels.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HotelsController : ControllerBase
    {

    }
}
