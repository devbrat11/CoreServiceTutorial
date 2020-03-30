using CoreService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            return Ok();
        }
    }
}