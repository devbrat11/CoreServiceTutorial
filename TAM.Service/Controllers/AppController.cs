using Microsoft.AspNetCore.Mvc;

namespace TAMService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAppStatus()
        {
            return Ok("App is running...");
        }
    }
}