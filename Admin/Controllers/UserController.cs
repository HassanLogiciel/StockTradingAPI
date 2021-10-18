using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(policy:"Admin")]
    public class UserController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok();
        }

        public IActionResult AsPost()
        {
            return Ok();
        }
    }
}
